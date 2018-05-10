using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgeingHaresSimulator.Common;

namespace AgeingHaresSimulator
{
    internal struct Chromosome
    {
        public double cunningGene { get; set; }
        public double ageingGene { get; set; }

        internal void ApplyMutations(Random random, Settings settings)
        {
            if (random.ProbCheck(settings.AgeingGeneMutationParams.MutationProbability))
            {
                this.ageingGene += settings.AgeingGeneMutationParams.Distribution.Sample(random);
                if (this.ageingGene < 0)
                {
                    this.ageingGene = 0;
                }
                if (this.ageingGene > 1)
                {
                    this.ageingGene = 1;
                }
            }

            if (random.ProbCheck(settings.CunningGeneMutationParams.MutationProbability))
            {
                this.cunningGene += settings.CunningGeneMutationParams.Distribution.Sample(random);
                if (this.cunningGene < 0)
                {
                    this.cunningGene = 0;
                }
                if (this.cunningGene > settings.MaximalCunning)
                {
                    this.cunningGene = settings.MaximalCunning;
                }
            }
        }

        internal static Chromosome CreateInitial(Random random, Settings settings, bool allowAgeingGene)
        {
            Chromosome chromosome = new Chromosome();
            if (allowAgeingGene)
            {
                chromosome.ageingGene = settings.AgeingGeneInitialDistribution.Sample(random);
            }
            else
            {
                chromosome.ageingGene = 0;
            }
            if (chromosome.ageingGene < 0)
            {
                chromosome.ageingGene = 0;
            }
            if (chromosome.ageingGene > 1)
            {
                chromosome.ageingGene = 1;
            }
            chromosome.cunningGene = settings.CunningGeneInitialDistribution.Sample(random);
            if (chromosome.cunningGene < 0)
            {
                chromosome.cunningGene = 0;
            }
            if (chromosome.cunningGene > settings.MaximalCunning)
            {
                chromosome.cunningGene = settings.MaximalCunning;
            }
            return chromosome;
        }
    }

    internal sealed class Species
    {
        internal Species(Chromosome chromosome1, Chromosome chromosome2, double initialSpeed, Func<double, double> cunningToSpeedPenaltyTransform)
        {
            this.chromosome1 = chromosome1;
            this.chromosome2 = chromosome2;

            this.cunning = (chromosome1.cunningGene + chromosome2.cunningGene) / 2;
            this.ageingSpeed = (chromosome1.ageingGene + chromosome2.ageingGene) / 2;
            double speedPenalty = cunningToSpeedPenaltyTransform(this.cunning);
            /*
            if (speedPenalty < 0 || speedPenalty > 1)
            {
                throw new ArgumentException($"Bad transform of cunning to speed penalty, cunning was {cunning}, got penalty {speedPenalty}");
            }
            this.speed = initialSpeed * (1 - speedPenalty);
            */
            this.speed = initialSpeed - speedPenalty;
            if (this.speed < 0)
            {
                this.speed = 0;
            }
        }

        public int age { get; private set; } = 0;
        public double speed { get; private set; }
        public double cunning { get; private set; }

        [Description("[0..1] - speed decrease per year (in fraction of current speed)")]
        public double ageingSpeed { get; private set; }

        public Chromosome chromosome1 { get; private set; }
        public Chromosome chromosome2 { get; private set; }


        internal double GetSurvivability(double crysisPower)
        {
            double currentSpeed = this.speed;
            currentSpeed -= currentSpeed * crysisPower;
            double result = Math.Max(currentSpeed, this.cunning);
            return result;
        }

        internal double GetSurvivalProbability(double crysisPower, Func<double, double> survivabilityToSurvivalProbabilityTransfrom, out double survivability)
        {
            survivability = GetSurvivability(crysisPower);
            double survivalProbability = survivabilityToSurvivalProbabilityTransfrom(survivability);
            if (survivalProbability < 0 || survivalProbability > 1)
            {
                throw new ArgumentException($"Bad transform of survivability to probability, survivability was {survivability}, got probability {survivalProbability}");
            }
            return survivalProbability;
        }

        public void NextYear(int yearsWithoutAgeing)
        {
            ++age;
            if (age > yearsWithoutAgeing)
            {
                this.speed -= this.speed * this.ageingSpeed;
            }
        }

        public Chromosome SelectChromosomeForOffspring(Random random, Settings settings)
        {
            if (settings.SexualReproduction)
            {
                Chromosome result, other;
                if (random.ProbCheck(0.5))
                {
                    result = this.chromosome1;
                    other = this.chromosome2;
                }
                else
                {
                    result = this.chromosome2;
                    other = this.chromosome1;
                }

                if (random.ProbCheck(settings.CrossoverProbability))
                {
                    result.ageingGene = other.ageingGene;
                }

                return result;
            }
            else
            {
                return this.chromosome1;
            }
        }

        internal static Species CreateOffspringSex(Species parent1, Species parent2, Random random, Settings settings)
        {
            Chromosome chromosome1 = parent1.SelectChromosomeForOffspring(random, settings);
            Chromosome chromosome2 = parent2.SelectChromosomeForOffspring(random, settings);

            chromosome1.ApplyMutations(random, settings);
            chromosome2.ApplyMutations(random, settings);

            Species result = new Species(chromosome1, chromosome2, settings.InitialSpeed, settings.CunningToSpeedPenaltyTransform);

            return result;
        }

        internal static Species CreateOffspringReplication(Species parent, Random random, Settings settings)
        {
            Chromosome chromosome1 = parent.SelectChromosomeForOffspring(random, settings);
            chromosome1.ApplyMutations(random, settings);
            

            Species result = new Species(chromosome1, chromosome1, settings.InitialSpeed, settings.CunningToSpeedPenaltyTransform);

            return result;
        }

        internal static Species CreateInitial(Random random, Settings settings, bool allowAgeingGene)
        {
            if (settings.SexualReproduction)
            {
                Chromosome chromosome1 = Chromosome.CreateInitial(random, settings, allowAgeingGene);
                Chromosome chromosome2 = Chromosome.CreateInitial(random, settings, allowAgeingGene);
                Species result = new Species(chromosome1, chromosome2, settings.InitialSpeed, settings.CunningToSpeedPenaltyTransform);
                return result;
            }
            else
            {
                Chromosome chromosome1 = Chromosome.CreateInitial(random, settings, allowAgeingGene);
                Species result = new Species(chromosome1, chromosome1, settings.InitialSpeed, settings.CunningToSpeedPenaltyTransform);
                return result;
            }
        }
    }


}
