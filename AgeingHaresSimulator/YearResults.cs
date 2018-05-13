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

        internal void ChartData(Dictionary<string, Series> charts)
        {
            charts["Ageing Cunning correlation"].Points.AddXY(Year, AgeingCunningCorrelation);
            charts["Population size"].Points.AddXY(Year, PopulationSize);
            charts["Crysis power"].Points.AddXY(Year, CrysisPower);

            charts["Average age"].Points.AddXY(Year, AgeStats.avgValue);
            charts["Ageing speed (Average)"].Points.AddXY(Year, AgeingSpeedStats.avgValue);
            charts["Cunning (Average)"].Points.AddXY(Year, CunningStats.avgValue);

            charts["Mortality rate"].Points.AddXY(Year, MortalityRate);
            charts["Rate of origination"].Points.AddXY(Year, RateOfOrigination);

            charts["Survivability"].Points.AddXY(Year, SurvivabilityStats.avgValue);
        }
    }
}
