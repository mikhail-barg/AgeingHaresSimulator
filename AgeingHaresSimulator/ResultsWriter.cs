using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgeingHaresSimulator.Common;
using CsvHelper;

namespace AgeingHaresSimulator
{
    internal sealed class ResultsWriter
    {
        private static readonly CsvHelper.Configuration.Configuration s_csvConfiguration = new CsvHelper.Configuration.Configuration();

        static ResultsWriter()
        {
            s_csvConfiguration.Delimiter = ";";
            s_csvConfiguration.HasHeaderRecord = true;
            s_csvConfiguration.IgnoreBlankLines = true;
            s_csvConfiguration.ReferenceHeaderPrefix = (type, name) => $"{name}.";
        }


        public static List<YearResults> ReadAll(string fileName)
        {
            using (Stream stream = File.OpenRead(fileName))
            using (StreamReader reader = new StreamReader(stream))
            using (CsvReader csv = new CsvReader(reader, s_csvConfiguration))
            {
                return csv.GetRecords<YearResults>().ToList();
            }
        }

        public static void WriteAll(string fileName, IEnumerable<YearResults> resultsList)
        {
            using (Stream stream = File.OpenWrite(fileName))
            using (StreamWriter writer = new StreamWriter(stream))
            using (CsvWriter csv = new CsvWriter(writer, s_csvConfiguration))
            {
                csv.WriteRecords(resultsList);
            }
        }
    }
}
