namespace AgeingHaresSimulator
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea10 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea11 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea12 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series26 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series27 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series28 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series29 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series30 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series31 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series32 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series33 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series34 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series35 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series36 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea9 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series25 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simulationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.propertyGrid1.Size = new System.Drawing.Size(416, 348);
            this.propertyGrid1.TabIndex = 0;
            this.propertyGrid1.ToolbarVisible = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.simulationToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1249, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openSettingsToolStripMenuItem,
            this.saveSettingsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openSettingsToolStripMenuItem
            // 
            this.openSettingsToolStripMenuItem.Name = "openSettingsToolStripMenuItem";
            this.openSettingsToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.openSettingsToolStripMenuItem.Text = "Open Settings...";
            this.openSettingsToolStripMenuItem.Click += new System.EventHandler(this.openSettingsToolStripMenuItem_Click);
            // 
            // saveSettingsToolStripMenuItem
            // 
            this.saveSettingsToolStripMenuItem.Name = "saveSettingsToolStripMenuItem";
            this.saveSettingsToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.saveSettingsToolStripMenuItem.Text = "Save Settings...";
            this.saveSettingsToolStripMenuItem.Click += new System.EventHandler(this.saveSettingsToolStripMenuItem_Click);
            // 
            // simulationToolStripMenuItem
            // 
            this.simulationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem});
            this.simulationToolStripMenuItem.Name = "simulationToolStripMenuItem";
            this.simulationToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.simulationToolStripMenuItem.Text = "Simulation";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Enabled = false;
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.chart1);
            this.splitContainer1.Size = new System.Drawing.Size(1249, 611);
            this.splitContainer1.SplitterDistance = 416;
            this.splitContainer1.TabIndex = 2;
            // 
            // chart1
            // 
            chartArea10.AxisX.MajorGrid.LineColor = System.Drawing.Color.Gray;
            chartArea10.AxisX.Minimum = 0D;
            chartArea10.AxisY.MajorGrid.LineColor = System.Drawing.Color.Gray;
            chartArea10.AxisY.Title = "Population size";
            chartArea10.AxisY2.MajorGrid.LineColor = System.Drawing.Color.DarkGray;
            chartArea10.AxisY2.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea10.AxisY2.MajorTickMark.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea10.AxisY2.Title = "Age";
            chartArea10.CursorX.LineColor = System.Drawing.Color.MidnightBlue;
            chartArea10.CursorX.SelectionColor = System.Drawing.SystemColors.Highlight;
            chartArea10.CursorY.LineColor = System.Drawing.Color.MidnightBlue;
            chartArea10.Name = "ChartArea1";
            chartArea11.AlignWithChartArea = "ChartArea1";
            chartArea11.AxisX.MajorGrid.LineColor = System.Drawing.Color.Gray;
            chartArea11.AxisX.Minimum = 0D;
            chartArea11.AxisY.MajorGrid.LineColor = System.Drawing.Color.Gray;
            chartArea11.AxisY.Title = "Orig./Mort. rate";
            chartArea11.AxisY2.MajorGrid.LineColor = System.Drawing.Color.DarkGray;
            chartArea11.AxisY2.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea11.AxisY2.MajorTickMark.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea11.AxisY2.Title = "Survivability";
            chartArea11.Name = "ChartArea2";
            chartArea12.AlignWithChartArea = "ChartArea2";
            chartArea12.AxisX.MajorGrid.LineColor = System.Drawing.Color.Gray;
            chartArea12.AxisX.Minimum = 0D;
            chartArea12.AxisY.MajorGrid.LineColor = System.Drawing.Color.Gray;
            chartArea12.AxisY.Title = "Cunning";
            chartArea12.AxisY2.Title = "Ageing";
            chartArea12.Name = "ChartArea3";
            this.chart1.ChartAreas.Add(chartArea10);
            this.chart1.ChartAreas.Add(chartArea11);
            this.chart1.ChartAreas.Add(chartArea12);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend6.Name = "Legend1";
            this.chart1.Legends.Add(legend6);
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Name = "chart1";
            series26.ChartArea = "ChartArea2";
            series26.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            series26.Legend = "Legend1";
            series26.Name = "Crysis power";
            series27.BorderWidth = 3;
            series27.ChartArea = "ChartArea1";
            series27.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series27.Legend = "Legend1";
            series27.Name = "Population size";
            series28.BorderWidth = 3;
            series28.ChartArea = "ChartArea1";
            series28.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series28.Legend = "Legend1";
            series28.Name = "Average age";
            series28.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            series29.ChartArea = "ChartArea2";
            series29.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series29.Legend = "Legend1";
            series29.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Square;
            series29.Name = "Rate of origination";
            series30.ChartArea = "ChartArea2";
            series30.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series30.Legend = "Legend1";
            series30.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series30.Name = "Mortality rate";
            series31.BorderWidth = 3;
            series31.ChartArea = "ChartArea3";
            series31.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series31.Legend = "Legend1";
            series31.Name = "Ageing speed (Average)";
            series31.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            series32.BorderWidth = 3;
            series32.ChartArea = "ChartArea3";
            series32.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series32.Legend = "Legend1";
            series32.Name = "Cunning (Average)";
            series33.ChartArea = "ChartArea3";
            series33.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series33.Enabled = false;
            series33.Legend = "Legend1";
            series33.Name = "Ageing speed (Median)";
            series34.ChartArea = "ChartArea3";
            series34.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series34.Enabled = false;
            series34.Legend = "Legend1";
            series34.Name = "Ageing speed (Q1)";
            series35.ChartArea = "ChartArea3";
            series35.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series35.Enabled = false;
            series35.Legend = "Legend1";
            series35.Name = "Ageing speed (Q3)";
            series36.ChartArea = "ChartArea2";
            series36.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series36.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            series36.Legend = "Legend1";
            series36.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Triangle;
            series36.Name = "Survivability";
            series36.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            this.chart1.Series.Add(series26);
            this.chart1.Series.Add(series27);
            this.chart1.Series.Add(series28);
            this.chart1.Series.Add(series29);
            this.chart1.Series.Add(series30);
            this.chart1.Series.Add(series31);
            this.chart1.Series.Add(series32);
            this.chart1.Series.Add(series33);
            this.chart1.Series.Add(series34);
            this.chart1.Series.Add(series35);
            this.chart1.Series.Add(series36);
            this.chart1.Size = new System.Drawing.Size(829, 611);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            this.chart1.TextAntiAliasingQuality = System.Windows.Forms.DataVisualization.Charting.TextAntiAliasingQuality.SystemDefault;
            // 
            // chart2
            // 
            chartArea9.AxisX.Crossing = 0D;
            chartArea9.AxisX.MajorGrid.LineColor = System.Drawing.Color.Gray;
            chartArea9.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea9.AxisX.Minimum = 0D;
            chartArea9.AxisY.Crossing = 0D;
            chartArea9.AxisY.MajorGrid.LineColor = System.Drawing.Color.DarkGray;
            chartArea9.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea9.AxisY.Maximum = 1D;
            chartArea9.AxisY.Minimum = -1D;
            chartArea9.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea9);
            this.chart2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart2.Enabled = false;
            legend5.Enabled = false;
            legend5.Name = "Legend1";
            this.chart2.Legends.Add(legend5);
            this.chart2.Location = new System.Drawing.Point(0, 0);
            this.chart2.Name = "chart2";
            series25.BorderWidth = 3;
            series25.ChartArea = "ChartArea1";
            series25.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series25.Legend = "Legend1";
            series25.Name = "Ageing Cunning correlation";
            this.chart2.Series.Add(series25);
            this.chart2.Size = new System.Drawing.Size(416, 259);
            this.chart2.TabIndex = 1;
            this.chart2.Text = "chart2";
            this.chart2.TextAntiAliasingQuality = System.Windows.Forms.DataVisualization.Charting.TextAntiAliasingQuality.SystemDefault;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.propertyGrid1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.chart2);
            this.splitContainer2.Size = new System.Drawing.Size(416, 611);
            this.splitContainer2.SplitterDistance = 348;
            this.splitContainer2.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1249, 635);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "AgeingHaresSimulator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSettingsToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem simulationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
    }
}

