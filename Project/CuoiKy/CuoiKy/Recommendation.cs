using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.Data;

namespace CuoiKy
{
    public class Recommendation
    {
        [LoadColumn(0)]
        public float Rating { get; set; }

        [LoadColumn(1)]
        public int DestinationID { get; set; }

        [LoadColumn(2)]
        public int CustomerID { get; set; }
    }
}
