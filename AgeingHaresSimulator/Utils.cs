using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgeingHaresSimulator
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
