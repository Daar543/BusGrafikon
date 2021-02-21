using RozvrhBusu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Grafikon_Busy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Size = new Size(1600, 800);
            BusChart.Size = new Size(this.Size.Width * 2 / 3, this.Size.Height);
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                          (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2 + 200);
        }
        static double[] kilometrage = new double[] { 0, 2, 3, 4, 5, 5, 6, 6.5, 7 };
        double[] kilometrage2 = ArrayCalculations.Normalize(kilometrage, 0.3);
        string[] StopsF;
        string[] StopsB;
        static char[] SignSeparators = new char[] { ' ' };
        const int directionsCount = 2;
        static readonly int DayTypeCount = Enum.GetNames(typeof(DayType)).Length;
        ConnectionGroup[] TableFront = new ConnectionGroup[DayTypeCount];
        ConnectionGroup[] TableBack = new ConnectionGroup[DayTypeCount];
        //Color[,] TableColoring = new Color[DayTypeCount, directionsCount];
        Color[,] TableColoring = new Color[4, directionsCount]{
            {Color.Brown,Color.Orange },{Color.Yellow,Color.Green },
            {Color.Blue,Color.DarkCyan },{Color.Magenta,Color.Purple}
        };

        private void RenderGraphicon(IEnumerable<ConnectionGroup> conns, string[] stoplist, double[]distances)
        {
            var Chart = BusChart.ChartAreas[0];
            BusChart.Size = new Size(this.Size.Width * 2 / 3, this.Size.Height);
            BusChart.Series.Clear();

            Chart.AxisX.IntervalType = DateTimeIntervalType.Number;
            Chart.AxisX.LabelStyle.Format = "";
            Chart.AxisY.LabelStyle.Format = "";

            Chart.AxisX.Minimum = 0 * 60;
            Chart.AxisX.Maximum = 24 * 60;

            Chart.AxisX.Interval = 15;

            Chart.AxisY.Interval = 1;

            Chart.AxisX.MajorGrid.LineWidth = 0;
            Chart.AxisY.MajorGrid.LineWidth = 0;

            Chart.AxisY.CustomLabels.Clear();

            Chart.AxisY.CustomLabels.Add(new CustomLabel
            {
                FromPosition = distances[0],
                ToPosition = distances[0] + (distances[1] - distances[0]) / 2,
                Text = stoplist[0],
            });
            for (int i = 1; i < stoplist.Length-1; ++i)
            {
                Chart.AxisY.CustomLabels.Add(new CustomLabel
                {
                    FromPosition = distances[i] - (distances[i]-distances[i-1])/2,
                    ToPosition = distances[i] + (distances[i+1] - distances[i]) / 2,
                    Text = stoplist[i],
                });
            }
            if (stoplist.Length >= 1)
            {
                Chart.AxisY.CustomLabels.Add(new CustomLabel
                {
                    FromPosition = distances[stoplist.Length-1] - (distances[stoplist.Length-1]-distances[stoplist.Length-2])/2,
                    ToPosition = distances[stoplist.Length - 1],
                    Text = stoplist[stoplist.Length - 1],
                });
            }
            BusChart.Series.Clear();

            /*BusChart.Series.Add("Spoj 3");
            BusChart.Series["Spoj 3"].ChartType = SeriesChartType.Line;
            BusChart.Series["Spoj 3"].Color = Color.Blue;

            for(int i = 0; i < Math.Min(connections.Length, stops.Length); ++i)
            {
                BusChart.Series["Spoj 3"].Points.AddXY(6*60+connections[i],i);
            }*/
            foreach (ConnectionGroup group in conns)
            {
                if (group is null || !group.Enabled)
                    continue;
                List<Series> newS = MakeSeriesForTable(group,distances);
                foreach (Series s in newS)
                    BusChart.Series.Add(s);
            }

            foreach (var legend in BusChart.Legends)
            {
                legend.Enabled = false;
            }
        }

        private void RenderButtonBoth_Click(object sender, EventArgs e)
        {
            if (StopsB.IsInverseOf(StopsF))
            {
                RenderGraphicon(TableFront.Concat(TableBack), StopsF,kilometrage2);
            }
            else
            {
                MessageBox.Show("Linky nemají protichůdné zastávky!", "Chybný vstup", MessageBoxButtons.OK);
                btnRenderBoth.Enabled = false;
            }
            //RenderGraphicon(ArrayExtension.Enumerate<ConnectionGroup>(Tables));

        }
        private List<Series> MakeSeriesForTable(ConnectionGroup CG,double[]distances)
        {
            List<Series> Ser = new List<Series>();
            foreach (var connName in CG.Connections.Keys)
            {
                var Sx = new Series
                {
                    ChartType = SeriesChartType.Line,
                    Color = CG.ChartColor,

                    //refactor these
                    MarkerColor = Color.Red,
                    MarkerStyle = MarkerStyle.Circle,
                    MarkerSize = 7
                };
                AddConnection(Sx, CG.Connections[connName], distances, CG.Direction);

                Ser.Add(Sx);
            }
            return Ser;
        }
        private void AddConnection(Series s, string[] connectionTimes, double[]distances, bool dir)
        {
            for (int i = 0; i < connectionTimes.Length; ++i)
            {
                string checkedTime = dir ? connectionTimes[i] : connectionTimes[connectionTimes.Length - 1 - i];
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
                    s.Points.AddXY(connMinute, distances[i]); //stop is in parsable time
                }
                else
                {
                    throw new FormatException("Time not in correct format!");
                }
            }
        }
        TimeTableParser TimeTableF;
        TimeTableParser TimeTableB;
        string[][] tempTable;

        /// <summary>
        /// Loads the line from a file whose name is specified in text box. If the file with this name does not exist, tries to add ".txt" to the file name. If neither exists, shows error message and ends.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChooseLine_Click(object sender, EventArgs e)
        {
            chbWorkday.Enabled = false;
            chbWorkday.Checked = false;
            chbSchoolHoliday.Enabled = false;
            chbSchoolHoliday.Checked = false;
            chbSaturday.Enabled = false;
            chbSaturday.Checked = false;
            chbSunday.Enabled = false;
            chbSunday.Checked = false;
            StopsF = null;
            TableFront = new ConnectionGroup[DayTypeCount];
            string line = textBoxLine.Text;
            try
            {
                string[][] initTable = SheetLoader.RowifyTable(SheetLoader.ReadExcelInput(line, '\t'));
                TimeTableF = new TimeTableParser(initTable, holidayPositive.Text, holidayNegativ.Text);
            }
            catch (FileNotFoundException)
            {
                line += ".txt";
                try
                {
                    string[][] initTable = SheetLoader.RowifyTable(SheetLoader.ReadExcelInput(line, '\t'));
                    TimeTableF = new TimeTableParser(initTable, holidayPositive.Text, holidayNegativ.Text);
                    textBoxLine.Text = line; //Updates the text so exception has not to be catched again
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
                    StopList = ref StopsF;
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
            if (!ChosenTimeTable.CreateStopsAndConnections(tempTable,
                out string[] analyzedStops, out Dictionary<string, string[]> analyzedConnections
                ))
            {
                MessageBox.Show("Vstup byl pravděpodobně poškozen, ovšem některá data je možno stále získat.", "Poškozený vstup", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Analýza jízdního řádu proběhla v pořádku", "Hotovo", MessageBoxButtons.OK);
            }

            FinalizedCG[(int)day] = new ConnectionGroup(analyzedConnections, color, direction == Direction.Forward);
            //bool ss = ChosenTimeTable.GetStops(out this.stops);
            if (StopList is null)
                StopList = analyzedStops;
            /*switch (direction)
            {
                case Direction.Forward:
                    if (StopsF is null)
                        StopsF = analyzedStops;
                    break;
                case Direction.Backward:
                    if (StopsB is null)
                        StopsB = analyzedStops;
                    break;
                default:
                    throw new ArgumentException("Nonsense direction!");
            }*/
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
        private void textBoxLine_TextChanged(object sender, EventArgs e)
        {

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

        private void btnChooseBackLine_Click(object sender, EventArgs e)
        {
            chbWorkdayBack.Enabled = false;
            chbWorkdayBack.Checked = false;
            chbSchoolHolidayBack.Enabled = false;
            chbSchoolHolidayBack.Checked = false;
            chbSaturdayBack.Enabled = false;
            chbSaturdayBack.Checked = false;
            chbSundayBack.Enabled = false;
            chbSundayBack.Checked = false;
            StopsB = null;
            TableBack = new ConnectionGroup[DayTypeCount];
            string line = textBoxLineBack.Text;
            try
            {
                string[][] initTable = SheetLoader.RowifyTable(SheetLoader.ReadExcelInput(line, '\t'));
                TimeTableB = new TimeTableParser(initTable, holidayPositive.Text, holidayNegativ.Text);
            }
            catch (FileNotFoundException)
            {
                line += ".txt";
                try
                {
                    string[][] initTable = SheetLoader.RowifyTable(SheetLoader.ReadExcelInput(line, '\t'));
                    TimeTableB = new TimeTableParser(initTable, holidayPositive.Text, holidayNegativ.Text);
                    textBoxLine.Text = line; //Updates the text so exception has not to be catched again
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("Linka neexistuje", "Chybný vstup", MessageBoxButtons.RetryCancel);
                }
            }
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
            RenderGraphicon(TableFront, StopsF,kilometrage2);
        }

        private void btnRenderBack_Click(object sender, EventArgs e)
        {
            RenderGraphicon(TableBack, StopsB.Reverse().ToArray(),kilometrage2);
        }
    }
}
