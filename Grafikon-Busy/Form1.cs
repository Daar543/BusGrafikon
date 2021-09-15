using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace Grafikon_Busy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.Size = defaultSize;
            this.AutoScroll = true;
            BusChart.Size = new Size(this.Size.Width * 2 / 3, this.Size.Height);
            BusChart.Legends.Clear();
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                          (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2 + 200);

            CheckBoxesFront = new List<CheckBox>() { chbWorkday, chbSchoolHoliday, chbSaturday, chbSunday, chbToursF }.AsReadOnly();
            CheckBoxesBack = new List<CheckBox>() { chbWorkdayBack, chbSchoolHolidayBack, chbSaturdayBack, chbSundayBack, chbToursB }.AsReadOnly();

#if DEBUG
            this.textBoxLineBack.Text = "JR/Text/805008-Z.txt";
            this.textBoxLine.Text = "JR/Text/805008-T.txt";
            this.txbJdfLoad.Text = "JR/Jdf/805008-2021";

            Thread.Sleep(1000);
            btnJdfLoad_Click(null, EventArgs.Empty);
#endif
        }

        string[] StopsF;
        string[] StopsB;
        Stop[][] StopsDistsF;
        Stop[][] StopsDistsB;
        TimeTableParser TimeTableF;
        TimeTableParser TimeTableB;
        string[][] tempTable;

        static char[] SignSeparators = new char[] { ' ' };
        const int directionsCount = 2;
        float distanceSpread => (float)nudStopDist.Value; //The extra range between individual Y-labels (stop names) need on chart
        float labelRange => this.distanceSpread / 2 - 0.001f; //Distance from the Y-label's center and edge;
        const int maxDistance = 0; //How will the distances be rescaled
        const int PointMarkSize = 7; // Size of the points on chart
        static readonly int DayTypeCount = Enum.GetNames(typeof(DayType)).Length;
        IReadOnlyList<CheckBox> CheckBoxesFront { get; }
        IReadOnlyList<CheckBox> CheckBoxesBack { get; }


        ConnectionGroup[] TableFront = new ConnectionGroup[DayTypeCount];
        ConnectionGroup[] TableBack = new ConnectionGroup[DayTypeCount];
        //Color[,] TableColoring = new Color[DayTypeCount, directionsCount];

        static readonly Color[,] TableColoring = new Color[4, directionsCount]{
            {Color.Brown,Color.Orange },{Color.Yellow,Color.Green },
            {Color.Blue,Color.DarkCyan },{Color.Magenta,Color.Purple}
        };
        Size defaultSize = new Size(1920, 800);
        enum RenderMode
        {
            Front = 0,
            Back = 1,
            Both = 2
        }
        enum DistanceMode
        {
            Front = 0,
            Back = 1,
            Neither = 2
        }

        /// <summary>
        /// Redraw the chart area
        /// </summary>
        private void ClearGraphicon()
        {
            var Chart = BusChart.ChartAreas[0];
            this.Size = new Size((int)(Math.Sqrt(slidZoom.Value) * defaultSize.Width), (int)(Math.Sqrt(slidZoom.Value) * defaultSize.Height));
            BusChart.Size = new Size((int)(this.Size.Width * Math.Sqrt(slidZoom.Value) - 2 * panel2.Width), (int)(this.Size.Height * Math.Sqrt(slidZoom.Value)));
            Chart.AxisX.ScaleView.Zoomable = true;
            Chart.CursorX.AutoScroll = true;
            Chart.CursorX.IsUserSelectionEnabled = true;
            //panel2.Location = new Point(BusChart.Width, 0);
            BusChart.Series.Clear();

            /*Chart.AxisX.IntervalType = DateTimeIntervalType.Number;*/
            Chart.AxisX.LabelStyle.Format = "";
            Chart.AxisY.LabelStyle.Format = "";

            Chart.AxisX.Minimum = 0 * 60;
            Chart.AxisX.Maximum = 24 * 60;

            //Chart.AxisY.Minimum = 0;
            //Chart.AxisY.Maximum = maxDistance+distanceSpread;

            Chart.AxisX.Interval = 60;

            for (int i = (int)Chart.AxisX.Minimum / 60; i < (int)Chart.AxisX.Maximum / 60; i += 1)
            {
                Chart.AxisX.CustomLabels.Add(new CustomLabel()
                {
                    FromPosition = (i - 0.5f) * 60,
                    ToPosition = (i + 0.5f) * 60,
                    Text = i.ToString(),
                });
            }

            Chart.AxisY.Interval = 1;

            Chart.AxisX.MajorGrid.LineWidth = 1;
            Chart.AxisX.MajorGrid.Interval = 60;

            Chart.AxisX.MajorGrid.Enabled = chbHourLine.Checked;
            Chart.AxisY.MajorGrid.Enabled = false;



            Chart.AxisY.CustomLabels.Clear();
            Chart.AxisY.StripLines.Clear();
        }
        /// <summary>
        /// Draws the graphicon based on all enabled settings (there should be no error, if the setting is enabled)
        /// </summary>
        /// <param name="conns">Collection of connections for a particular line</param>
        /// <param name="stoplist">Names of all stops, as they are in the timetable</param>
        /// <param name="stopDists">List of tariff distances for each stops</param>
        /// <param name="priorityDir"></param>
        private void RenderGraphicon(IEnumerable<ConnectionGroup> conns, string[] stoplist, Stop[] stopDistsF, Stop[]stopDistsB, RenderMode rm)
        {

            ClearGraphicon();

            var Chart = BusChart.ChartAreas[0];

            DistanceMode dm;
            
            if (rm == RenderMode.Front)
            {
                dm = stopDistsF is null ? DistanceMode.Neither:DistanceMode.Front;

            }
            else if (rm == RenderMode.Back)
            {
                dm = stopDistsB is null ? DistanceMode.Neither : DistanceMode.Back;
            }
            else if (rm == RenderMode.Both)
            {
                if (stopDistsF is null && stopDistsB is null)
                {
                    dm = DistanceMode.Neither;
                } 
                else if (stopDistsF != null)
                    
                {
                    dm = DistanceMode.Front;
                    stopDistsB = stopDistsF;
                }
                else //Maybe add this later
                {
                    dm = DistanceMode.Back;
                    stopDistsF = stopDistsB;
                }
                    
            }
            else
            {
                throw new ArgumentException("Nonexistent distance mode");
            }
            Stop[] stopDists;
            switch (dm)
            {
                case DistanceMode.Neither:
                    stopDists = null;
                    break;
                case DistanceMode.Front:
                    stopDists = stopDistsF;
                    break;
                case DistanceMode.Back:
                    //stopDists = stopDistsB;
                    stopDists = stopDistsB;
                    break;
                default:
                    throw new ArgumentException("Nonexistent distance mode");
            }
            //If there are no distances, then spread them by 1
            if (dm == DistanceMode.Neither)
            {
                Chart.AxisY.Minimum = 0;
                Chart.AxisY.Maximum = stoplist.Length + labelRange;
                double lineWidth = PointMarkSize*Chart.AxisY.Maximum / 3200d; 
                //At smaller charts, the stripline normally gets thicker;
                //therefore, here I multiply it by the size of chart

                Chart.AxisY.CustomLabels.Add(new CustomLabel
                {
                    FromPosition = 0,
                    ToPosition = labelRange,
                    Text = stoplist[0],
                });
                for (int i = 1; i < stoplist.Length; ++i)
                {
                    Chart.AxisY.CustomLabels.Add(new CustomLabel
                    {
                        FromPosition = i - labelRange,
                        ToPosition = i + labelRange,
                        Text = stoplist[i],
                    });
                    Chart.AxisY.StripLines.Add(new StripLine
                    {
                        Interval = 0,
                        IntervalOffset = i - lineWidth / 2,
                        StripWidth = lineWidth,
                        BackColor = Color.Black,
                    });
                }

            }
            else
            {
                //Labels in format: distance, name
                var maxDistance = stopDists[stopDists.Length - 1].Distance;
                Chart.AxisY.Minimum = 0;
                Chart.AxisY.Maximum = maxDistance;
                double lineWidth = PointMarkSize * Chart.AxisY.Maximum / 3200d;

                Stop sd;
                double distance;
                int order;

                for (int i = 0; i < stopDists.Length; ++i)
                {
                    sd = stopDists[i];
                    distance = dm == DistanceMode.Back ? maxDistance - sd.Distance : sd.Distance;
                    //order = dm == DistanceMode.Back ? stoplist.Length - 1 - sd.Order : sd.Order;
                    //distance = sd.Distance;
                    order = sd.Order;

                    Chart.AxisY.CustomLabels.Add(new CustomLabel
                    {
                        FromPosition = distance - labelRange,
                        ToPosition = distance + labelRange,
                        Text = $"({distance:f0}) {stoplist[order]}"
                    });
                    Chart.AxisY.StripLines.Add(new StripLine
                    {
                        Interval = 0,
                        IntervalOffset = distance - lineWidth / 2,
                        StripWidth = lineWidth,
                        BackColor = Color.Black,
                    });
                }
            }


            BusChart.Series.Clear();
            foreach (ConnectionGroup group in conns)
            {
                //Skip the disabled days
                if (group is null || !group.Enabled)
                    continue;
                bool invert = false;
                if(rm == RenderMode.Both && (!group.Direction &&  dm == DistanceMode.Front) ||(group.Direction && dm == DistanceMode.Back))
                {
                    invert = true;
                }
                List<Series> newS = MakeStopSeriesForTable(group, group.Direction?stopDistsF:stopDistsB,invert);
                foreach (Series s in newS)
                    BusChart.Series.Add(s);
            }
        }


        private List<Series> MakeStopSeriesForTable(ConnectionGroup CG, Stop[] zst, bool inverted = false)
        {
            List<Series> Ser = new List<Series>();
            foreach (var connName in CG.Connections.Keys)
            {
                var Sx = new Series
                {
                    ChartType = SeriesChartType.Line,
                    Color = CG.ChartColor,

                    MarkerColor = CG.ChartColor,
                    MarkerStyle = MarkerStyle.Circle,
                    MarkerSize = PointMarkSize
                };
                string[] connection = inverted ? CG.Connections[connName].Reverse().ToArray() : CG.Connections[connName];
                if (zst is null)
                {
                    AddConnection(Sx, connection, CG.Direction);
                }
                else
                {
                    AddConnection(Sx, connection, zst, CG.Direction, inverted);
                }
                Ser.Add(Sx);
            }
            return Ser;
        }
        private void AddConnection(Series s, string[] connectionTimes, bool forward)
        {
            for (int i = 0; i < connectionTimes.Length; ++i)
            {
                int indx = forward ? i : connectionTimes.Length - 1 - i;
                string checkedTime = connectionTimes[indx];
                if (checkedTime == "|" || checkedTime == "<")
                {
                    continue; //stop is skipped
                }
                else if (checkedTime == "")
                {
                    continue; //stop is skipped
                }
                else if (TimeConverter.HoursMinsToMins(checkedTime, out int connMinute))
                {
                    s.Points.AddXY(connMinute, i); //stop is in parsable time
                }
                else
                {
                    throw new FormatException("Time not in correct format!");
                }
            }
        }
        private void AddConnection(Series s, string[] connectionTimes, Stop[] stops, bool forward, bool invert)
        {
            for (int i = 0; i < stops.Length; ++i)
            {
                Stop Z = stops[i];
                double dist = (forward ^ invert) ? Z.Distance : stops[stops.Length - 1].Distance - Z.Distance;
                string checkedTime = connectionTimes[Z.Order];
                if (checkedTime == "|" || checkedTime == "")
                {
                    continue; //stops is passed through, has no impact on kilometrage
                }
                else if (checkedTime == "<")
                {
                    /*s.Points.Clear();*/ //This route should not be on the stops
                    continue;
                    //return;
                }
                else if (TimeConverter.HoursMinsToMins(checkedTime, out int connMinute))
                {
                    s.Points.AddXY(connMinute, dist); //stop is in parsable time
                }
                else
                {
                    throw new FormatException("Time not in correct format!");
                }
            }
        }
        

        /// <summary>
        /// Loads the line from a file whose name is specified in text box. If the file with this name does not exist, tries to add ".txt" to the file name. If neither exists, shows error message and ends.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChooseLine_Click(object sender, EventArgs e)
        {
            CheckBoxesFront.DisableAll();
            slidToursF.Enabled = false;

            StopsF = null;
            TableFront = new ConnectionGroup[DayTypeCount];
            string line = textBoxLine.Text;

            if (line == "")
            {
                MessageBox.Show("Linka neexistuje", "Chybný vstup", MessageBoxButtons.RetryCancel);
                return;
            }
            try
            {
                string[][] initTable = SheetLoader.RowifyTable(SheetLoader.ReadCsvInput(line, '\t'));
                TimeTableF = new TimeTableParser(initTable, holidayPositive.Text, holidayNegativ.Text);
                btnLoadDistsF.Enabled = true;
            }
            catch (FileNotFoundException)
            {
                line += ".txt";
                try
                {
                    string[][] initTable = SheetLoader.RowifyTable(SheetLoader.ReadCsvInput(line, '\t'));
                    TimeTableF = new TimeTableParser(initTable, holidayPositive.Text, holidayNegativ.Text);
                    textBoxLine.Text = line; //Updates the text so exception has not to be catched again
                    btnLoadDistsF.Enabled = true;
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("Linka neexistuje", "Chybný vstup", MessageBoxButtons.RetryCancel);
                }
            }
        }
        private void btnChooseBackLine_Click(object sender, EventArgs e)
        {
            CheckBoxesBack.DisableAll();
            slidToursB.Enabled = false;
            StopsB = null;
            TableBack = new ConnectionGroup[DayTypeCount];
            string line = textBoxLineBack.Text;
            if (line == "")
            {
                MessageBox.Show("Linka neexistuje", "Chybný vstup", MessageBoxButtons.RetryCancel);
                return;
            }
            try
            {
                string[][] initTable = SheetLoader.RowifyTable(SheetLoader.ReadCsvInput(line, '\t'));
                TimeTableB = new TimeTableParser(initTable, holidayPositive.Text, holidayNegativ.Text);
                btnLoadDistsB.Enabled = true;
            }
            catch (FileNotFoundException)
            {
                line += ".txt";
                try
                {
                    string[][] initTable = SheetLoader.RowifyTable(SheetLoader.ReadCsvInput(line, '\t'));
                    TimeTableB = new TimeTableParser(initTable, holidayPositive.Text, holidayNegativ.Text);
                    textBoxLine.Text = line; //Updates the text so exception has not to be catched again
                    btnLoadDistsB.Enabled = true;
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("Linka neexistuje", "Chybný vstup", MessageBoxButtons.RetryCancel);
                }
            }
        }
        private enum DayType
        {
            Workday, SchoolHoliday, Saturday, Sunday
        }
        private enum Direction
        {
            Forward, Backward
        }
        private bool ExtractDaysFromTimeTable(DayType day, Direction direction)
        {
            TimeTableParser ChosenTimeTable;
            ConnectionGroup[] FinalizedCG;
            ref string[] StopList = ref StopsF;
            switch (direction)
            {
                case Direction.Forward:
                    ChosenTimeTable = TimeTableF;
                    FinalizedCG = TableFront;
                    //StopList = ref StopsF;
                    break;
                case Direction.Backward:
                    ChosenTimeTable = TimeTableB;
                    FinalizedCG = TableBack;
                    StopList = ref StopsB;
                    break;
                default:
                    throw new ArgumentException("Nonsense direction!");
            }
            if (ChosenTimeTable == null)
            {
                MessageBox.Show("Žádná linka není načtena", "Chybný vstup", MessageBoxButtons.OK);
                return false;
            }
            Color color;
            switch (day)
            {
                case DayType.Workday:
                    tempTable = ChosenTimeTable.CreateWorkdayTable();
                    break;
                case DayType.SchoolHoliday:
                    tempTable = ChosenTimeTable.CreateWorkHolidayTable();
                    break;
                case DayType.Saturday:
                    tempTable = ChosenTimeTable.CreateSaturdayTable();
                    break;
                case DayType.Sunday:
                    tempTable = ChosenTimeTable.CreateSundayTable();
                    break;
                default:
                    throw new ArgumentException("Type of day not recognized");

            }
            color = TableColoring[(int)day, (int)direction];

            //Make connections from the selected table
            if (!ChosenTimeTable.CreateStopsAndConnections(tempTable,
                out string[] analyzedStops, out Dictionary<string, string[]> analyzedConnections
                ))
            {
                MessageBox.Show("Vstup byl pravděpodobně poškozen, ovšem některá data je možno stále získat.", "Poškozený vstup", MessageBoxButtons.OK);
            }
            else
            {
                //MessageBox.Show("Analýza jízdního řádu proběhla v pořádku", "Hotovo", MessageBoxButtons.OK);
            }

            FinalizedCG[(int)day] = new ConnectionGroup(analyzedConnections, color, direction == Direction.Forward);
            //bool ss = ChosenTimeTable.GetStops(out this.stops);
            if (StopList is null)
                StopList = analyzedStops;

            return true;
        }
        private void btnWorkday_Click(object sender, EventArgs e)
        {
            bool succ = ExtractDaysFromTimeTable(DayType.Workday, Direction.Forward);
            chbWorkday.Enabled = succ;
            return;
        }
        private void btnSaturday_Click(object sender, EventArgs e)
        {
            bool succ = ExtractDaysFromTimeTable(DayType.Saturday, Direction.Forward);
            chbSaturday.Enabled = succ;
            return;
        }

        private void btnNoSchoolWorkday_Click(object sender, EventArgs e)
        {
            bool succ = ExtractDaysFromTimeTable(DayType.SchoolHoliday, Direction.Forward);
            chbSchoolHoliday.Enabled = succ;
            return;
        }

        private void btnSunday_Click(object sender, EventArgs e)
        {
            bool succ = ExtractDaysFromTimeTable(DayType.Sunday, Direction.Forward);
            chbSunday.Enabled = succ;
            return;
        }

        private void chbWorkday_CheckedChanged(object sender, EventArgs e)
        {
            TableFront[(int)DayType.Workday].Enabled = chbWorkday.Checked;
        }

        private void chbSchoolHoliday_CheckedChanged(object sender, EventArgs e)
        {
            TableFront[(int)DayType.SchoolHoliday].Enabled = chbSchoolHoliday.Checked;
        }

        private void chbSaturday_CheckedChanged(object sender, EventArgs e)
        {
            TableFront[(int)DayType.Saturday].Enabled = chbSaturday.Checked;
        }

        private void chbSunday_CheckedChanged(object sender, EventArgs e)
        {
            TableFront[(int)DayType.Sunday].Enabled = chbSunday.Checked;
        }
        //Fix this taking too much time when changing text
        private void holidayPositive_TextChanged(object sender, EventArgs e)
        {
            if (TimeTableF is null)
                return;
            TimeTableF.HolidayPositiveSigns = holidayPositive.Text.Split(SignSeparators);
        }
        //Fix this taking too much time when changing text
        private void holidayNegative_TextChanged(object sender, EventArgs e)
        {
            if (TimeTableF is null)
                return;
            TimeTableF.HolidayNegativeSigns = holidayNegativ.Text.Split(SignSeparators);
        }



        private void btnWorkdayBack_Click(object sender, EventArgs e)
        {
            bool succ = ExtractDaysFromTimeTable(DayType.Workday, Direction.Backward);
            chbWorkdayBack.Enabled = succ;
            return;
        }

        private void btnSchoolHolidayBack_Click(object sender, EventArgs e)
        {
            bool succ = ExtractDaysFromTimeTable(DayType.SchoolHoliday, Direction.Backward);
            chbSchoolHolidayBack.Enabled = succ;
            return;
        }

        private void btnSaturdayBack_Click(object sender, EventArgs e)
        {
            bool succ = ExtractDaysFromTimeTable(DayType.Saturday, Direction.Backward);
            chbSaturdayBack.Enabled = succ;
            return;
        }

        private void btnSundayBack_Click(object sender, EventArgs e)
        {
            bool succ = ExtractDaysFromTimeTable(DayType.Sunday, Direction.Backward);
            chbSundayBack.Enabled = succ;
            return;
        }

        private void chbWorkBack_CheckedChanged(object sender, EventArgs e)
        {
            TableBack[(int)DayType.Workday].Enabled = chbWorkdayBack.Checked;
        }

        private void chbHolidayBack_CheckedChanged(object sender, EventArgs e)
        {
            TableBack[(int)DayType.SchoolHoliday].Enabled = chbSchoolHolidayBack.Checked;
        }

        private void chbSaturdayBack_CheckedChanged(object sender, EventArgs e)
        {
            TableBack[(int)DayType.Saturday].Enabled = chbSaturdayBack.Checked;
        }

        private void chbSundayBack_CheckedChanged(object sender, EventArgs e)
        {
            TableBack[(int)DayType.Sunday].Enabled = chbSundayBack.Checked;
        }

        private void btnRenderFront_Click(object sender, EventArgs e)
        {
            if (StopsF is null)
            {
                ClearGraphicon();
                return;
            }
            if (chbToursF.Checked)
            {
                RenderGraphicon(TableFront, StopsF, StopsDistsF[slidToursF.Value], null, RenderMode.Front);
            }
            else
            {
                RenderGraphicon(TableFront, StopsF, null, null, RenderMode.Front);
            }

        }

        private void btnRenderBack_Click(object sender, EventArgs e)
        {
            if (StopsB is null)
            {
                ClearGraphicon();
                return;
            }
            if (chbToursB.Checked)
            {
                RenderGraphicon(TableBack, StopsB, null, StopsDistsB[slidToursB.Value], RenderMode.Back);
            }
            else
            {
                RenderGraphicon(TableBack, StopsB.Reverse().ToArray(), null, null, RenderMode.Back);
            }

        }
        private void RenderButtonBoth_Click(object sender, EventArgs e)
        {
            if (StopsF is null || StopsB is null)
            {
                MessageBox.Show("Linky nemají protichůdné zastávky, nebo zastávky ještě nejsou načteny.", "Chybný vstup", MessageBoxButtons.OK);
                //ClearGraphicon();
                return;
            }
            else if (StopsB.IsInverseOf(StopsF))
            {
                if (chbToursF.Checked == chbToursB.Checked) //Both distances are checked or unchecked, so no distance measured
                {
                    RenderGraphicon(TableFront.Concat(TableBack), StopsF, null, null,RenderMode.Both);
                }
                else
                {
                    if (chbToursF.Checked)
                    {
                        RenderGraphicon(TableFront.Concat(TableBack), StopsF, StopsDistsF[slidToursF.Value], null, RenderMode.Both );
                    }
                    else
                    {
                        RenderGraphicon(TableFront.Concat(TableBack), StopsF, null, StopsDistsB[slidToursB.Value], RenderMode.Both);
                    }
                }
            }
            else
            {
                MessageBox.Show("Linky nemají protichůdné zastávky, nebo zastávky ještě nejsou načteny.", "Chybný vstup", MessageBoxButtons.OK);
            }

        }
        private void btnLoadDistsF_Click(object sender, EventArgs e)
        {
            chbToursF.Enabled = false;
            if (!TimeTableF.GetKilometrageTable(out string[][] kilometrage))
            {
                MessageBox.Show("Vstup nemá správný formát!", "Chybný vstup", MessageBoxButtons.OK);
                return;
            }

            if (!TimeTableF.ExtractKilometragesFromTable(kilometrage, distanceSpread, maxDistance, false, out Stop[][] Zastavky))
            {
                MessageBox.Show("Vzdálenosti nešlo dobře najít", "Chybný vstup", MessageBoxButtons.OK);
                return;
            }
            this.StopsDistsF = Zastavky;
            chbToursF.Enabled = true;
            slidToursF.Maximum = StopsDistsF.Length - 1;
            slidToursF.Enabled = true;
            return;
        }

        private void btnLoadDistsB_Click(object sender, EventArgs e)
        {
            chbToursB.Enabled = false;
            if (!TimeTableB.GetKilometrageTable(out string[][] kilometrage))
            {
                MessageBox.Show("Vstup nemá správný formát!", "Chybný vstup", MessageBoxButtons.OK);
                return;
            }

            if (!TimeTableB.ExtractKilometragesFromTable(kilometrage, distanceSpread, maxDistance, false, out Stop[][] Zastavky))
            {
                MessageBox.Show("Vzdálenosti nešlo dobře najít", "Chybný vstup", MessageBoxButtons.OK);
                return;
            }
            this.StopsDistsB = Zastavky;
            chbToursB.Enabled = true;
            slidToursB.Maximum = StopsDistsB.Length - 1;
            slidToursB.Enabled = true;
            return;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            BusChart.Printing.Print(true);
        }

        private void btnJdfLoad_Click(object sender, EventArgs e)
        {
            var Parser = new JdfParser();
            string folder = txbJdfLoad.Text;
            //check if folder exists
            Parser.NactiVse(folder);
            Parser.VytvorObjekty();
            Parser.MapujPevneKody();
            Parser.SeradVse();
            /*/var tabulka1 = Parser.PostavTabulku((805008, 1), true);
            TimeTableF = new TimeTableParser(tabulka1, holidayPositive.Text, holidayNegativ.Text);
            btnLoadDistsF.Enabled = true;/**/

            /**/var tabulka2 = Parser.PostavTabulku((805008, 1), false);
            TimeTableB = new TimeTableParser(tabulka2, holidayPositive.Text, holidayNegativ.Text);
            btnLoadDistsB.Enabled = true;/**/
        }

    }
}
