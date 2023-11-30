using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.ML.DataOperationsCatalog;

using System.IO;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms;
using System.Net;
using Microsoft.ML.Trainers;

namespace CuoiKy
{
    internal class DemoRecommendation
    {
        private static MLContext context = new MLContext();
        private static IDataView dataView;
        private static TrainTestData TrainTestData;
        private static ITransformer model;
        private static EstimatorChain<ValueToKeyMappingTransformer> estimator;
        public static void Execute()
        {
            LoadData();
            PreProcessData();
            CreateModel();
            EvaluateModel();
            PredictModel();

        }

        private static void PredictModel()
        {
            var predictionEngine = context.Model.CreatePredictionEngine<Recommendation, ResultRecommend>(model);
            var prediction = predictionEngine.Predict(new Recommendation { CustomerID = 5, DestinationID = 7 });
            PrintResult(prediction);
        }

        private static void PrintResult(ResultRecommend result)
        {
            Console.WriteLine($"Predicted Rating: {result.Rating}");
        }

       


        private static void EvaluateModel()
        {
            var predictions = model.Transform(TrainTestData.TestSet);
            var metrics = context.Recommendation().Evaluate(predictions, labelColumnName: nameof(Recommendation.Rating));
            Console.WriteLine("R^2: {0} | LossFunction: {1}| MeanAsoluteError: {2} | MeanSquareError: {3}",
                metrics.RSquared, metrics.LossFunction, metrics.MeanSquaredError, metrics.MeanSquaredError);
        }

        private static void CreateModel()
        {
           var options = new MatrixFactorizationTrainer.Options
           {
               LabelColumnName = nameof(Recommendation.Rating),
               MatrixColumnIndexColumnName = "Encoded_CustomerID",
               MatrixRowIndexColumnName ="Encoded_DestinationID",
               NumberOfIterations = 100,
               ApproximationRank = 100

           };
            var trainer = context.Recommendation().Trainers.MatrixFactorization(options);
            var pipeline = estimator.Append(trainer);
            model = pipeline.Fit(TrainTestData.TrainSet);
        }

        private static void PreProcessData()
        {
            estimator = context.Transforms.Conversion.MapValueToKey(
                outputColumnName: "Encoded_CustomerID",
                inputColumnName: nameof(Recommendation.CustomerID) ).Append(context.Transforms.Conversion.
                MapValueToKey(outputColumnName: "Encoded_DestinationID", inputColumnName: nameof(Recommendation.DestinationID)));
            var preProcessData = estimator.Fit(dataView).Transform(dataView);
            TrainTestData = context.Data.TrainTestSplit(preProcessData, 0.05);
        }

        private static void LoadData()
        {
            dbTourismDataContext db = new dbTourismDataContext();
            var reviewData = from review in db.Reviews
                             select new
                             {
                                 Rating = (float)review.Rating,
                                 DestinationID = review.DestinationID,
                                 CustomerID = review.CustomerID
                             };
            dataView = context.Data.LoadFromEnumerable(reviewData);
        }
    }
   
    
}
