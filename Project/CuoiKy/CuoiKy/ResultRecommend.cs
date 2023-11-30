using Microsoft.ML.Data;

namespace CuoiKy
{
    public class ResultRecommend: Recommendation
    {
        [ColumnName("Score")]
        public float Score { get; set; }
    }
}
