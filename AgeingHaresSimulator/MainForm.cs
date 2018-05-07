using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using AgeingHaresSimulator.Common;

namespace AgeingHaresSimulator
{
    public partial class MainForm : Form
    {
        private CancellationTokenSource m_tokenSource;
        private Task m_task;

        private readonly Series m_populationSizeSeries;
        private readonly Series m_averageAgeSeries;

        private readonly Series m_ageingSpeedAvgSeries;
        private readonly Series m_cunningAvgSeries;
        private readonly Series m_ageingSpeedMedianSeries;
        private readonly Series m_ageingSpeedQ1Series;
        private readonly Series m_ageingSpeedQ3Series;

        private readonly Series m_crysisPowerSeries;

        private readonly Series m_mortalityRateSeries;
        private readonly Series m_originationRateSeries;

        private readonly Series m_survivabilitySeries;

        private readonly Series m_ageingCunningCorrelationSeries;

        public MainForm()
        {
            InitializeComponent();

            this.Text += " v" + Assembly.GetEntryAssembly().GetName().Version;

            this.propertyGrid1.SelectedObject = new Settings();

            this.m_populationSizeSeries = this.chart1.Series["Population size"];
            this.m_averageAgeSeries = this.chart1.Series["Average age"];
            this.m_ageingSpeedAvgSeries = this.chart1.Series["Ageing speed (Average)"];
            this.m_ageingSpeedMedianSeries = this.chart1.Series["Ageing speed (Median)"];
            this.m_ageingSpeedQ1Series = this.chart1.Series["Ageing speed (Q1)"];
            this.m_ageingSpeedQ3Series = this.chart1.Series["Ageing speed (Q3)"];

            this.m_crysisPowerSeries = this.chart1.Series["Crysis power"];

            this.m_originationRateSeries = this.chart1.Series["Rate of origination"];
            this.m_mortalityRateSeries = this.chart1.Series["Mortality rate"];
            this.m_cunningAvgSeries = this.chart1.Series["Cunning (Average)"];
            this.m_survivabilitySeries = this.chart1.Series["Survivability"];

            this.m_ageingCunningCorrelationSeries = this.chart2.Series["Ageing Cunning correlation"];
        }

        private void openSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Settings files (*.json)|*.json|All files (*.*)|*.*";
                dialog.CheckFileExists = true;
                dialog.Multiselect = false;
                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    Settings settings = Settings.LoadFromFile(dialog.FileName);
                    this.propertyGrid1.SelectedObject = settings;
                }
                catch (Exception ex)
                {
                    ExceptionViewForm.Show(ex);
                }
            }
        }

        private void saveSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "Settings files (*.json)|*.json|All files (*.*)|*.*";
                dialog.OverwritePrompt = true;
                dialog.CheckPathExists = true;
                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    Settings settings = (Settings)this.propertyGrid1.SelectedObject;
                    settings.SaveToFile(dialog.FileName);
                }
                catch (Exception ex)
                {
                    ExceptionViewForm.Show(ex);
                }
            }
        }

        private const int DISPLAY_YEAR = 1;

        private void RunModel(CancellationToken token)
        {
            Settings settings = (Settings)this.propertyGrid1.SelectedObject;
            Model model = new Model(settings);
            this.InvokeLambda(() => DisplayResults(model));

            while (!token.IsCancellationRequested)
            {
                model.NextYear();
                if (model.Year % DISPLAY_YEAR == 0 || model.PopulationSize == 0)
                {
                    this.InvokeLambda(() => DisplayResults(model));
                }
                
                if (model.PopulationSize == 0)
                {
                    break;
                }
            }

            this.InvokeLambda(() => {
                stopToolStripMenuItem.Enabled = false;
                startToolStripMenuItem.Enabled = true;
                m_tokenSource = null;
                m_task = null;
            });
        }

        private void DisplayResults(Model model)
        {
            double correlation = model.GetAgeingCunningCorrelation();
            this.m_ageingCunningCorrelationSeries.Points.AddXY(model.Year, correlation);

            this.m_populationSizeSeries.Points.AddXY(model.Year, model.PopulationSize);
            this.m_crysisPowerSeries.Points.AddXY(model.Year, model.LastCrysisPower);
            Stats ageStats = model.GetAgeStats();
            this.m_averageAgeSeries.Points.AddXY(model.Year, ageStats.avgValue);
            Stats ageingSpeedStats = model.GetAgeingSpeedStats();
            Stats cunningStats = model.GetCunningStats();
            this.m_ageingSpeedAvgSeries.Points.AddXY(model.Year, ageingSpeedStats.avgValue);
            this.m_ageingSpeedMedianSeries.Points.AddXY(model.Year, ageingSpeedStats.medianValue);
            this.m_ageingSpeedQ1Series.Points.AddXY(model.Year, ageingSpeedStats.q1Value);
            this.m_ageingSpeedQ3Series.Points.AddXY(model.Year, ageingSpeedStats.q3Value);
            this.m_cunningAvgSeries.Points.AddXY(model.Year, cunningStats.avgValue);

            this.m_mortalityRateSeries.Points.AddXY(model.Year, model.MortalityRate);
            this.m_originationRateSeries.Points.AddXY(model.Year, model.RateOfOrigination);

            this.m_survivabilitySeries.Points.AddXY(model.Year, model.GetSurvivabilityStats().avgValue);

            this.chart1.Update();
            this.chart2.Update();

        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.startToolStripMenuItem.Enabled = false;
            this.stopToolStripMenuItem.Enabled = true;

            foreach (Series series in this.chart1.Series)
            {
                series.Points.Clear();
            }

            foreach (Series series in this.chart2.Series)
            {
                series.Points.Clear();
            }

            m_tokenSource = new CancellationTokenSource();
            CancellationToken token = m_tokenSource.Token;

            m_task = Task.Run(() => RunModel(token), token);
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_tokenSource.Cancel();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Task task = this.m_task;
            if (m_tokenSource != null)
            {
                e.Cancel = true;
                m_tokenSource.Cancel();
                task.ContinueWith(t => this.InvokeLambda(Close));
            }
        }
    }
}
