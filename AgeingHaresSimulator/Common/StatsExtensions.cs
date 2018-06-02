using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeingHaresSimulator.Common
{
    public static class StatsExtensions
    {
        public static Stats ToStats(this IEnumerable<double> values)
        {
            return new Stats(values);
        }


    }
}
