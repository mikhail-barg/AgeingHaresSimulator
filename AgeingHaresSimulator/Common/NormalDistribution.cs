using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeingHaresSimulator.Common
{
    public sealed class NormalDistribution
    {
        private static readonly double SQRT_2PI = Math.Sqrt(2 * Math.PI);

        public double Mean { get; set; } = 0.0;
        public double StdDev { get; set; } = 1.0;

        public override string ToString()
        {
            return $"N{{{Mean}, {StdDev}}}";
        }

        public NormalDistribution Clone()
        {
            return (NormalDistribution)this.MemberwiseClone();
        }

        public double Sample(Random random)
        {
            //see http://stackoverflow.com/a/218600
            // a Box-Muller transform
            double u1 = random.NextDouble(); //these are uniform(0,1) random doubles
            double u2 = random.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            double randNormal = this.Mean + this.StdDev * randStdNormal; //random normal(mean,stdDev^2)

            return randNormal;
        }

        internal double Transform(double x)
        {
            //https://en.wikipedia.org/wiki/Normal_distribution
            /*
             
             Y = 1 / Sqrt(2 * Pi * stdDev^2) * exp(- (x - Mean)^2 / (2*stdDev^2)
             
             */

            double y = 1.0 / (SQRT_2PI * this.StdDev) * Math.Exp(-(x - this.Mean) * (x - this.Mean) / (2 * this.StdDev * this.StdDev));
            return y;
        }
    }
}
