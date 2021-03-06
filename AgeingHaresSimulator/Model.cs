﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgeingHaresSimulator.Common;

namespace AgeingHaresSimulator
{
    internal sealed class Model
    {
        private readonly Random m_random = new Random();
        private readonly Settings m_settings;
        private readonly Population m_population;

        internal int Year => m_population.Year;
        internal int PopulationSize => m_population.Size;
        internal double CrysisPower { get; private set; }
        internal double RateOfOrigination => m_population.RateOfOrigination;
        internal double MortalityRate => m_population.MortalityRate;

        internal Settings settings => m_settings;

        internal Model(Settings settings)
        {
            this.m_settings = settings;
            this.m_population = new Population(m_random, m_settings);
            this.CrysisPower = 0.0;
        }

        internal void NextYear()
        {
            bool isCrysis = m_random.ProbCheck(m_settings.CrysisProbability);
            double crysisPower = 0;
            if (isCrysis)
            {
                crysisPower = m_settings.CrysisPowerDistribution.Sample(m_random);
                if (crysisPower < 0)
                {
                    crysisPower = 0;
                }
                if (crysisPower > 1)
                {
                    crysisPower = 1;
                }
            }
            this.CrysisPower = crysisPower;
            this.m_population.NextYear(crysisPower, m_random, m_settings);
        }

        
        internal YearResults GetYearResults()
        {
            YearResults results = new YearResults() {
                Year = this.Year,
                AgeingCunningCorrelation = Utils.CalculateCorrelation(m_population.Individuals, item => item.ageingSpeed, item => item.cunning),
                PopulationSize = this.PopulationSize,
                CrysisPower = this.CrysisPower,
                AgeingSpeedStats = new Stats(m_population.Individuals.Select(item => item.ageingSpeed)),
                AgeStats = new Stats(m_population.Individuals.Select(item => (double)item.age)),
                AgeAtDeathStats = m_population.AgeAtDeathStats,
                CunningStats = new Stats(m_population.Individuals.Select(item => item.cunning)),
                MortalityRate = this.MortalityRate,
                RateOfOrigination = this.RateOfOrigination,
                SurvivabilityStats = m_population.SurvivabilityStats,
                SurvivabilityByAge = m_population.SurvivabilityByAgeStats,
                CunningHistogramm = m_population.CunningHistogramm
            };

            return results;
        }
    }
}
