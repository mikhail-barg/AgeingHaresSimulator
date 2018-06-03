using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using AgeingHaresSimulator.Common;

namespace AgeingHaresSimulator
{
    public sealed class YearResults
    {
        public int Year { get; set; }

        public double AgeingCunningCorrelation { get; set; }
        public int PopulationSize { get; set; }
        public double CrysisPower { get; set; }
        public Stats AgeStats { get; set; }
        public Stats AgeingSpeedStats { get; set; }
        public Stats CunningStats { get; set; }
        public double MortalityRate { get; set; }
        public double RateOfOrigination { get; set; }
        public Stats SurvivabilityStats { get; set; }
        public Stats AgeAtDeathStats { get; set; }
        public Stats2D SurvivabilityByAge { get; set; }
        public Stats2D CunningHistogramm { get; set; }

        internal void ChartData(Dictionary<string, Series> series, bool isCurrentlyLast)
        {
            series["Ageing Cunning correlation"].Points.AddXY(Year, AgeingCunningCorrelation);
            series["Population size"].Points.AddXY(Year, PopulationSize);
            series["Crysis power"].Points.AddXY(Year, CrysisPower);

            series["Average age"].Points.AddXY(Year, AgeStats.avgValue);
            series["Avg. age at death"].Points.AddXY(Year, AgeAtDeathStats.avgValue);

            series["Ageing speed (Average)"].Points.AddXY(Year, AgeingSpeedStats.avgValue);
            series["Cunning (Average)"].Points.AddXY(Year, CunningStats.avgValue);

            series["Mortality rate"].Points.AddXY(Year, MortalityRate);
            series["Rate of origination"].Points.AddXY(Year, RateOfOrigination);

            series["Survivability"].Points.AddXY(Year, SurvivabilityStats.avgValue);

            if (isCurrentlyLast)
            {
                {
                    Series avgSeries = series["Avg. Survivability by Age"];
                    Series statsSeries = series["Survivability by Age Stats"];
                    Series countSeries = series["Individuals count by Age"];
                    avgSeries.Points.Clear();
                    statsSeries.Points.Clear();
                    countSeries.Points.Clear();
                    foreach (Stats2D.Bucket bucket in SurvivabilityByAge.yBuckets)
                    {
                        if (bucket.stats.count > 0)
                        {
                            avgSeries.Points.AddXY(bucket.minXValue, bucket.stats.avgValue);
                            //statsSeries.Points.AddXY(bucket.minXValue, bucket.stats.maxValue, bucket.stats.minValue, bucket.stats.q1Value, bucket.stats.q3Value);
                            countSeries.Points.AddXY(bucket.minXValue, bucket.stats.count);
                        }
                    }
                }

                {
                    Series cunningHystogramm = series["Cunning histogramm"];
                    cunningHystogramm.Points.Clear();
                    foreach (Stats2D.Bucket bucket in CunningHistogramm.yBuckets)
                    {
                        if (bucket.stats.count > 0)
                        {
                            cunningHystogramm.Points.AddXY(bucket.minXValue, bucket.stats.count);
                        }
                    }
                }
            }
        }
    }
}
