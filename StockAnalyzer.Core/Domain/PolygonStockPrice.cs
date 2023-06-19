using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.Core.Domain
{
    public class PolygonStockPrice
    {
        public BigInteger queryCount { get; set; }
        public int resultsCount { get; set; }
        public bool adjested { get; set; }
        public string request_id { get; set; }
        public List<Results> results { get; set; } 
    }
    public class Results
    {
        public string T { get; set; }
        public double c { get; set; }
        public double h { get; set; }
        public double l { get; set; }
        public double o { get; set; }
        public bool oct { get; set; }
        public BigInteger t { get; set; }
        public BigInteger v { get; set; }
        public double vw { get; set; }
    }
}
