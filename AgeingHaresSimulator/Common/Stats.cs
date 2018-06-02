using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeingHaresSimulator.Common
{
    public sealed class Stats
    {
        public int count { get; set; }
        public double minValue { get; set; }
        public double maxValue { get; set; }
        public double avgValue { get; set; }
        public double medianValue { get; set; }
        public double q1Value { get; set; }
        public double q3Value { get; set; }
        public double stdDev { get; set; }

        //Used by CsvHelper
        public Stats() { }

        internal Stats(IEnumerable<double> source)
        {
            List<double> data = source.ToList();
            count = data.Count;
            if (count > 0)
            {
                data.Sort();
                minValue = data[0];
                maxValue = data[count - 1];
                medianValue = data[count / 2];
                q1Value = data[count / 4];
                q3Value = data[count * 3 / 4];
                avgValue = data.Average();
                stdDev = Math.Sqrt(data.Sum(item => (avgValue - item) * (avgValue - item)) / (count > 1 ? count : 1));
            }
            else
            {
                minValue = 0;
                maxValue = 0;
                medianValue = 0;
                q1Value = 0;
                q3Value = 0;
                avgValue = 0;
                stdDev = 0;
            }
        }
    }
}
