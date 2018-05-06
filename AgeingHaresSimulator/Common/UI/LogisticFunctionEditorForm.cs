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

namespace AgeingHaresSimulator.Common.UI
{
    public partial class LogisticFunctionEditorForm : Form
    {
        private const int POINTS_COUNT = 70;

        internal readonly LogisticFunction Value;
        private readonly Series m_series;

        public LogisticFunctionEditorForm(LogisticFunction value)
        {
            this.Value = value.Clone();
            InitializeComponent();

            this.propertyGrid1.SelectedObject = this.Value;
            m_series = this.chart1.Series[0];
            UpdateData();
        }

        internal void UpdateData()
        {
            double spreadSize = 7 / this.Value.K;
            double stepSize = spreadSize / (POINTS_COUNT / 2);
            double minValue = this.Value.X0 - spreadSize;
            m_series.Points.Clear();
            for (int i = 0; i < POINTS_COUNT; ++i)
            {
                double x = minValue + stepSize * i;
                double y = this.Value.Evaluate(x);
                m_series.Points.AddXY(x, y);
            }

            Axis xAxis = chart1.ChartAreas[0].AxisX;
            xAxis.Interval = spreadSize / 5;
            xAxis.Minimum = minValue;
            chart1.ChartAreas[0].CursorX.Interval = xAxis.Interval / 5;
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
