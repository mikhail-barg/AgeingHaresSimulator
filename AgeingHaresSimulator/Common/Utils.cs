using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgeingHaresSimulator.Common
{
    public static class Utils
    {
        public static bool ProbCheck(this Random random, double probability)
        {
            return random.NextDouble() < probability;
        }

        public static void Shuffle<T>(this Random random, List<T> list)
        {
            for (int i = 0; i < list.Count - 1; ++i)
            {
                int j = random.Next(i, list.Count); //minValue is inclusive, maxValue is exclusive
                T tmp = list[j];
                list[j] = list[i];
                list[i] = tmp;
            }
        }

        //see https://en.wikipedia.org/wiki/Pearson_correlation_coefficient#For_a_sample
        public static double CalculateCorrelation<T>(IEnumerable<T> data, Func<T, double> xSelector, Func<T, double> ySelector)
        {
            int n = data.Count();
            double xAvg = 0;
            double yAvg = 0;
            foreach (T item in data)
            {
                xAvg += xSelector(item);
                yAvg += ySelector(item);
            }
            xAvg /= n;
            yAvg /= n;

            double sxx = 0;
            double syy = 0;
            double sxy = 0;
            foreach (T item in data)
            {
                double x = xSelector(item);
                double y = ySelector(item);
                sxx += (x - xAvg) * (x - xAvg);
                syy += (y - yAvg) * (y - yAvg);
                sxy += (x - xAvg) * (y - yAvg);
            }

            double result = sxy / (Math.Sqrt(sxx) * Math.Sqrt(syy));
            return result;
        }
    }

    public static class ControlsExtensions
    {
        public static T InvokeLambda<T>(this Control control, Func<T> action)
        {
            return (T)control.Invoke(action);
        }
        public static void InvokeLambda(this Control control, Action action)
        {
            control.Invoke(action);
        }
        public static void InvokeLambda(this ISynchronizeInvoke control, Action action)
        {
            control.Invoke(action, null);
        }

        public static IAsyncResult BeginInvokeLambda(this Control control, Action action)
        {
            return control.BeginInvoke(action, null);
        }

        public static void BeginInvokeLambdaIfRequired(this Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                control.BeginInvoke(action, null);
            }
            else
            {
                action();
            }
        }

        public static IAsyncResult BeginInvokeLambda(this ISynchronizeInvoke control, Action action)
        {
            return control.BeginInvoke(action, null);
        }
    }
}
