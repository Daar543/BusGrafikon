using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafikon_Busy
{
    class TimeTableParser
    {
        /// <summary>
        /// The timetable is transposed - one connection is in one row, the first row contains all stops
        /// </summary>
        private string[][] TimeTable;
        private string[][] ReducedTable = null;

        const char WorkdaySign = 'X';
        const char SaturdaySign = '6';
        const char SundaySign = '+';
        static string DistanceSign = "km";
        static char[] SignSeparators = new char[] { ' ' };
        public string[] HolidayPositiveSigns;
        public string[] HolidayNegativeSigns;
        public TimeTableParser(string[][] tt)
        {
            this.TimeTable = tt;
        }
        public TimeTableParser(string[][] tt, string HolidayPositive, string HolidayNegative)
        {
            this.TimeTable = tt;
            this.HolidayPositiveSigns = HolidayPositive.Split(SignSeparators);
            this.HolidayNegativeSigns = HolidayNegative.Split(SignSeparators);
        }
        public string[][] Cutout()
        {
            string[][] origTable = this.TimeTable;
            List<int> consideredRows = new List<int>();
            int validRow = 0;
            int firstCol = 0;
            int lastCol = 0;
            int stopCount = 0;

            for (validRow = 0; validRow < origTable.Length; ++validRow)
            {
                if (consideredRows.Count == 0)
                {
                    for (firstCol = 0; firstCol < origTable[validRow].Length; ++firstCol)
                    {
                        if (origTable[validRow][firstCol] == "Tč")
                        {
                            consideredRows.Add(validRow);
                            consideredRows.Add(validRow + 1);
                            for (lastCol = origTable[validRow].Length - 1; lastCol >= firstCol; --lastCol)
                            {
                                if (int.TryParse(origTable[validRow][lastCol], out stopCount))
                                    break;
                            }
                            break;
                        }
                    }
                }
                else if (origTable[validRow][firstCol] != "")
                {
                    consideredRows.Add(validRow);
                }
            }
            string[][] resultTable = new string[consideredRows.Count][];
            int i = 0;
            foreach (int row in consideredRows)
            {
                resultTable[i] = new string[lastCol - firstCol + 1];
                for (int j = 0; j < resultTable[i].Length; ++j)
                {
                    resultTable[i][j] = origTable[row][j + firstCol];
                }
                ++i;
            }
            return resultTable;
        }
        public bool GetStops(out string[]stops)
        {
            stops = null;
            if (ReducedTable == null)
                ReducedTable = this.Cutout();
            try
            {
                stops = new string[ReducedTable[1].Length - 2];
                Array.Copy(ReducedTable[1], 2, stops, 0, stops.Length);
            }
            catch
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Gets all kilometrage tours from the reduced table
        /// </summary>
        /// <param name="table"></param>
        /// <param name="kms"></param>
        /// <returns></returns>
        public bool GetKilometrageTable(out string[][] kms)
        {
            if (ReducedTable == null)
                ReducedTable = this.Cutout();
            List<int> AllowedRows = new List<int>();
                AllowedRows.Add(1); //Stops
            for (int i = 2; i < ReducedTable.Length; ++i)
            {
                if (ReducedTable[i][0].Contains(DistanceSign) || ReducedTable[i][1].Contains(DistanceSign))
                {
                    AllowedRows.Add(i);
                }
            }
            kms = new string[AllowedRows.Count][];
            if (AllowedRows.Count == 0)
                return false;

            int j = 0;
            foreach (int row in AllowedRows)
            {
                kms[j] = new string[ReducedTable[row].Length];
                Array.Copy(ReducedTable[row], kms[j], ReducedTable[row].Length);
                ++j;
            }
            return true;
        }
        public bool ExtractKilometragesFromTable(string[][]table, double normalizingDistance, out Zastavka[][]toursNorm)
        {
            int[][] tours = new int[table.Length - 1][];
            int len = table[0].Length;
            int diff = 0;
            int i = 0;
            for(; i<table[0].Length && table[0][i] == ""; ++i)
            {
                len--;
                diff++;
            }
            
            for(int j = 0; j < tours.Length; ++j)
            {
                tours[j] = new int[len];
            }
            for(int j = 0; j < tours.Length; ++j)
            {
                for(i = diff; i < table[0].Length; ++i)
                {
                    if (int.TryParse(table[j+1][i], out int dist))
                        tours[j][i - diff] = dist;
                    else
                        tours[j][i - diff] = -1;
                }
            }



            toursNorm = new Zastavka[tours.Length][];
            if (len == 0)
                return false;
            for (int j = 0; j < tours.Length; ++j)
            {
                double[] toursNormA = ArrayCalculations.Normalize(tours[j], normalizingDistance);
                var zst = new List<Zastavka>();
                for (int k = 0; k < len; ++k)
                {
                    if (toursNormA[k] >= 0)
                    {
                        zst.Add(new Zastavka { Order = k, Distance = toursNormA[k] });
                    }
                }
                toursNorm[j] = zst.ToArray();
            }
            return true;
        }
        /// <summary>
        /// Parses the weekday/holiday.. table into array of stops and dictionary of connections->times
        /// </summary>
        /// <param name="table">Initial table, already without ballast</param>
        /// <param name="stops">All stops on the way</param>
        /// <param name="connections">Dictionary, where keys are connection names, and values are array of times (of stopping at given stops)</param>
        /// <returns>Indicator if the conversion has fully succeeded</returns>
        public bool CreateStopsAndConnections(string[][] table, out string[] stops, out Dictionary<string, string[]> connections)
        {
            stops = new string[table[0].Length - 2];
            connections = new Dictionary<string, string[]>();

            try
            {
                Array.Copy(table[0], 2, stops, 0, stops.Length);
                for (int i = 1; i < table.Length; ++i)
                {
                    string[] times = new string[table[i].Length - 2];
                    string name = table[i][0];
                    Array.Copy(table[i], 2, times, 0, times.Length);
                    connections.Add(name, times);
                }
                return true;
            }
            catch (Exception ex)
            {
                if (ex is IndexOutOfRangeException || ex is ArgumentException)
                    return false;
                else
                    throw;
            }

        }
        public string[][] CreateWorkdayTable()
        {
            if (ReducedTable == null)
                ReducedTable = this.Cutout();

            List<int> AllowedRows = new List<int>();
            AllowedRows.Add(1); //Second row contains stops

            for (int i = 2; i < ReducedTable.Length; ++i)
            {
                if (ReducedTable[i][1].Contains(WorkdaySign))
                {
                    AllowedRows.Add(i);
                }
            }
            string[][] newTable = new string[AllowedRows.Count][];
            int j = 0;
            foreach (int row in AllowedRows)
            {
                newTable[j] = new string[ReducedTable[row].Length];
                Array.Copy(ReducedTable[row], newTable[j], ReducedTable[row].Length);
                ++j;
            }
            return newTable;
        }
        public string[][] CreateWorkHolidayTable()
        {
            if(ReducedTable == null)
                ReducedTable = this.Cutout();
            List<int> AllowedRows = new List<int>();
            AllowedRows.Add(1); //Second row contains stops

            for (int i = 2; i < ReducedTable.Length; ++i)
            {
                bool allowed = true;
                if (ReducedTable[i][1].Contains(WorkdaySign))
                {
                    foreach (string forbiddenString in HolidayNegativeSigns)
                    {
                        if (forbiddenString == "")
                            continue;
                        if (ReducedTable[i][1].Contains(forbiddenString))
                        {
                            allowed = false;
                            break;
                        }
                    }
                }
                else
                {
                    allowed = false;
                    foreach(string allowedString in HolidayPositiveSigns)
                    {
                        if(allowedString == "")
                            continue;
                        if (ReducedTable[i][1].Contains(allowedString))
                        {
                            allowed = true;
                            break;
                        }
                    }
                }
                if(allowed)
                    AllowedRows.Add(i);
            }
            string[][] newTable = new string[AllowedRows.Count][];
            int j = 0;
            foreach (int row in AllowedRows)
            {
                newTable[j] = new string[ReducedTable[row].Length];
                Array.Copy(ReducedTable[row], newTable[j], ReducedTable[row].Length);
                ++j;
            }
            return newTable;
            throw new NotImplementedException();
        }
        public string[][] CreateSaturdayTable()
        {
            if (ReducedTable == null)
                ReducedTable = this.Cutout();

            List<int> AllowedRows = new List<int>();
            AllowedRows.Add(1); //Second row contains stops

            for (int i = 2; i < ReducedTable.Length; ++i)
            {
                if (ReducedTable[i][1].Contains(SaturdaySign))
                {
                    AllowedRows.Add(i);
                }
            }
            string[][] newTable = new string[AllowedRows.Count][];
            int j = 0;
            foreach (int row in AllowedRows)
            {
                newTable[j] = new string[ReducedTable[row].Length];
                Array.Copy(ReducedTable[row], newTable[j], ReducedTable[row].Length);
                ++j;
            }
            return newTable;
        }
        public string[][] CreateSundayTable()
        {
            if (ReducedTable == null)
                ReducedTable = this.Cutout();

            List<int> AllowedRows = new List<int>();
            AllowedRows.Add(1); //Second row contains stops

            for (int i = 2; i < ReducedTable.Length; ++i)
            {
                if (ReducedTable[i][1].Contains(SundaySign))
                {
                    AllowedRows.Add(i);
                }
            }
            string[][] newTable = new string[AllowedRows.Count][];
            int j = 0;
            foreach (int row in AllowedRows)
            {
                newTable[j] = new string[ReducedTable[row].Length];
                Array.Copy(ReducedTable[row], newTable[j], ReducedTable[row].Length);
                ++j;
            }
            return newTable;
        }
        public string[][] CreateNationalHolidayTable()
        {
            if (ReducedTable == null)
                ReducedTable = this.Cutout();
            throw new NotImplementedException();
        }
    }
}
