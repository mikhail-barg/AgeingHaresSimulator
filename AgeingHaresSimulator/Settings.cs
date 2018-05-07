using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgeingHaresSimulator.Common;
using AgeingHaresSimulator.Common.UI;
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
        [Editor(typeof(LogisticFunctionUiEditor), typeof(UITypeEditor))]
        public LogisticFunction SurvivabilityToSurvivalProbabilityTransfromParams { get; set; } = new LogisticFunction();


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
        [Editor(typeof(LogisticFunctionUiEditor), typeof(UITypeEditor))]
        public LogisticFunction PopulationSizeToMatingProbabilityTransformParams { get; set; } = new LogisticFunction();


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
        [Editor(typeof(LogisticFunctionUiEditor), typeof(UITypeEditor))]
        public LogisticFunction CunningToSpeedPenaltyTransformParams { get; set; } = new LogisticFunction();
        */

        [Category("7. New individual"), Description("Maximum value for cunning")]
        public double MaximalCunning { get; set; } = 7.0;

        [Category("7. New individual"), Description("Cunning to speed penalty linear transform value at MaximalCunning. Penalty = Cunning / MaximalCunning * CunningToSpeedPenaltyMax")]
        public double CunningToSpeedPenaltyMax { get; set; } = 2.0;

        internal double CunningToSpeedPenaltyTransform(double cunningValue)
        {
            //double penalty = this.CunningToSpeedPenaltyTransformParams.Evaluate(cunningValue);
            double penalty = cunningValue / this.MaximalCunning * CunningToSpeedPenaltyMax;
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
}
