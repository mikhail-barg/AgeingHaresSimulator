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

        private readonly Dictionary<string, Series> m_allSeries;
        private readonly List<YearResults> m_currentResults = new List<YearResults>();

        public MainForm()
        {
            InitializeComponent();

            this.Text += " v" + Assembly.GetEntryAssembly().GetName().Version;

            this.propertyGrid1.SelectedObject = new Settings();

            m_allSeries = chart1.Series.Union(chart2.Series).ToDictionary(item => item.Name);
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
        private const int SPEED_COUNT_YEAR = 50;

        private void RunModel(CancellationToken token)
        {
            Settings settings = (Settings)this.propertyGrid1.SelectedObject;
            Model model = new Model(settings);
            m_currentResults.Clear();

            DateTime startTime = DateTime.Now;
            double currentSpeed = 0.0;


            this.InvokeLambda(() => DisplayResults(model, currentSpeed, false));

            while (!token.IsCancellationRequested)
            {
                model.NextYear();
                if (model.Year % SPEED_COUNT_YEAR == 0)
                {
                    DateTime now = DateTime.Now;
                    currentSpeed = SPEED_COUNT_YEAR / (now - startTime).TotalSeconds;
                    startTime = now;
                }

                if (model.Year % DISPLAY_YEAR == 0 || model.PopulationSize == 0)
                {
                    this.InvokeLambda(() => DisplayResults(model, currentSpeed, token.IsCancellationRequested));
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

        private void DisplayResults(Model model, double currentSpeed, bool isFinal)
        {
            {
                YearResults results = model.GetYearResults();
                m_currentResults.Add(results);
            }

            if (m_currentResults.Count < model.settings.DisplayWaitPeriod && !isFinal)
            {
                return;
            }

            this.speedToolStripTextBox.Text = currentSpeed.ToString("N2") + " years/s";

            foreach (YearResults results in m_currentResults)
            {
                results.ChartData(m_allSeries);
            }
            m_currentResults.Clear();


            if (model.settings.MaximumYearsToDisplay > 0)
            {
                foreach (Series series in m_allSeries.Values)
                {
                    while (series.Points.Count > model.settings.MaximumYearsToDisplay)
                    {
                        series.Points.RemoveAt(0);
                    }
                }
                this.chart1.ResetAutoValues();
                this.chart2.ResetAutoValues();
            }

            
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
