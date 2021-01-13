namespace CPUSchedulerPart1
{
    partial class MainView
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.btnLoad = new System.Windows.Forms.Button();
            this.txt1 = new System.Windows.Forms.TextBox();
            this.btnFCFSZ = new System.Windows.Forms.Button();
            this.btnSJFZ = new System.Windows.Forms.Button();
            this.btnFCFSO = new System.Windows.Forms.Button();
            this.btnSJFO = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(47, 249);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(241, 45);
            this.btnLoad.TabIndex = 0;
            this.btnLoad.Text = "Załaduj dane z pliku";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // txt1
            // 
            this.txt1.Location = new System.Drawing.Point(94, 12);
            this.txt1.Multiline = true;
            this.txt1.Name = "txt1";
            this.txt1.ReadOnly = true;
            this.txt1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txt1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt1.Size = new System.Drawing.Size(730, 220);
            this.txt1.TabIndex = 1;
            this.txt1.TextChanged += new System.EventHandler(this.txt1_TextChanged);
            // 
            // btnFCFSZ
            // 
            this.btnFCFSZ.Location = new System.Drawing.Point(370, 272);
            this.btnFCFSZ.Name = "btnFCFSZ";
            this.btnFCFSZ.Size = new System.Drawing.Size(179, 45);
            this.btnFCFSZ.TabIndex = 4;
            this.btnFCFSZ.Text = "FCFS";
            this.btnFCFSZ.UseVisualStyleBackColor = true;
            this.btnFCFSZ.Click += new System.EventHandler(this.btnFCFSZ_Click);
            // 
            // btnSJFZ
            // 
            this.btnSJFZ.Location = new System.Drawing.Point(370, 327);
            this.btnSJFZ.Name = "btnSJFZ";
            this.btnSJFZ.Size = new System.Drawing.Size(179, 45);
            this.btnSJFZ.TabIndex = 5;
            this.btnSJFZ.Text = "SJF";
            this.btnSJFZ.UseVisualStyleBackColor = true;
            this.btnSJFZ.Click += new System.EventHandler(this.btnSJFZ_Click);
            // 
            // btnFCFSO
            // 
            this.btnFCFSO.Location = new System.Drawing.Point(685, 272);
            this.btnFCFSO.Name = "btnFCFSO";
            this.btnFCFSO.Size = new System.Drawing.Size(179, 45);
            this.btnFCFSO.TabIndex = 6;
            this.btnFCFSO.Text = "FCFS";
            this.btnFCFSO.UseVisualStyleBackColor = true;
            this.btnFCFSO.Click += new System.EventHandler(this.btnFCFSO_Click);
            // 
            // btnSJFO
            // 
            this.btnSJFO.Location = new System.Drawing.Point(685, 327);
            this.btnSJFO.Name = "btnSJFO";
            this.btnSJFO.Size = new System.Drawing.Size(179, 45);
            this.btnSJFO.TabIndex = 7;
            this.btnSJFO.Text = "SJF";
            this.btnSJFO.UseVisualStyleBackColor = true;
            this.btnSJFO.Click += new System.EventHandler(this.btnSJFO_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(337, 244);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(257, 25);
            this.label1.TabIndex = 10;
            this.label1.Text = "Zamknięta pula procesów";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(654, 244);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(232, 25);
            this.label2.TabIndex = 11;
            this.label2.Text = "Otwarta pula procesów";
            // 
            // chart1
            // 
            this.chart1.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            chartArea1.Area3DStyle.PointDepth = 20;
            chartArea1.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.AxisX.Title = "Numer procesu";
            chartArea1.AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            chartArea1.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Bottom;
            legend1.Name = "Legend1";
            legend1.Title = "Wykres Gantta";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(0, 498);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.RangeBar;
            series1.IsVisibleInLegend = false;
            series1.LabelBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            series1.Legend = "Legend1";
            series1.Name = "s1";
            series1.YValuesPerPoint = 2;
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(953, 441);
            this.chart1.TabIndex = 12;
            this.chart1.Text = "chart1";
            title1.BackColor = System.Drawing.Color.White;
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            title1.Name = "Wykres Gantta";
            title1.Text = "Wykres Gantta";
            this.chart1.Titles.Add(title1);
            // 
            // checkBox1
            // 
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.checkBox1.Location = new System.Drawing.Point(47, 327);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(241, 38);
            this.checkBox1.TabIndex = 13;
            this.checkBox1.Text = "Zapisuj dane do pliku .txt";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(953, 939);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSJFO);
            this.Controls.Add(this.btnFCFSO);
            this.Controls.Add(this.btnSJFZ);
            this.Controls.Add(this.btnFCFSZ);
            this.Controls.Add(this.txt1);
            this.Controls.Add(this.btnLoad);
            this.Name = "MainView";
            this.Text = "Program symulacyjny - algorytmy FCFS i SJF - Jaroslaw T";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.TextBox txt1;
        private System.Windows.Forms.Button btnFCFSZ;
        private System.Windows.Forms.Button btnSJFZ;
        private System.Windows.Forms.Button btnFCFSO;
        private System.Windows.Forms.Button btnSJFO;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

