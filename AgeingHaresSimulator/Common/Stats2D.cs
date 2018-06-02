using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeingHaresSimulator.Common
{
    public sealed class Stats2D
    {
        public sealed class Bucket
        {
            public int index;
            public double minXValue;
            public double maxXValue;
            public double centerXValue;
            public Stats stats;

            //Used by CsvHelper
            public Bucket() { }

            public Bucket(int index, double minXValue, double maxXValue, double centerXValue, Stats stats)
            {
                this.index = index;
                this.minXValue = minXValue;
                this.maxXValue = maxXValue;
                this.centerXValue = centerXValue;
                this.stats = stats;
            }
        }

        public double xMin;
        public double xMax;
        public double xBucketSize;
        public List<Bucket> yBuckets;

        //Used by CsvHelper
        public Stats2D() { }

        public Stats2D(double xMin, double xMax, double xBucketSize, IEnumerable<Tuple<double, double>> values)
        {
            this.xMin = xMin;
            this.xMax = xMax;
            this.xBucketSize = xBucketSize;

            if (xBucketSize == 0 || values == null)
            {
                yBuckets = new List<Bucket>();
                return;
            }

            int bucketsCount = (int)Math.Ceiling((xMax - xMin) / xBucketSize);

            if (bucketsCount == 0)
            {
                bucketsCount = 1;
            }

            List<List<double>> bucketsData = new List<List<double>>();
            for (int i = 0; i < bucketsCount; ++i)
            {
                bucketsData.Add(new List<double>());
            }

            foreach (Tuple<double, double> xyPair in values)
            {
                int index = (int)((xyPair.Item1 - xMin) / xBucketSize);
                if (index < 0)
                {
                    index = 0;
                }
                if (index >= bucketsCount)
                {
                    index = bucketsCount - 1;
                }
                bucketsData[index].Add(xyPair.Item2);
            }

            yBuckets = bucketsData.Select(item => item.ToStats())
                .Select((stats, index) => new Bucket(index, index * xBucketSize + xMin, (index + 1) * xBucketSize + xMin, index * xBucketSize + xBucketSize / 2 + xMin, stats))
                .ToList();
        }
    }
}
