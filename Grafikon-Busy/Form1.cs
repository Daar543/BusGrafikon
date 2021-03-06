﻿using RozvrhBusu;
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


//TODO: MÍSTO STRUKTURY "ZASTÁVKA" UDĚLEJ MAPOVÁNÍ POŘADÍ:KILOMETRY
namespace Grafikon_Busy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Size = new Size(1920, 800);
            BusChart.Size = new Size(this.Size.Width * 2 / 3, this.Size.Height);
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                          (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2 + 200);
        }
        bool Detection = false; //True version not working yet
        string[] StopsF;
        string[] StopsB;
        Zastavka[][] StopsDistsF;
        Zastavka[][] StopsDistsB;
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
        private void ClearGraphicon()
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
        }
        private void RenderGraphicon(IEnumerable<ConnectionGroup> conns, string[] stoplist, Zastavka[]stopDists, bool priorityDir)
        {

            ClearGraphicon();
            
            var Chart = BusChart.ChartAreas[0];
            /*Chart.AxisY.CustomLabels.Add(new CustomLabel
            {
                FromPosition = stopDists[0].Distance,
                ToPosition = stopDists[0].Distance + (stopDists[1].Distance - stopDists[0].Distance) / 2,
                Text = stoplist[stopDists[0].Order],
            });
            for (int i = 1; i < stopDists.Length-1; ++i)
            {
                Chart.AxisY.CustomLabels.Add(new CustomLabel
                {
                    FromPosition = stopDists[i].Distance - (stopDists[i].Distance-stopDists[i-1].Distance)/2,
                    ToPosition = stopDists[i].Distance + (stopDists[i+1].Distance - stopDists[i].Distance) / 2,
                    Text = stoplist[stopDists[i].Order],
                });
            }
            if (stopDists.Length >= 1)
            {
                Chart.AxisY.CustomLabels.Add(new CustomLabel
                {
                    FromPosition = stopDists[stopDists.Length-1].Distance - (stopDists[stopDists.Length-1].Distance - stopDists[stopDists.Length-2].Distance) /2,
                    ToPosition = stopDists[stopDists.Length - 1].Distance,
                    Text = stoplist[stopDists[stopDists.Length - 1].Order],
                });
            }*/
            if (stopDists is null)
            {
                Chart.AxisY.CustomLabels.Add(new CustomLabel
                {
                    FromPosition = 0,
                    ToPosition = 0.25,
                    Text = stoplist[0],
                });
                for (int i = 1; i < stoplist.Length; ++i)
                {
                    Chart.AxisY.CustomLabels.Add(new CustomLabel
                    {
                        FromPosition = i - 0.25,
                        ToPosition = i + 0.25,
                        Text = stoplist[i],
                    });
                }
                
            }
            else
            {
                Chart.AxisY.CustomLabels.Add(new CustomLabel
                {
                    FromPosition = stopDists[0].Distance - 0.25,
                    ToPosition = stopDists[0].Distance + 0.25,
                    Text = stoplist[stopDists[0].Order],
                });
                for (int i = 1; i < stopDists.Length - 1; ++i)
                {
                    Chart.AxisY.CustomLabels.Add(new CustomLabel
                    {
                        FromPosition = stopDists[i].Distance - 0.25,
                        ToPosition = stopDists[i].Distance + 0.25,
                        Text = stoplist[stopDists[i].Order],
                    });
                }
                if (stopDists.Length >= 1)
                {
                    Chart.AxisY.CustomLabels.Add(new CustomLabel
                    {
                        FromPosition = stopDists[stopDists.Length - 1].Distance - 0.25,
                        ToPosition = stopDists[stopDists.Length - 1].Distance + 0.25,
                        Text = stoplist[stopDists[stopDists.Length - 1].Order],
                    });
                }
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
                List<Series> newS = MakeSeriesForTable(group,stopDists, priorityDir);
                foreach (Series s in newS)
                    BusChart.Series.Add(s);
            }

            foreach (var legend in BusChart.Legends)
            {
                legend.Enabled = false;
            }
        }

        
        private List<Series> MakeSeriesForTable(ConnectionGroup CG, Zastavka[]zst, bool priorityDir)
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
                if(zst is null)
                    AddConnection(Sx, CG.Connections[connName], CG.Direction);
                else
                    AddConnection(Sx, CG.Connections[connName], zst, CG.Direction^priorityDir);
                

                Ser.Add(Sx);
            }
            return Ser;
        }
        private void AddConnection(Series s, string[] connectionTimes, bool dir)
        {
            for (int i = 0; i < connectionTimes.Length; ++i)
            {
                int indx = dir ? i : connectionTimes.Length - 1 - i;
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
        private void AddConnection(Series s, string[] connectionTimes, Zastavka[] dists, bool forw)
        {
            int first = int.MaxValue;
            int last = 0;
            //Slower method, but can also mark routes with other stops
            /*if(Detection)
            {
                bool susge = false;
                int current = 0;
                for(int i = dists[0].Order; i < connectionTimes.Length; ++i)
                {
                    ref string checkedTime = ref connectionTimes[i];
                    if (checkedTime == "|")
                    {
                        continue; //stops is passed through, has no impact on kilometrage
                    }
                    else if(checkedTime == "")
                    {
                        if(current < dists.Length)
                            if (i == dists[current].Order)
                                current++;
                        continue; 
                    }
                    else if (checkedTime == "<")
                    {
                        if (current < dists.Length)
                            if (i == dists[current].Order)
                                current++;
                        continue;
                    }
                    else if (TimeConverter.HoursMinsToMins(checkedTime, out int connMinute))
                    {
                        if(current < dists.Length && i < dists[current].Order)
                        {
                            //Detect routes with additional stops
                            susge = true;
                            //s.BorderDashStyle = ChartDashStyle.Dash;
                            continue;
                        }
                        if(susge)
                        {
                            s.BorderDashStyle = ChartDashStyle.Dash;
                        }
                        if (current >= dists.Length)
                            return;
                        s.Points.AddXY(connMinute, dists[current].Distance); //stop is in parsable time
                        current++;
                    }
                    else
                    {
                        throw new FormatException("Time not in correct format!");
                    }
                }
            }
            else*/
            foreach (var Z in dists)
            {
                if (Z.Order < first) first = Z.Order;   //Not necessary
                if (Z.Order > last) last = Z.Order;     //if the dists are ordered
                //int indx = !dir ? Z.Order : connectionTimes.Length - 1 - Z.Order;
                int indx = forw ? Z.Order : connectionTimes.Length - 1 - Z.Order;
                string checkedTime = connectionTimes[indx];
                if (checkedTime == "|" || checkedTime == "" ) 
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
                    s.Points.AddXY(connMinute, Z.Distance); //stop is in parsable time
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
            chbToursF.Enabled = false;
            chbToursF.Checked = false;
            slidToursF.Enabled = false;
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
            chbToursB.Enabled = false;
            chbToursB.Checked = false;
            slidToursB.Enabled = false;
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
                RenderGraphicon(TableFront, StopsF, StopsDistsF[slidToursF.Value], false);
            else
                RenderGraphicon(TableFront, StopsF, null, false);
        }

        private void btnRenderBack_Click(object sender, EventArgs e)
        {
            if(StopsB is null)
            {
                ClearGraphicon();
                return;
            }
            if (chbToursB.Checked)
                RenderGraphicon(TableBack, StopsB, StopsDistsB[slidToursB.Value], true);
            else
                RenderGraphicon(TableBack, StopsB.Reverse().ToArray(), null, false);
            
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
                    RenderGraphicon(TableFront.Concat(TableBack), StopsF, null, false);
                }
                else
                {
                    if (chbToursF.Checked)
                    {
                        RenderGraphicon(TableFront.Concat(TableBack), StopsF, StopsDistsF[slidToursF.Value], false);
                    }
                    else
                    {
                        RenderGraphicon(TableFront.Concat(TableBack), StopsB, StopsDistsB[slidToursB.Value], true);
                    }
                }
            }
            else
            {
                MessageBox.Show("Linky nemají protichůdné zastávky, nebo zastávky ještě nejsou načteny.", "Chybný vstup", MessageBoxButtons.OK);
            }
            //RenderGraphicon(ArrayExtension.Enumerate<ConnectionGroup>(Tables));

        }
        private void btnLoadDistsF_Click(object sender, EventArgs e)
        {
            chbToursF.Enabled = false;
            if (!TimeTableF.GetKilometrageTable(out string[][] kilometrage))
            {
                MessageBox.Show("Vstup nemá správný formát!", "Chybný vstup", MessageBoxButtons.OK);
                return;
            }
                
            if(!TimeTableF.ExtractKilometragesFromTable(kilometrage, 0.5, false, out Zastavka[][]Zastavky))
            {
                MessageBox.Show("Vzdálenosti nešlo dobře najít", "Chybný vstup", MessageBoxButtons.OK);
                return;
            }
            this.StopsDistsF = Zastavky;
            chbToursF.Enabled = true;
            slidToursF.Maximum = StopsDistsF.Length-1;
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

            if (!TimeTableB.ExtractKilometragesFromTable(kilometrage, 0.5, true, out Zastavka[][] Zastavky))
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
    }
}
