﻿using System;
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
        internal Stats SurvivabilityStats = new Stats(new List<double>());
        internal Stats AgeAtDeathStats = new Stats(new List<double>());
        internal Stats2D SurvivabilityByAgeStats = new Stats2D(0, 0, 0, null);
        internal Stats2D CunningHistogramm = new Stats2D(0, 0, 0, null);

        private List<Species> m_speciesList;

        
        internal Population(Random random, Settings settings)
        {
            m_speciesList = new List<Species>(settings.InitialPopulationSize);
            for (int i = 0; i < settings.InitialPopulationSize; ++i)
            {
                bool allowAgeingGene = i >= settings.InitialPopulationFractionWithoutAgeingGene * settings.InitialPopulationSize;
                m_speciesList.Add(Species.CreateInitial(random, settings, allowAgeingGene));
            }
        }

        internal void NextYear(double crysisPower, Random random, Settings settings)
        {
            ++Year;
            Selection(crysisPower, random, settings);
            Ageing(settings);
            Breeding(random, settings, crysisPower);
        }

        private void Selection(double crysisPower, Random random, Settings settings)
        {
            List<Tuple<double, double>> survivabilityByAge =  new List<Tuple<double, double>>();

            List<Species> survivors = new List<Species>(this.m_speciesList.Count);
            List<Species> victims = new List<Species>(this.m_speciesList.Count);
            int maxAge = 0;
            foreach (Species species in this.m_speciesList)
            {
                double survivability;
                double survivalProbability = species.GetSurvivalProbability(crysisPower, settings.SurvivabilityToSurvivalProbabilityTransfrom, out survivability);
                survivabilityByAge.Add(Tuple.Create((double)species.age, survivability));
                if (random.ProbCheck(survivalProbability))
                {
                    survivors.Add(species);
                }
                else
                {
                    victims.Add(species);
                }
                if (species.age > maxAge)
                {
                    maxAge = species.age;
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
            this.SurvivabilityStats = new Stats(survivabilityByAge.Select(item => item.Item2));
            this.AgeAtDeathStats = new Stats(victims.Select(item => (double)item.age));
            this.SurvivabilityByAgeStats = new Stats2D(0, maxAge, 1, survivabilityByAge);
        }

        private void Ageing(Settings settings)
        {
            foreach (Species species in this.m_speciesList)
            {
                species.NextYear(settings.YearsWithoutAgeingEffect);
            }
        }

        private static double SimilarityDistanceSquared(Species species1, Species species2, double maxCunning, double maxAgeingSpeed)
        {
            if (maxCunning == 0)
            {
                maxCunning = 1;
            }
            if (maxAgeingSpeed == 0)
            {
                maxAgeingSpeed = 1;
            }
            double cunningDiff = (species1.cunning - species2.cunning) / maxCunning;
            double speedDiff = (species1.ageingSpeed - species2.ageingSpeed) / maxAgeingSpeed;

            return cunningDiff * cunningDiff + speedDiff * speedDiff; 
        }

        private void Breeding(Random random, Settings settings, double crysisPower)
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

            List<Species> offsprings = new List<Species>();
            if (settings.SexualReproduction)
            {
                if (parents.Count > 1)
                {
                    switch (settings.MatingStrategy)
                    {
                    case Settings.MatingStrategyType.Random:
                        random.Shuffle(parents);
                        for (int i = 0; i < parents.Count; i += 2)
                        {
                            Species parent1 = parents[i];
                            int j = i + 1;
                            if (j >= parents.Count)
                            {
                                j = 0;
                            }
                            Species parent2 = parents[j];

                            Species offspring = Species.CreateOffspringSex(parent1, parent2, random, settings);
                            offsprings.Add(offspring);
                        }
                        break;

                    case Settings.MatingStrategyType.PositiveAssortion:
                        parents = parents.OrderByDescending(item => item.GetSurvivability(crysisPower)).ToList();   //descending because we'll be taking last from list as it's much faster
                        {
                            double maxCunning = parents.Max(item => item.cunning);
                            double maxAgeingSpeed = parents.Max(item => item.ageingSpeed);
                            while (parents.Count > 2)
                            {
                                Species parent1 = parents[parents.Count - 1];
                                parents.RemoveAt(parents.Count - 1);

                                int bestPartnerIndex = 0;
                                double bestPartnerDistance = SimilarityDistanceSquared(parent1, parents[bestPartnerIndex], maxCunning, maxAgeingSpeed);
                                for (int i = 1; i < parents.Count; ++i)
                                {
                                    double distance = SimilarityDistanceSquared(parent1, parents[i], maxCunning, maxAgeingSpeed);
                                    if (distance < bestPartnerDistance)
                                    {
                                        bestPartnerDistance = distance;
                                        bestPartnerIndex = i;
                                    }
                                }

                                Species parent2 = parents[bestPartnerIndex];
                                parents.RemoveAt(bestPartnerIndex);
                                Species offspring = Species.CreateOffspringSex(parent1, parent2, random, settings);
                                offsprings.Add(offspring);
                            }
                        }
                        break;

                    default:
                        throw new ApplicationException("Unexpected strategy " + settings.MatingStrategy);
                    }
                }
            }
            else
            {
                foreach (Species parent in parents)
                {
                    Species offspring = Species.CreateOffspringReplication(parent, random, settings);
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

            this.CunningHistogramm = Stats2D.CreateForCounts(0, settings.MaximalCunning, 0.25, m_speciesList.Select(item => item.cunning));
        }
    }
}
