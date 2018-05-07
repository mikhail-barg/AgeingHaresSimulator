using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgeingHaresSimulator.Common;

namespace AgeingHaresSimulator
{
    internal sealed class Population
    {
        public int Year { get; private set; } = 0;
        public int Size => m_speciesList.Count;
        internal IEnumerable<Species> Individuals => m_speciesList;
        internal double RateOfOrigination { get; private set; }
        internal double MortalityRate { get; private set; }
        internal List<double> SurvivabilityList = new List<double>();

        private List<Species> m_speciesList;

        
        internal Population(Random random, Settings settings)
        {
            m_speciesList = new List<Species>(settings.InitialPopulationSize);
            for (int i = 0; i < settings.InitialPopulationSize; ++i)
            {
                m_speciesList.Add(Species.CreateInitial(random, settings));
            }
        }

        internal void NextYear(double crysisPower, Random random, Settings settings)
        {
            ++Year;
            Selection(crysisPower, random, settings);
            Ageing(settings);
            Breeding(random, settings);
        }

        private void Selection(double crysisPower, Random random, Settings settings)
        {
            this.SurvivabilityList = new List<double>();
            List<Species> survivors = new List<Species>(this.m_speciesList.Count);
            foreach (Species species in this.m_speciesList)
            {
                double survivability;
                double survivalProbability = species.GetSurvivalProbability(crysisPower, settings.SurvivabilityToSurvivalProbabilityTransfrom, out survivability);
                this.SurvivabilityList.Add(survivability);
                if (random.ProbCheck(survivalProbability))
                {
                    survivors.Add(species);
                }
            }
            if (m_speciesList.Count > 0)
            {
                this.MortalityRate = 1.0 * (m_speciesList.Count - survivors.Count) / m_speciesList.Count;
            }
            else
            {
                this.MortalityRate = 0;
            }
            this.m_speciesList = survivors;
        }

        private void Ageing(Settings settings)
        {
            foreach (Species species in this.m_speciesList)
            {
                species.NextYear(settings.YearsWithoutAgeingEffect);
            }
        }

        private void Breeding(Random random, Settings settings)
        {
            List<Species> parents = new List<Species>(this.m_speciesList.Count);
            double matingProbability = settings.PopulationSizeToMatingProbabilityTransform(this.m_speciesList.Count);
            foreach (Species species in this.m_speciesList)
            {
                if (random.ProbCheck(matingProbability))
                {
                    parents.Add(species);
                }
            }
            random.Shuffle(parents);

            List<Species> offsprings = new List<Species>();
            if (parents.Count > 1)
            {
                for (int i = 0; i < parents.Count; i += 2)
                {
                    Species parent1 = parents[i];
                    int j = i + 1;
                    if (j >= parents.Count)
                    {
                        j = 0;
                    }
                    Species parent2 = parents[j];

                    Species offspring = Species.CreateOffspring(parent1, parent2, random, settings);
                    offsprings.Add(offspring);
                }
            }

            if (m_speciesList.Count > 0)
            {
                this.RateOfOrigination = 1.0 * (offsprings.Count) / m_speciesList.Count;
            }
            else
            {
                this.RateOfOrigination = 0;
            }

            this.m_speciesList.AddRange(offsprings);
        }
    }
}
