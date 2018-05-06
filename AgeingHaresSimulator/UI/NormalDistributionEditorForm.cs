using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace AgeingHaresSimulator.UI
{
    public partial class NormalDistributionEditorForm : Form
    {
        private const int POINTS_COUNT = 70;
        private const double SIZE_SCALE_SIGMA = 3;

        internal readonly NormalDistribution Value;
        private readonly Series m_series;

        public NormalDistributionEditorForm(NormalDistribution value)
        {
            this.Value = value.Clone();
            InitializeComponent();

            this.propertyGrid1.SelectedObject = this.Value;
            m_series = this.chart1.Series[0];
            UpdateData();
        }

        internal void UpdateData()
        {
            double stepSize = SIZE_SCALE_SIGMA * this.Value.StdDev / (POINTS_COUNT / 2);
            double minValue = this.Value.Mean - SIZE_SCALE_SIGMA * this.Value.StdDev;
            m_series.Points.Clear();
            for (int i = 0; i < POINTS_COUNT; ++i)
            {
                double x = minValue + stepSize * i;
                double y = this.Value.Transform(x);
                m_series.Points.AddXY(x, y);
            }

            Axis xAxis = chart1.ChartAreas[0].AxisX;
            xAxis.Interval = SIZE_SCALE_SIGMA * this.Value.StdDev / 5;
            xAxis.Minimum = minValue;
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            UpdateData();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
