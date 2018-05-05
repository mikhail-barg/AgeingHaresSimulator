using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeingHaresSimulator
{
    internal sealed class Model
    {
        private readonly Random m_random = new Random();
        private readonly Settings m_settings;
        private readonly Population m_population;

        internal int Year => m_population.Year;
        internal int PopulationSize => m_population.Size;
        internal double LastCrysisPower { get; private set; }
        internal double RateOfOrigination => m_population.RateOfOrigination;
        internal double MortalityRate => m_population.MortalityRate;


        internal Model(Settings settings)
        {
            this.m_settings = settings;
            this.m_population = new Population(m_random, m_settings);
            this.LastCrysisPower = 0.0;
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
            this.LastCrysisPower = crysisPower;
            this.m_population.NextYear(crysisPower, m_random, m_settings);
        }

        internal Stats GetAgeStats()
        {
            return new Stats(m_population.Individuals.Select(item => (double)item.age));
        }

        internal Stats GetAgeingSpeedStats()
        {
            return new Stats(m_population.Individuals.Select(item => (double)item.ageingSpeed));
        }

        internal Stats GetCunningStats()
        {
            return new Stats(m_population.Individuals.Select(item => (double)item.cunning));
        }

        internal Stats GetSurvivabilityStats()
        {
            return new Stats(m_population.SurvivabilityList);
        }
    }
}
