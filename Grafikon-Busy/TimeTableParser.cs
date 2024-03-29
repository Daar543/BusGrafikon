﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Grafikon_Busy
{
    static class TimeConverter
    {
        /// <summary>
        /// Converts string in format hh:mm to minutes
        /// </summary>
        /// <param name="hoursMins">String in format number:number. Each part (separated by :) might contain leading/trailing spaces and leading zeros</param>
        /// <param name="totalMinutes">If the input is in valid format, returns the hours and minutes converted to minutes only.</param>
        /// <returns>True if conversion was valid</returns>
        public static bool HoursMinsToMins(string hoursMins, out int totalMinutes)
        {
            int hours;
            int minutes;
            totalMinutes = 0;

            string[] timeString = hoursMins.Split(':');
            if (!(
                int.TryParse(timeString[0], out hours)
                && int.TryParse(timeString[1], out minutes)
                ))
                return false;
            if (!(
                0 <= hours && hours < 24 && 0 <= minutes && minutes < 60)
                )
                return false;

            totalMinutes = 60 * hours + minutes;
            return true;
        }
    }
    class TimeTableParser
    {
        public string[] HolidayNegativeSigns;

        public string[] HolidayPositiveSigns;

        const string DistanceSign = "km";

        const char SaturdaySign = '6';

        const char SundaySign = '+';

        const char WorkdaySign = 'X';

        static readonly char[] SignSeparators = new char[] { ' ' };

        /// <summary>
        /// The timetable is transposed - one connection is in one row, the first row contains all stops
        /// </summary>
        private readonly string[][] TimeTable;
        private string[][] ReducedTable = null;
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

        /// <summary>
        /// Scans through the timetable, checking only for working days w/o holiday positive signs
        /// </summary>
        /// <returns></returns>
        public string[][] CreateWorkdayTable()
        {
            if (ReducedTable == null)
            {
                ReducedTable = this.Cutout();
            }


            List<int> AllowedRows = new List<int>();
            AllowedRows.Add(1); //Second row contains stops

            for (int i = 2; i < ReducedTable.Length; ++i)
            {
                bool allowed = true;
                if (ReducedTable[i][1].Contains(WorkdaySign))
                {
                    foreach (string forbiddenString in HolidayPositiveSigns)
                    {
                        if (forbiddenString == "")
                            continue;
                        else if (ReducedTable[i][1].Contains(forbiddenString))
                        {
                            allowed = false;
                            break;
                        }
                    }
                }
                else
                {
                    allowed = false;
                }

                if (allowed)
                {
                    AllowedRows.Add(i);
                }

            }
            //Copy content from the selected rows to the new table
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
            if (ReducedTable == null)
            {
                ReducedTable = this.Cutout();
            }
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
                        else if (ReducedTable[i][1].Contains(forbiddenString))
                        {
                            allowed = false;
                            break;
                        }
                    }
                }
                else
                {
                    allowed = false;
                    foreach (string allowedString in HolidayPositiveSigns)
                    {
                        if (allowedString == "")
                            continue;
                        if (ReducedTable[i][1].Contains(allowedString))
                        {
                            allowed = true;
                            break;
                        }
                    }
                }
                if (allowed)
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="table">Reduced timetable - only kilometrages and stop names </param>
        /// <param name="spreadDistance">By how much should stops with same kilometrage differ</param>
        /// <param name="mirror">If true, the distances will be calculated as complement to the max distance</param>
        /// <param name="toursNorm">Output with normalized distances from stops (1 tour = 1 inner array)</param>
        /// <returns></returns>
        public bool ExtractKilometragesFromTable(string[][] table, double spreadDistance, int maximumDistance, bool mirror, out Stop[][] toursNorm)
        {
            int[][] tours = new int[table.Length - 1][];

            //Determine how many rows need to be scanned
            int len = table[0].Length;
            int diff = 0;
            int i = 0;
            for (; i < table[0].Length && table[0][i] == ""; ++i)
            {
                len--;
                diff++;
            }

            for (int j = 0; j < tours.Length; ++j)
            {
                tours[j] = new int[len];
            }
            for (int j = 0; j < tours.Length; ++j)
            {
                for (i = diff; i < table[0].Length; ++i)
                {
                    if (int.TryParse(table[j + 1][i], out int dist))
                        tours[j][i - diff] = dist;
                    else
                        tours[j][i - diff] = -1;
                }
            }



            toursNorm = new Stop[tours.Length][];
            if (len == 0)
                return false;


            for (int j = 0; j < tours.Length; ++j)
            {
                double[] toursNormA = ArrayCalculations.Normalize(tours[j], spreadDistance, maximumDistance);
                if (mirror)
                {
                    toursNormA.MirrorPositives();
                }
                var zst = new List<Stop>();
                for (int k = 0; k < len; ++k)
                {
                    if (toursNormA[k] >= 0)
                    {
                        zst.Add(new Stop { Order = k, Distance = toursNormA[k] });
                    }
                }
                toursNorm[j] = zst.ToArray();
            }
            return true;
        }

        /// <summary>
        /// Gets all kilometrage tours from the reduced table
        /// </summary>
        /// <param name="table"></param>
        /// <param name="kms"></param>
        /// <returns>True iff the parsing has ended succesfully</returns>
        public bool GetKilometrageTable(out string[][] kms)
        {
            if (ReducedTable == null)
            {
                ReducedTable = this.Cutout();
            }

            List<int> AllowedRows = new List<int>();
            AllowedRows.Add(1); //This is the row with stop names
            for (int i = 2; i < ReducedTable.Length; ++i)
            {
                if (ReducedTable[i][0].Contains(DistanceSign) || ReducedTable[i][1].Contains(DistanceSign)) //Add row with "km"
                {
                    AllowedRows.Add(i);
                }
            }
            kms = new string[AllowedRows.Count][];

            if (AllowedRows.Count <= 1)
            {
                return false;
            }

            int j = 0;
            foreach (int row in AllowedRows)
            {
                kms[j] = new string[ReducedTable[row].Length];
                Array.Copy(ReducedTable[row], kms[j], ReducedTable[row].Length);
                ++j;
            }
            return true;
        }
    }
}
