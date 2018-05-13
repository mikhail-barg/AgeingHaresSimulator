using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeingHaresSimulator.Common
{
    public sealed class Stats
    {
        public double minValue { get; set; }
        public double maxValue { get; set; }
        public double avgValue { get; set; }
        public double medianValue { get; set; }
        public double q1Value { get; set; }
        public double q3Value { get; set; }

        //Used by CsvHelper
        public Stats() { }

        internal Stats(IEnumerable<double> source)
        {
            List<double> data = source.ToList();
            if (data.Count > 0)
            {
                data.Sort();
                minValue = data[0];
                maxValue = data[data.Count - 1];
                medianValue = data[data.Count / 2];
                q1Value = data[data.Count / 4];
                q3Value = data[data.Count * 3 / 4];
                avgValue = data.Average();
            }
            else
            {
                minValue = 0;
                maxValue = 0;
                medianValue = 0;
                q1Value = 0;
                q3Value = 0;
                avgValue = 0;
            }
        }
    }
}
