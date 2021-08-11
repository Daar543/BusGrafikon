using System;
using System.Collections.Generic;
using System.IO;

namespace Grafikon_Busy
{
    class SheetLoader
    {

        /// <summary>
        /// Reads the values from excel, loads into 2D jagged array of strings
        /// </summary>
        /// <param name="filename">CSV filename</param>
        /// <param name="separator">Character for separating values in CSV file (usually TAB)</param>
        /// <returns></returns>
        public static string[][] ReadExcelInput(string filename, char separator='\t')
        {
            string line;
            string[] row;
            List<string[]> table = new List<string[]>();
            StreamReader sr = new StreamReader(filename);

            while (true)
            {
                line = sr.ReadLine();
                if (line == null) { break; }
                else if (line == "") { continue; }
                //Empty row eliminated
                row = line.Split(separator);
                table.Add(row);
            }
            return table.ToArray();
        }
        /// <summary>
        /// Transposes table while also removing empty rows and padding columns
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static string[][] RowifyTable(string[][] table)
        {
            int maxLen = 0;
            for (int i = 0; i < table.Length; ++i)
            {
                maxLen = Math.Max(table[i].Length, maxLen);
            }
            var newTable = new List<string[]>();
            for (int j = 0; j < maxLen; ++j)
            {
                bool empty = true;
                for (int i = 0; i < table.Length; ++i)
                {
                    if (j < table[i].Length && (table[i][j] != ""))
                    {
                        empty = false;
                        break;
                    }
                }
                //Ingore empty column
                if (empty)
                {
                    continue;
                }


                List<string> col = new List<string>();

                for (int i = 0; i < table.Length; ++i)
                {
                    //Pad column
                    if (j >= table[i].Length)
                    {
                        col.Add("");
                    }
                    else
                    {
                        col.Add(table[i][j]);
                    }
                }
                newTable.Add(col.ToArray());
            }
            return newTable.ToArray();

        }
    }
}
