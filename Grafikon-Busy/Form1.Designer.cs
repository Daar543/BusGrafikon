namespace Grafikon_Busy
{
    partial class Form1
    {
        /// <summary>
        /// Vyžaduje se proměnná návrháře.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Uvolněte všechny používané prostředky.
        /// </summary>
        /// <param name="disposing">hodnota true, když by se měl spravovaný prostředek odstranit; jinak false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kód generovaný Návrhářem Windows Form

        /// <summary>
        /// Metoda vyžadovaná pro podporu Návrháře - neupravovat
        /// obsah této metody v editoru kódu.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.BusChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnRenderBoth = new System.Windows.Forms.Button();
            this.btnChooseLine = new System.Windows.Forms.Button();
            this.textBoxLine = new System.Windows.Forms.TextBox();
            this.btnWorkday = new System.Windows.Forms.Button();
            this.btnSaturday = new System.Windows.Forms.Button();
            this.btnSunday = new System.Windows.Forms.Button();
            this.btnNoSchoolWorkday = new System.Windows.Forms.Button();
            this.chbWorkday = new System.Windows.Forms.CheckBox();
            this.chbSaturday = new System.Windows.Forms.CheckBox();
            this.chbSunday = new System.Windows.Forms.CheckBox();
            this.chbSchoolHoliday = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnRenderFront = new System.Windows.Forms.Button();
            this.btnRenderBack = new System.Windows.Forms.Button();
            this.chbSundayBack = new System.Windows.Forms.CheckBox();
            this.chbWorkdayBack = new System.Windows.Forms.CheckBox();
            this.chbSaturdayBack = new System.Windows.Forms.CheckBox();
            this.chbSchoolHolidayBack = new System.Windows.Forms.CheckBox();
            this.btnChooseBackLine = new System.Windows.Forms.Button();
            this.textBoxLineBack = new System.Windows.Forms.TextBox();
            this.btnSundayBack = new System.Windows.Forms.Button();
            this.btnSchoolHolidayBack = new System.Windows.Forms.Button();
            this.btnSaturdayBack = new System.Windows.Forms.Button();
            this.btnWorkdayBack = new System.Windows.Forms.Button();
            this.textboxInfoHoliday = new System.Windows.Forms.TextBox();
            this.holidayNegativ = new System.Windows.Forms.TextBox();
            this.holidayPositive = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.BusChart)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // BusChart
            // 
            chartArea1.Name = "ChartArea1";
            this.BusChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.BusChart.Legends.Add(legend1);
            this.BusChart.Location = new System.Drawing.Point(13, -11);
            this.BusChart.Margin = new System.Windows.Forms.Padding(4);
            this.BusChart.Name = "BusChart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.BusChart.Series.Add(series1);
            this.BusChart.Size = new System.Drawing.Size(556, 528);
            this.BusChart.TabIndex = 0;
            this.BusChart.Text = "BusChart";
            // 
            // btnRenderBoth
            // 
            this.btnRenderBoth.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnRenderBoth.Location = new System.Drawing.Point(172, 473);
            this.btnRenderBoth.Margin = new System.Windows.Forms.Padding(4);
            this.btnRenderBoth.Name = "btnRenderBoth";
            this.btnRenderBoth.Size = new System.Drawing.Size(123, 44);
            this.btnRenderBoth.TabIndex = 1;
            this.btnRenderBoth.Text = "Vykresli obě";
            this.btnRenderBoth.UseVisualStyleBackColor = true;
            this.btnRenderBoth.Click += new System.EventHandler(this.RenderButtonBoth_Click);
            // 
            // btnChooseLine
            // 
            this.btnChooseLine.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnChooseLine.Location = new System.Drawing.Point(362, 7);
            this.btnChooseLine.Name = "btnChooseLine";
            this.btnChooseLine.Size = new System.Drawing.Size(112, 59);
            this.btnChooseLine.TabIndex = 2;
            this.btnChooseLine.Text = "Vyber linku";
            this.btnChooseLine.UseVisualStyleBackColor = true;
            this.btnChooseLine.Click += new System.EventHandler(this.btnChooseLine_Click);
            // 
            // textBoxLine
            // 
            this.textBoxLine.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.textBoxLine.Location = new System.Drawing.Point(362, 72);
            this.textBoxLine.Name = "textBoxLine";
            this.textBoxLine.Size = new System.Drawing.Size(112, 22);
            this.textBoxLine.TabIndex = 3;
            this.textBoxLine.Text = "JR/805004-T.txt";
            this.textBoxLine.TextChanged += new System.EventHandler(this.textBoxLine_TextChanged);
            // 
            // btnWorkday
            // 
            this.btnWorkday.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnWorkday.Location = new System.Drawing.Point(362, 100);
            this.btnWorkday.Name = "btnWorkday";
            this.btnWorkday.Size = new System.Drawing.Size(112, 58);
            this.btnWorkday.TabIndex = 4;
            this.btnWorkday.Text = "Načti pracovní dny";
            this.btnWorkday.UseVisualStyleBackColor = true;
            this.btnWorkday.Click += new System.EventHandler(this.btnWorkday_Click);
            // 
            // btnSaturday
            // 
            this.btnSaturday.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSaturday.Location = new System.Drawing.Point(362, 228);
            this.btnSaturday.Name = "btnSaturday";
            this.btnSaturday.Size = new System.Drawing.Size(112, 58);
            this.btnSaturday.TabIndex = 5;
            this.btnSaturday.Text = "Načti soboty";
            this.btnSaturday.UseVisualStyleBackColor = true;
            this.btnSaturday.Click += new System.EventHandler(this.btnSaturday_Click);
            // 
            // btnSunday
            // 
            this.btnSunday.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSunday.Location = new System.Drawing.Point(362, 292);
            this.btnSunday.Name = "btnSunday";
            this.btnSunday.Size = new System.Drawing.Size(112, 58);
            this.btnSunday.TabIndex = 6;
            this.btnSunday.Text = "Načti neděle";
            this.btnSunday.UseVisualStyleBackColor = true;
            this.btnSunday.Click += new System.EventHandler(this.btnSunday_Click);
            // 
            // btnNoSchoolWorkday
            // 
            this.btnNoSchoolWorkday.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnNoSchoolWorkday.Location = new System.Drawing.Point(362, 164);
            this.btnNoSchoolWorkday.Name = "btnNoSchoolWorkday";
            this.btnNoSchoolWorkday.Size = new System.Drawing.Size(112, 58);
            this.btnNoSchoolWorkday.TabIndex = 7;
            this.btnNoSchoolWorkday.Text = "Načti školní prázdniny";
            this.btnNoSchoolWorkday.UseVisualStyleBackColor = true;
            this.btnNoSchoolWorkday.Click += new System.EventHandler(this.btnNoSchoolWorkday_Click);
            // 
            // chbWorkday
            // 
            this.chbWorkday.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chbWorkday.Enabled = false;
            this.chbWorkday.Location = new System.Drawing.Point(324, 356);
            this.chbWorkday.Name = "chbWorkday";
            this.chbWorkday.Size = new System.Drawing.Size(150, 20);
            this.chbWorkday.TabIndex = 8;
            this.chbWorkday.Text = "Pracovní dny";
            this.chbWorkday.UseVisualStyleBackColor = true;
            this.chbWorkday.CheckedChanged += new System.EventHandler(this.chbWorkday_CheckedChanged);
            // 
            // chbSaturday
            // 
            this.chbSaturday.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chbSaturday.Enabled = false;
            this.chbSaturday.Location = new System.Drawing.Point(324, 412);
            this.chbSaturday.Name = "chbSaturday";
            this.chbSaturday.Size = new System.Drawing.Size(150, 20);
            this.chbSaturday.TabIndex = 9;
            this.chbSaturday.Text = "Sobota";
            this.chbSaturday.UseVisualStyleBackColor = true;
            this.chbSaturday.CheckedChanged += new System.EventHandler(this.chbSaturday_CheckedChanged);
            // 
            // chbSunday
            // 
            this.chbSunday.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chbSunday.Enabled = false;
            this.chbSunday.Location = new System.Drawing.Point(324, 438);
            this.chbSunday.Name = "chbSunday";
            this.chbSunday.Size = new System.Drawing.Size(150, 20);
            this.chbSunday.TabIndex = 10;
            this.chbSunday.Text = "Neděle";
            this.chbSunday.UseVisualStyleBackColor = true;
            this.chbSunday.CheckedChanged += new System.EventHandler(this.chbSunday_CheckedChanged);
            // 
            // chbSchoolHoliday
            // 
            this.chbSchoolHoliday.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chbSchoolHoliday.Enabled = false;
            this.chbSchoolHoliday.Location = new System.Drawing.Point(324, 386);
            this.chbSchoolHoliday.Name = "chbSchoolHoliday";
            this.chbSchoolHoliday.Size = new System.Drawing.Size(150, 20);
            this.chbSchoolHoliday.TabIndex = 11;
            this.chbSchoolHoliday.Text = "Školní prázdniny";
            this.chbSchoolHoliday.UseVisualStyleBackColor = true;
            this.chbSchoolHoliday.CheckedChanged += new System.EventHandler(this.chbSchoolHoliday_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.btnRenderFront);
            this.panel2.Controls.Add(this.btnRenderBack);
            this.panel2.Controls.Add(this.chbSundayBack);
            this.panel2.Controls.Add(this.chbWorkdayBack);
            this.panel2.Controls.Add(this.chbSaturdayBack);
            this.panel2.Controls.Add(this.chbSchoolHolidayBack);
            this.panel2.Controls.Add(this.btnChooseBackLine);
            this.panel2.Controls.Add(this.textBoxLineBack);
            this.panel2.Controls.Add(this.btnSundayBack);
            this.panel2.Controls.Add(this.btnSchoolHolidayBack);
            this.panel2.Controls.Add(this.btnSaturdayBack);
            this.panel2.Controls.Add(this.btnWorkdayBack);
            this.panel2.Controls.Add(this.textboxInfoHoliday);
            this.panel2.Controls.Add(this.holidayNegativ);
            this.panel2.Controls.Add(this.holidayPositive);
            this.panel2.Controls.Add(this.chbSunday);
            this.panel2.Controls.Add(this.chbWorkday);
            this.panel2.Controls.Add(this.chbSaturday);
            this.panel2.Controls.Add(this.chbSchoolHoliday);
            this.panel2.Controls.Add(this.btnChooseLine);
            this.panel2.Controls.Add(this.textBoxLine);
            this.panel2.Controls.Add(this.btnRenderBoth);
            this.panel2.Controls.Add(this.btnSunday);
            this.panel2.Controls.Add(this.btnNoSchoolWorkday);
            this.panel2.Controls.Add(this.btnSaturday);
            this.panel2.Controls.Add(this.btnWorkday);
            this.panel2.Location = new System.Drawing.Point(576, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(478, 523);
            this.panel2.TabIndex = 14;
            // 
            // btnRenderFront
            // 
            this.btnRenderFront.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRenderFront.Location = new System.Drawing.Point(362, 475);
            this.btnRenderFront.Margin = new System.Windows.Forms.Padding(4);
            this.btnRenderFront.Name = "btnRenderFront";
            this.btnRenderFront.Size = new System.Drawing.Size(112, 40);
            this.btnRenderFront.TabIndex = 26;
            this.btnRenderFront.Text = "Vykresli";
            this.btnRenderFront.UseVisualStyleBackColor = true;
            this.btnRenderFront.Click += new System.EventHandler(this.btnRenderFront_Click);
            // 
            // btnRenderBack
            // 
            this.btnRenderBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRenderBack.Location = new System.Drawing.Point(4, 479);
            this.btnRenderBack.Margin = new System.Windows.Forms.Padding(4);
            this.btnRenderBack.Name = "btnRenderBack";
            this.btnRenderBack.Size = new System.Drawing.Size(112, 40);
            this.btnRenderBack.TabIndex = 25;
            this.btnRenderBack.Text = "Vykresli";
            this.btnRenderBack.UseVisualStyleBackColor = true;
            this.btnRenderBack.Click += new System.EventHandler(this.btnRenderBack_Click);
            // 
            // chbSundayBack
            // 
            this.chbSundayBack.Enabled = false;
            this.chbSundayBack.Location = new System.Drawing.Point(3, 434);
            this.chbSundayBack.Name = "chbSundayBack";
            this.chbSundayBack.Size = new System.Drawing.Size(150, 20);
            this.chbSundayBack.TabIndex = 23;
            this.chbSundayBack.Text = "Neděle";
            this.chbSundayBack.UseVisualStyleBackColor = true;
            this.chbSundayBack.CheckedChanged += new System.EventHandler(this.chbSundayBack_CheckedChanged);
            // 
            // chbWorkdayBack
            // 
            this.chbWorkdayBack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.chbWorkdayBack.Enabled = false;
            this.chbWorkdayBack.Location = new System.Drawing.Point(3, 352);
            this.chbWorkdayBack.Name = "chbWorkdayBack";
            this.chbWorkdayBack.Size = new System.Drawing.Size(150, 20);
            this.chbWorkdayBack.TabIndex = 21;
            this.chbWorkdayBack.Text = "Pracovní dny";
            this.chbWorkdayBack.UseVisualStyleBackColor = true;
            this.chbWorkdayBack.CheckedChanged += new System.EventHandler(this.chbWorkBack_CheckedChanged);
            // 
            // chbSaturdayBack
            // 
            this.chbSaturdayBack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.chbSaturdayBack.Enabled = false;
            this.chbSaturdayBack.Location = new System.Drawing.Point(3, 408);
            this.chbSaturdayBack.Name = "chbSaturdayBack";
            this.chbSaturdayBack.Size = new System.Drawing.Size(150, 20);
            this.chbSaturdayBack.TabIndex = 22;
            this.chbSaturdayBack.Text = "Sobota";
            this.chbSaturdayBack.UseVisualStyleBackColor = true;
            this.chbSaturdayBack.CheckedChanged += new System.EventHandler(this.chbSaturdayBack_CheckedChanged);
            // 
            // chbSchoolHolidayBack
            // 
            this.chbSchoolHolidayBack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.chbSchoolHolidayBack.Enabled = false;
            this.chbSchoolHolidayBack.Location = new System.Drawing.Point(3, 382);
            this.chbSchoolHolidayBack.Name = "chbSchoolHolidayBack";
            this.chbSchoolHolidayBack.Size = new System.Drawing.Size(150, 20);
            this.chbSchoolHolidayBack.TabIndex = 24;
            this.chbSchoolHolidayBack.Text = "Školní prázdniny";
            this.chbSchoolHolidayBack.UseVisualStyleBackColor = true;
            this.chbSchoolHolidayBack.CheckedChanged += new System.EventHandler(this.chbHolidayBack_CheckedChanged);
            // 
            // btnChooseBackLine
            // 
            this.btnChooseBackLine.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnChooseBackLine.Location = new System.Drawing.Point(3, 3);
            this.btnChooseBackLine.Name = "btnChooseBackLine";
            this.btnChooseBackLine.Size = new System.Drawing.Size(112, 59);
            this.btnChooseBackLine.TabIndex = 15;
            this.btnChooseBackLine.Text = "Vyber linku (protisměr)";
            this.btnChooseBackLine.UseVisualStyleBackColor = true;
            this.btnChooseBackLine.Click += new System.EventHandler(this.btnChooseBackLine_Click);
            // 
            // textBoxLineBack
            // 
            this.textBoxLineBack.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.textBoxLineBack.Location = new System.Drawing.Point(3, 68);
            this.textBoxLineBack.Name = "textBoxLineBack";
            this.textBoxLineBack.Size = new System.Drawing.Size(112, 22);
            this.textBoxLineBack.TabIndex = 16;
            this.textBoxLineBack.Text = "JR/805004-Z.txt";
            // 
            // btnSundayBack
            // 
            this.btnSundayBack.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnSundayBack.Location = new System.Drawing.Point(3, 288);
            this.btnSundayBack.Name = "btnSundayBack";
            this.btnSundayBack.Size = new System.Drawing.Size(112, 58);
            this.btnSundayBack.TabIndex = 19;
            this.btnSundayBack.Text = "Načti neděle";
            this.btnSundayBack.UseVisualStyleBackColor = true;
            this.btnSundayBack.Click += new System.EventHandler(this.btnSundayBack_Click);
            // 
            // btnSchoolHolidayBack
            // 
            this.btnSchoolHolidayBack.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnSchoolHolidayBack.Location = new System.Drawing.Point(3, 160);
            this.btnSchoolHolidayBack.Name = "btnSchoolHolidayBack";
            this.btnSchoolHolidayBack.Size = new System.Drawing.Size(112, 58);
            this.btnSchoolHolidayBack.TabIndex = 20;
            this.btnSchoolHolidayBack.Text = "Načti školní prázdniny";
            this.btnSchoolHolidayBack.UseVisualStyleBackColor = true;
            this.btnSchoolHolidayBack.Click += new System.EventHandler(this.btnSchoolHolidayBack_Click);
            // 
            // btnSaturdayBack
            // 
            this.btnSaturdayBack.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnSaturdayBack.Location = new System.Drawing.Point(3, 224);
            this.btnSaturdayBack.Name = "btnSaturdayBack";
            this.btnSaturdayBack.Size = new System.Drawing.Size(112, 58);
            this.btnSaturdayBack.TabIndex = 18;
            this.btnSaturdayBack.Text = "Načti soboty";
            this.btnSaturdayBack.UseVisualStyleBackColor = true;
            this.btnSaturdayBack.Click += new System.EventHandler(this.btnSaturdayBack_Click);
            // 
            // btnWorkdayBack
            // 
            this.btnWorkdayBack.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnWorkdayBack.Location = new System.Drawing.Point(3, 96);
            this.btnWorkdayBack.Name = "btnWorkdayBack";
            this.btnWorkdayBack.Size = new System.Drawing.Size(112, 58);
            this.btnWorkdayBack.TabIndex = 17;
            this.btnWorkdayBack.Text = "Načti pracovní dny";
            this.btnWorkdayBack.UseVisualStyleBackColor = true;
            this.btnWorkdayBack.Click += new System.EventHandler(this.btnWorkdayBack_Click);
            // 
            // textboxInfoHoliday
            // 
            this.textboxInfoHoliday.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxInfoHoliday.Enabled = false;
            this.textboxInfoHoliday.Location = new System.Drawing.Point(240, 7);
            this.textboxInfoHoliday.Multiline = true;
            this.textboxInfoHoliday.Name = "textboxInfoHoliday";
            this.textboxInfoHoliday.Size = new System.Drawing.Size(116, 108);
            this.textboxInfoHoliday.TabIndex = 14;
            this.textboxInfoHoliday.Text = "Značky pro prázdniny (odděluj mezerou): nahoře pozitivní, dole negativní";
            // 
            // holidayNegativ
            // 
            this.holidayNegativ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.holidayNegativ.Location = new System.Drawing.Point(297, 182);
            this.holidayNegativ.Multiline = true;
            this.holidayNegativ.Name = "holidayNegativ";
            this.holidayNegativ.Size = new System.Drawing.Size(59, 58);
            this.holidayNegativ.TabIndex = 13;
            this.holidayNegativ.Text = "20 28 29";
            this.holidayNegativ.TextChanged += new System.EventHandler(this.holidayNegative_TextChanged);
            // 
            // holidayPositive
            // 
            this.holidayPositive.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.holidayPositive.Location = new System.Drawing.Point(297, 121);
            this.holidayPositive.Multiline = true;
            this.holidayPositive.Name = "holidayPositive";
            this.holidayPositive.Size = new System.Drawing.Size(59, 46);
            this.holidayPositive.TabIndex = 12;
            this.holidayPositive.Text = "18 19";
            this.holidayPositive.TextChanged += new System.EventHandler(this.holidayPositive_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.BusChart);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.BusChart)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart BusChart;
        private System.Windows.Forms.Button btnRenderBoth;
        private System.Windows.Forms.Button btnChooseLine;
        private System.Windows.Forms.TextBox textBoxLine;
        private System.Windows.Forms.Button btnWorkday;
        private System.Windows.Forms.Button btnSaturday;
        private System.Windows.Forms.Button btnSunday;
        private System.Windows.Forms.Button btnNoSchoolWorkday;
        private System.Windows.Forms.CheckBox chbWorkday;
        private System.Windows.Forms.CheckBox chbSaturday;
        private System.Windows.Forms.CheckBox chbSunday;
        private System.Windows.Forms.CheckBox chbSchoolHoliday;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textboxInfoHoliday;
        private System.Windows.Forms.TextBox holidayNegativ;
        private System.Windows.Forms.TextBox holidayPositive;
        private System.Windows.Forms.CheckBox chbSundayBack;
        private System.Windows.Forms.CheckBox chbWorkdayBack;
        private System.Windows.Forms.CheckBox chbSaturdayBack;
        private System.Windows.Forms.CheckBox chbSchoolHolidayBack;
        private System.Windows.Forms.Button btnChooseBackLine;
        private System.Windows.Forms.TextBox textBoxLineBack;
        private System.Windows.Forms.Button btnSundayBack;
        private System.Windows.Forms.Button btnSchoolHolidayBack;
        private System.Windows.Forms.Button btnSaturdayBack;
        private System.Windows.Forms.Button btnWorkdayBack;
        private System.Windows.Forms.Button btnRenderFront;
        private System.Windows.Forms.Button btnRenderBack;
    }
}

