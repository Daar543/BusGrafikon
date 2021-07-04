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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
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
            this.chbToursF = new System.Windows.Forms.CheckBox();
            this.chbToursB = new System.Windows.Forms.CheckBox();
            this.btnLoadDistsF = new System.Windows.Forms.Button();
            this.btnLoadDistsB = new System.Windows.Forms.Button();
            this.slidToursB = new System.Windows.Forms.TrackBar();
            this.slidToursF = new System.Windows.Forms.TrackBar();
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
            this.slidZoom = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.BusChart)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slidToursB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slidToursF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slidZoom)).BeginInit();
            this.SuspendLayout();
            // 
            // BusChart
            // 
            chartArea2.Name = "ChartArea1";
            this.BusChart.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.BusChart.Legends.Add(legend2);
            this.BusChart.Location = new System.Drawing.Point(10, 9);
            this.BusChart.Name = "BusChart";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.BusChart.Series.Add(series2);
            this.BusChart.Size = new System.Drawing.Size(404, 429);
            this.BusChart.TabIndex = 0;
            this.BusChart.Text = "BusChart";
            // 
            // btnRenderBoth
            // 
            this.btnRenderBoth.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnRenderBoth.Location = new System.Drawing.Point(152, 384);
            this.btnRenderBoth.Name = "btnRenderBoth";
            this.btnRenderBoth.Size = new System.Drawing.Size(92, 36);
            this.btnRenderBoth.TabIndex = 1;
            this.btnRenderBoth.Text = "Vykresli obě";
            this.btnRenderBoth.UseVisualStyleBackColor = true;
            this.btnRenderBoth.Click += new System.EventHandler(this.RenderButtonBoth_Click);
            // 
            // btnChooseLine
            // 
            this.btnChooseLine.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnChooseLine.Location = new System.Drawing.Point(320, 7);
            this.btnChooseLine.Margin = new System.Windows.Forms.Padding(2);
            this.btnChooseLine.Name = "btnChooseLine";
            this.btnChooseLine.Size = new System.Drawing.Size(84, 48);
            this.btnChooseLine.TabIndex = 2;
            this.btnChooseLine.Text = "Vyber linku";
            this.btnChooseLine.UseVisualStyleBackColor = true;
            this.btnChooseLine.Click += new System.EventHandler(this.btnChooseLine_Click);
            // 
            // textBoxLine
            // 
            this.textBoxLine.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.textBoxLine.Location = new System.Drawing.Point(320, 59);
            this.textBoxLine.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxLine.Name = "textBoxLine";
            this.textBoxLine.Size = new System.Drawing.Size(85, 20);
            this.textBoxLine.TabIndex = 3;
            this.textBoxLine.Text = "JR/805004-T.txt";
            this.textBoxLine.TextChanged += new System.EventHandler(this.textBoxLine_TextChanged);
            // 
            // btnWorkday
            // 
            this.btnWorkday.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnWorkday.Location = new System.Drawing.Point(320, 82);
            this.btnWorkday.Margin = new System.Windows.Forms.Padding(2);
            this.btnWorkday.Name = "btnWorkday";
            this.btnWorkday.Size = new System.Drawing.Size(84, 47);
            this.btnWorkday.TabIndex = 4;
            this.btnWorkday.Text = "Načti pracovní dny";
            this.btnWorkday.UseVisualStyleBackColor = true;
            this.btnWorkday.Click += new System.EventHandler(this.btnWorkday_Click);
            // 
            // btnSaturday
            // 
            this.btnSaturday.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSaturday.Location = new System.Drawing.Point(320, 186);
            this.btnSaturday.Margin = new System.Windows.Forms.Padding(2);
            this.btnSaturday.Name = "btnSaturday";
            this.btnSaturday.Size = new System.Drawing.Size(84, 47);
            this.btnSaturday.TabIndex = 5;
            this.btnSaturday.Text = "Načti soboty";
            this.btnSaturday.UseVisualStyleBackColor = true;
            this.btnSaturday.Click += new System.EventHandler(this.btnSaturday_Click);
            // 
            // btnSunday
            // 
            this.btnSunday.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSunday.Location = new System.Drawing.Point(320, 238);
            this.btnSunday.Margin = new System.Windows.Forms.Padding(2);
            this.btnSunday.Name = "btnSunday";
            this.btnSunday.Size = new System.Drawing.Size(84, 47);
            this.btnSunday.TabIndex = 6;
            this.btnSunday.Text = "Načti neděle";
            this.btnSunday.UseVisualStyleBackColor = true;
            this.btnSunday.Click += new System.EventHandler(this.btnSunday_Click);
            // 
            // btnNoSchoolWorkday
            // 
            this.btnNoSchoolWorkday.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnNoSchoolWorkday.Location = new System.Drawing.Point(320, 134);
            this.btnNoSchoolWorkday.Margin = new System.Windows.Forms.Padding(2);
            this.btnNoSchoolWorkday.Name = "btnNoSchoolWorkday";
            this.btnNoSchoolWorkday.Size = new System.Drawing.Size(84, 47);
            this.btnNoSchoolWorkday.TabIndex = 7;
            this.btnNoSchoolWorkday.Text = "Načti školní prázdniny";
            this.btnNoSchoolWorkday.UseVisualStyleBackColor = true;
            this.btnNoSchoolWorkday.Click += new System.EventHandler(this.btnNoSchoolWorkday_Click);
            // 
            // chbWorkday
            // 
            this.chbWorkday.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chbWorkday.Enabled = false;
            this.chbWorkday.Location = new System.Drawing.Point(291, 291);
            this.chbWorkday.Margin = new System.Windows.Forms.Padding(2);
            this.chbWorkday.Name = "chbWorkday";
            this.chbWorkday.Size = new System.Drawing.Size(112, 16);
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
            this.chbSaturday.Location = new System.Drawing.Point(291, 335);
            this.chbSaturday.Margin = new System.Windows.Forms.Padding(2);
            this.chbSaturday.Name = "chbSaturday";
            this.chbSaturday.Size = new System.Drawing.Size(112, 18);
            this.chbSaturday.TabIndex = 9;
            this.chbSaturday.Text = "Sobota";
            this.chbSaturday.UseVisualStyleBackColor = true;
            this.chbSaturday.CheckedChanged += new System.EventHandler(this.chbSaturday_CheckedChanged);
            // 
            // chbSunday
            // 
            this.chbSunday.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chbSunday.Enabled = false;
            this.chbSunday.Location = new System.Drawing.Point(291, 356);
            this.chbSunday.Margin = new System.Windows.Forms.Padding(2);
            this.chbSunday.Name = "chbSunday";
            this.chbSunday.Size = new System.Drawing.Size(112, 16);
            this.chbSunday.TabIndex = 10;
            this.chbSunday.Text = "Neděle";
            this.chbSunday.UseVisualStyleBackColor = true;
            this.chbSunday.CheckedChanged += new System.EventHandler(this.chbSunday_CheckedChanged);
            // 
            // chbSchoolHoliday
            // 
            this.chbSchoolHoliday.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chbSchoolHoliday.Enabled = false;
            this.chbSchoolHoliday.Location = new System.Drawing.Point(291, 314);
            this.chbSchoolHoliday.Margin = new System.Windows.Forms.Padding(2);
            this.chbSchoolHoliday.Name = "chbSchoolHoliday";
            this.chbSchoolHoliday.Size = new System.Drawing.Size(112, 16);
            this.chbSchoolHoliday.TabIndex = 11;
            this.chbSchoolHoliday.Text = "Školní prázdniny";
            this.chbSchoolHoliday.UseVisualStyleBackColor = true;
            this.chbSchoolHoliday.CheckedChanged += new System.EventHandler(this.chbSchoolHoliday_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.slidZoom);
            this.panel2.Controls.Add(this.chbToursF);
            this.panel2.Controls.Add(this.chbToursB);
            this.panel2.Controls.Add(this.btnLoadDistsF);
            this.panel2.Controls.Add(this.btnLoadDistsB);
            this.panel2.Controls.Add(this.slidToursB);
            this.panel2.Controls.Add(this.slidToursF);
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
            this.panel2.Location = new System.Drawing.Point(419, 11);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(406, 427);
            this.panel2.TabIndex = 14;
            // 
            // chbToursF
            // 
            this.chbToursF.AutoSize = true;
            this.chbToursF.Location = new System.Drawing.Point(228, 224);
            this.chbToursF.Name = "chbToursF";
            this.chbToursF.Size = new System.Drawing.Size(82, 17);
            this.chbToursF.TabIndex = 32;
            this.chbToursF.Text = "Vybrat trasu";
            this.chbToursF.UseVisualStyleBackColor = true;
            // 
            // chbToursB
            // 
            this.chbToursB.AutoSize = true;
            this.chbToursB.Location = new System.Drawing.Point(92, 224);
            this.chbToursB.Name = "chbToursB";
            this.chbToursB.Size = new System.Drawing.Size(82, 17);
            this.chbToursB.TabIndex = 31;
            this.chbToursB.Text = "Vybrat trasu";
            this.chbToursB.UseVisualStyleBackColor = true;
            // 
            // btnLoadDistsF
            // 
            this.btnLoadDistsF.Location = new System.Drawing.Point(212, 195);
            this.btnLoadDistsF.Name = "btnLoadDistsF";
            this.btnLoadDistsF.Size = new System.Drawing.Size(103, 23);
            this.btnLoadDistsF.TabIndex = 30;
            this.btnLoadDistsF.Text = "Načíst vzdálenosti tras";
            this.btnLoadDistsF.UseVisualStyleBackColor = true;
            this.btnLoadDistsF.Click += new System.EventHandler(this.btnLoadDistsF_Click);
            // 
            // btnLoadDistsB
            // 
            this.btnLoadDistsB.Location = new System.Drawing.Point(92, 195);
            this.btnLoadDistsB.Name = "btnLoadDistsB";
            this.btnLoadDistsB.Size = new System.Drawing.Size(104, 23);
            this.btnLoadDistsB.TabIndex = 29;
            this.btnLoadDistsB.Text = "Načíst vzdálenosti tras";
            this.btnLoadDistsB.UseVisualStyleBackColor = true;
            this.btnLoadDistsB.Click += new System.EventHandler(this.btnLoadDistsB_Click);
            // 
            // slidToursB
            // 
            this.slidToursB.Location = new System.Drawing.Point(92, 240);
            this.slidToursB.Name = "slidToursB";
            this.slidToursB.Size = new System.Drawing.Size(104, 45);
            this.slidToursB.TabIndex = 28;
            // 
            // slidToursF
            // 
            this.slidToursF.Location = new System.Drawing.Point(212, 240);
            this.slidToursF.Name = "slidToursF";
            this.slidToursF.Size = new System.Drawing.Size(104, 45);
            this.slidToursF.TabIndex = 27;
            // 
            // btnRenderFront
            // 
            this.btnRenderFront.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRenderFront.Location = new System.Drawing.Point(320, 388);
            this.btnRenderFront.Name = "btnRenderFront";
            this.btnRenderFront.Size = new System.Drawing.Size(84, 32);
            this.btnRenderFront.TabIndex = 26;
            this.btnRenderFront.Text = "Vykresli";
            this.btnRenderFront.UseVisualStyleBackColor = true;
            this.btnRenderFront.Click += new System.EventHandler(this.btnRenderFront_Click);
            // 
            // btnRenderBack
            // 
            this.btnRenderBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRenderBack.Location = new System.Drawing.Point(3, 391);
            this.btnRenderBack.Name = "btnRenderBack";
            this.btnRenderBack.Size = new System.Drawing.Size(84, 32);
            this.btnRenderBack.TabIndex = 25;
            this.btnRenderBack.Text = "Vykresli";
            this.btnRenderBack.UseVisualStyleBackColor = true;
            this.btnRenderBack.Click += new System.EventHandler(this.btnRenderBack_Click);
            // 
            // chbSundayBack
            // 
            this.chbSundayBack.Enabled = false;
            this.chbSundayBack.Location = new System.Drawing.Point(2, 353);
            this.chbSundayBack.Margin = new System.Windows.Forms.Padding(2);
            this.chbSundayBack.Name = "chbSundayBack";
            this.chbSundayBack.Size = new System.Drawing.Size(112, 16);
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
            this.chbWorkdayBack.Location = new System.Drawing.Point(2, 286);
            this.chbWorkdayBack.Margin = new System.Windows.Forms.Padding(2);
            this.chbWorkdayBack.Name = "chbWorkdayBack";
            this.chbWorkdayBack.Size = new System.Drawing.Size(112, 18);
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
            this.chbSaturdayBack.Location = new System.Drawing.Point(2, 332);
            this.chbSaturdayBack.Margin = new System.Windows.Forms.Padding(2);
            this.chbSaturdayBack.Name = "chbSaturdayBack";
            this.chbSaturdayBack.Size = new System.Drawing.Size(112, 18);
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
            this.chbSchoolHolidayBack.Location = new System.Drawing.Point(2, 310);
            this.chbSchoolHolidayBack.Margin = new System.Windows.Forms.Padding(2);
            this.chbSchoolHolidayBack.Name = "chbSchoolHolidayBack";
            this.chbSchoolHolidayBack.Size = new System.Drawing.Size(112, 18);
            this.chbSchoolHolidayBack.TabIndex = 24;
            this.chbSchoolHolidayBack.Text = "Školní prázdniny";
            this.chbSchoolHolidayBack.UseVisualStyleBackColor = true;
            this.chbSchoolHolidayBack.CheckedChanged += new System.EventHandler(this.chbHolidayBack_CheckedChanged);
            // 
            // btnChooseBackLine
            // 
            this.btnChooseBackLine.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnChooseBackLine.Location = new System.Drawing.Point(2, 3);
            this.btnChooseBackLine.Margin = new System.Windows.Forms.Padding(2);
            this.btnChooseBackLine.Name = "btnChooseBackLine";
            this.btnChooseBackLine.Size = new System.Drawing.Size(84, 48);
            this.btnChooseBackLine.TabIndex = 15;
            this.btnChooseBackLine.Text = "Vyber linku (protisměr)";
            this.btnChooseBackLine.UseVisualStyleBackColor = true;
            this.btnChooseBackLine.Click += new System.EventHandler(this.btnChooseBackLine_Click);
            // 
            // textBoxLineBack
            // 
            this.textBoxLineBack.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.textBoxLineBack.Location = new System.Drawing.Point(2, 56);
            this.textBoxLineBack.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxLineBack.Name = "textBoxLineBack";
            this.textBoxLineBack.Size = new System.Drawing.Size(85, 20);
            this.textBoxLineBack.TabIndex = 16;
            this.textBoxLineBack.Text = "JR/805004-Z.txt";
            // 
            // btnSundayBack
            // 
            this.btnSundayBack.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnSundayBack.Location = new System.Drawing.Point(2, 235);
            this.btnSundayBack.Margin = new System.Windows.Forms.Padding(2);
            this.btnSundayBack.Name = "btnSundayBack";
            this.btnSundayBack.Size = new System.Drawing.Size(84, 47);
            this.btnSundayBack.TabIndex = 19;
            this.btnSundayBack.Text = "Načti neděle";
            this.btnSundayBack.UseVisualStyleBackColor = true;
            this.btnSundayBack.Click += new System.EventHandler(this.btnSundayBack_Click);
            // 
            // btnSchoolHolidayBack
            // 
            this.btnSchoolHolidayBack.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnSchoolHolidayBack.Location = new System.Drawing.Point(2, 131);
            this.btnSchoolHolidayBack.Margin = new System.Windows.Forms.Padding(2);
            this.btnSchoolHolidayBack.Name = "btnSchoolHolidayBack";
            this.btnSchoolHolidayBack.Size = new System.Drawing.Size(84, 47);
            this.btnSchoolHolidayBack.TabIndex = 20;
            this.btnSchoolHolidayBack.Text = "Načti školní prázdniny";
            this.btnSchoolHolidayBack.UseVisualStyleBackColor = true;
            this.btnSchoolHolidayBack.Click += new System.EventHandler(this.btnSchoolHolidayBack_Click);
            // 
            // btnSaturdayBack
            // 
            this.btnSaturdayBack.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnSaturdayBack.Location = new System.Drawing.Point(2, 183);
            this.btnSaturdayBack.Margin = new System.Windows.Forms.Padding(2);
            this.btnSaturdayBack.Name = "btnSaturdayBack";
            this.btnSaturdayBack.Size = new System.Drawing.Size(84, 47);
            this.btnSaturdayBack.TabIndex = 18;
            this.btnSaturdayBack.Text = "Načti soboty";
            this.btnSaturdayBack.UseVisualStyleBackColor = true;
            this.btnSaturdayBack.Click += new System.EventHandler(this.btnSaturdayBack_Click);
            // 
            // btnWorkdayBack
            // 
            this.btnWorkdayBack.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnWorkdayBack.Location = new System.Drawing.Point(2, 79);
            this.btnWorkdayBack.Margin = new System.Windows.Forms.Padding(2);
            this.btnWorkdayBack.Name = "btnWorkdayBack";
            this.btnWorkdayBack.Size = new System.Drawing.Size(84, 47);
            this.btnWorkdayBack.TabIndex = 17;
            this.btnWorkdayBack.Text = "Načti pracovní dny";
            this.btnWorkdayBack.UseVisualStyleBackColor = true;
            this.btnWorkdayBack.Click += new System.EventHandler(this.btnWorkdayBack_Click);
            // 
            // textboxInfoHoliday
            // 
            this.textboxInfoHoliday.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxInfoHoliday.Enabled = false;
            this.textboxInfoHoliday.Location = new System.Drawing.Point(228, 3);
            this.textboxInfoHoliday.Margin = new System.Windows.Forms.Padding(2);
            this.textboxInfoHoliday.Multiline = true;
            this.textboxInfoHoliday.Name = "textboxInfoHoliday";
            this.textboxInfoHoliday.Size = new System.Drawing.Size(88, 88);
            this.textboxInfoHoliday.TabIndex = 14;
            this.textboxInfoHoliday.Text = "Značky pro prázdniny (odděluj mezerou): nahoře pozitivní, dole negativní";
            // 
            // holidayNegativ
            // 
            this.holidayNegativ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.holidayNegativ.Location = new System.Drawing.Point(271, 140);
            this.holidayNegativ.Margin = new System.Windows.Forms.Padding(2);
            this.holidayNegativ.Multiline = true;
            this.holidayNegativ.Name = "holidayNegativ";
            this.holidayNegativ.Size = new System.Drawing.Size(45, 48);
            this.holidayNegativ.TabIndex = 13;
            this.holidayNegativ.Text = "20 28 29";
            this.holidayNegativ.TextChanged += new System.EventHandler(this.holidayNegative_TextChanged);
            // 
            // holidayPositive
            // 
            this.holidayPositive.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.holidayPositive.Location = new System.Drawing.Point(271, 96);
            this.holidayPositive.Margin = new System.Windows.Forms.Padding(2);
            this.holidayPositive.Multiline = true;
            this.holidayPositive.Name = "holidayPositive";
            this.holidayPositive.Size = new System.Drawing.Size(45, 40);
            this.holidayPositive.TabIndex = 12;
            this.holidayPositive.Text = "18 19";
            this.holidayPositive.TextChanged += new System.EventHandler(this.holidayPositive_TextChanged);
            // 
            // slidZoom
            // 
            this.slidZoom.Location = new System.Drawing.Point(140, 314);
            this.slidZoom.Maximum = 50;
            this.slidZoom.Minimum = 1;
            this.slidZoom.Name = "slidZoom";
            this.slidZoom.Size = new System.Drawing.Size(104, 45);
            this.slidZoom.TabIndex = 33;
            this.slidZoom.Value = 1;
            this.slidZoom.Scroll += new System.EventHandler(this.slidZoom_Scroll);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(819, 538);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.BusChart);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.BusChart)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slidToursB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slidToursF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slidZoom)).EndInit();
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
        private System.Windows.Forms.CheckBox chbToursF;
        private System.Windows.Forms.CheckBox chbToursB;
        private System.Windows.Forms.Button btnLoadDistsF;
        private System.Windows.Forms.Button btnLoadDistsB;
        private System.Windows.Forms.TrackBar slidToursB;
        private System.Windows.Forms.TrackBar slidToursF;
        private System.Windows.Forms.TrackBar slidZoom;
    }
}

