using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeingHaresSimulator.Common
{
    public sealed class LogisticFunction
    {
        [Description("The x-value of the sigmoid's midpoint")]
        public double X0 { get; set; } = 0;

        [Description("The steepness of the curve")]
        public double K { get; set; } = 1;

        public override string ToString()
        {
            return $"Logistic{{x0={X0}, k={K}}}";
        }

        public double Evaluate(double x)
        {
            return 1 / (1 + Math.Exp(-this.K * (x - this.X0)));
        }

        public LogisticFunction Clone()
        {
            return (LogisticFunction)this.MemberwiseClone();
        }
    }
}
