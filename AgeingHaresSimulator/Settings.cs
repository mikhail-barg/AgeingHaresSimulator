using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgeingHaresSimulator.UI;
using Newtonsoft.Json;

namespace AgeingHaresSimulator
{
    public sealed class Settings
    {
        private static JsonSerializerSettings s_jsonSettings = new JsonSerializerSettings() {
            Formatting = Formatting.Indented,
            TypeNameHandling = TypeNameHandling.Auto,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };

        static Settings()
        {
            s_jsonSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
        }

        [JsonProperty()]
        private decimal SettingsVersion { get; set; } = 1.1m;

        [Category("1. Initial"), Description("Number of individuals in initial population")]
        public int InitialPopulationSize { get; set; } = 25;

        [Category("1. Initial"), Description("Distribution parameters for the Ageing gene value in initial population")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [Editor(typeof(NormalDistributionUiEditor), typeof(UITypeEditor))]
        public NormalDistribution AgeingGeneInitialDistribution { get; set; } = new NormalDistribution();

        [Category("1. Initial"), Description("Distribution parameters for the Cunning gene value in initial population")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [Editor(typeof(NormalDistributionUiEditor), typeof(UITypeEditor))]
        public NormalDistribution CunningGeneInitialDistribution { get; set; } = new NormalDistribution();


        [Category("2. Selection"), Description("Parameters of a logistic function transforming the individual's Survivability into a probability to survive during current year. Survivability = Max(speed, cunning)")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public LogisticFunction SurvivabilityToSurvivalProbabilityTransfromParams { get; private set; } = new LogisticFunction();


        [Category("3. Crysises"), Description("Probability of the year having a crysis. During the crysis speed is affected by the penalty, governed by CrysisPowerDistribution")]
        public double CrysisProbability { get; set; } = 0.2;

        [Category("3. Crysises"), Description("Parameters of the normal distribution producing the power of the crysis. The power is a fraction of speed penalty applied to all individuals during the year's survival phase")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [Editor(typeof(NormalDistributionUiEditor), typeof(UITypeEditor))]
        public NormalDistribution CrysisPowerDistribution { get; set; } = new NormalDistribution();


        [Category("4. Ageing"), Description("Number of years since birth, when ageing has no effect on speed")]
        public int YearsWithoutAgeingEffect { get; set; } = 1;


        [Category("5. Mating"), Description("Parameters of a (reverse)logistic function transforming population size into the probability of having an offspring at current year. The function is applied as P=(1-Y), i.e. the larget the population is, the smaller the probability of mating")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public LogisticFunction PopulationSizeToMatingProbabilityTransformParams { get; private set; } = new LogisticFunction();


        [Category("6. Sex"), Description("Probability of taking second gene from another allele [0 - 0.5]. 0 means no crossover, i.e. genes are linked and always inhereted together")]
        public double CrossoverProbability { get; set; } = 0;

        [Category("6. Sex"), Description("Probability and distribution parameters for the Ageing gene mutations")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public MutationParams AgeingGeneMutationParams { get; private set; } = new MutationParams();

        [Category("6. Sex"), Description("Probability and distribution parameters for the Cunning gene mutations")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public MutationParams CunningGeneMutationParams { get; private set; } = new MutationParams();


        [Category("7. New individual"), Description("Default speed at the moment of birth")]
        public double InitialSpeed { get; set; } = 10.0;

        /*
        [Category("7. New individual"), Description("Parameters of logistic function transforming cunning value into speed decrease penalty. The penalty is fraction of initial speed descrease")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public LogisticFunction CunningToSpeedPenaltyTransformParams { get; private set; } = new LogisticFunction();
        */

        [Category("7. New individual"), Description("Maximum value for cunning. Set to 0 to disable limit")]
        public double MaximalCunning { get; set; } = 0.0;

        [Category("7. New individual"), Description("Cunning to speed penalty linear transform coefficient. Penalty = Cunning * Coefficient")]
        public double CunningToSpeedPenaltyCoefficient { get; set; } = 0.0;

        internal double CunningToSpeedPenaltyTransform(double cunningValue)
        {
            //double penalty = this.CunningToSpeedPenaltyTransformParams.Evaluate(cunningValue);
            double penalty = this.CunningToSpeedPenaltyCoefficient * cunningValue;
            return penalty;
        }

        internal static Settings LoadFromFile(string fileName)
        {
            string json = File.ReadAllText(fileName);
            Settings settings = JsonConvert.DeserializeObject<Settings>(json, s_jsonSettings);
            return settings;
        }

        internal void SaveToFile(string fileName)
        {
            string json = JsonConvert.SerializeObject(this, s_jsonSettings);
            File.WriteAllText(fileName, json);
        }

        internal double SurvivabilityToSurvivalProbabilityTransfrom(double survivability)
        {
            double probability = SurvivabilityToSurvivalProbabilityTransfromParams.Evaluate(survivability);
            return probability;
        }

        internal double PopulationSizeToMatingProbabilityTransform(int populationSize)
        {
            double probability = 1 - PopulationSizeToMatingProbabilityTransformParams.Evaluate(populationSize);
            return probability;
        }
    }

    public sealed class MutationParams
    {
        public double MutationProbability { get; set; } = 0.01;

        [TypeConverter(typeof(ExpandableObjectConverter))]
        [Editor(typeof(NormalDistributionUiEditor), typeof(UITypeEditor))]
        public NormalDistribution Distribution { get; set; } = new NormalDistribution();

        public override string ToString()
        {
            return $"P={MutationProbability}, {Distribution.ToString()}";
        }
    }

    public sealed class NormalDistribution
    {
        private static readonly double SQRT_2PI = Math.Sqrt(2 * Math.PI);

        public double Mean { get; set; } = 0.0;
        public double StdDev { get; set; } = 1.0;

        public override string ToString()
        {
            return $"N{{{Mean}, {StdDev}}}";
        }

        public NormalDistribution Clone()
        {
            return (NormalDistribution)this.MemberwiseClone();
        }

        public double Sample(Random random)
        {
            //see http://stackoverflow.com/a/218600
            // a Box-Muller transform
            double u1 = random.NextDouble(); //these are uniform(0,1) random doubles
            double u2 = random.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            double randNormal = this.Mean + this.StdDev * randStdNormal; //random normal(mean,stdDev^2)

            return randNormal;
        }

        internal double Transform(double x)
        {
            //https://en.wikipedia.org/wiki/Normal_distribution
            /*
             
             Y = 1 / Sqrt(2 * Pi * stdDev^2) * exp(- (x - Mean)^2 / (2*stdDev^2)
             
             */

            double y = 1.0 / (SQRT_2PI * this.StdDev) * Math.Exp( - (x - this.Mean) * (x - this.Mean) / (2 * this.StdDev * this.StdDev));
            return y;
        }
    }

    public sealed class LogisticFunction
    {
        [Description("The x-value of the sigmoid's midpoint")]
        public double X0 { get; set; } = 0;

        [Description("The steepness of the curve")]
        public double K { get; set; } = 1;

        public override string ToString()
        {
            return $"Logistic{{x0={X0}, k={K}}}";
        }

        public double Evaluate(double x)
        {
            return 1 / (1 + Math.Exp(- this.K * (x - this.X0)));
        }
    }
}
