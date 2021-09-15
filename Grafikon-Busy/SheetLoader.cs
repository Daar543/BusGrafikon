using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic.FileIO;

namespace Grafikon_Busy
{
    class SheetLoader
    {

        /// <summary>
        /// Reads CSV with quoted fields (and no other quotes inside) - this allows for easier parsing, ignoring separators
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="separator"></param>
        /// <param name="endline"></param>
        /// <returns></returns>
        public static string[][] ReadQuotedCSV(string filename, char separator = ',', char endline = ';')
        {
            string sep = separator.ToString();
            string endl = endline.ToString();
            List<string[]> data = new List<string[]>();
            using (var sr = new StreamReader(filename, System.Text.Encoding.UTF8))
            {
                while (true)
                {
                    var line = sr.ReadLine();
                    if (line == null) break;
                    else if (line == "") continue;
                    var row = line.Split('\"');
                    var modRow = new List<string>();
                    for (int i = 1; i < row.Length; ++i) //Skip the first item, as the line starts with quotes
                    {
                        string x = row[i];
                        if (x == sep)
                            continue;
                        else if (x == endl)
                            break;
                        else
                            modRow.Add(x);
                    }
                    data.Add(modRow.ToArray());
                }

            }

            return data.ToArray();
        }

        /// <summary>
        /// Reads the values from excel, loads into 2D jagged array of strings
        /// </summary>
        /// <param name="filename">CSV filename</param>
        /// <param name="separator">Character for separating values in CSV file (usually TAB)</param>
        /// <returns></returns>
        public static string[][] ReadSheetInput(string filename, char separator='\t')
        {
            string line;
            string[] row;
            List<string[]> table = new List<string[]>();
            StreamReader sr = new StreamReader(filename,System.Text.Encoding.UTF8);

            while (true)
            {
                line = sr.ReadLine();
                if (line == null) { break; }
                else if (line == "") { continue; }
                //Empty row eliminated
                row = line.Split(separator);
                for(int i = 0; i<row.Length;++i)
                {
                    row[i] = Regex.Replace(row[i], "\"", string.Empty);
                }
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
