using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafikon_Busy
{
    public class ConnectionGroup
    {
        public Dictionary<string, string[]> Connections;
        public Color ChartColor;
        public bool Direction = true;
        private bool enabled;
        public bool Enabled
        {
            get
            {
                if (this.Connections == null)
                    return false;
                else return enabled;
            }
            set
            {
                this.enabled = value;
            }
        }
        /*public ConnectionGroup(Dictionary<string, string[]> conn, Color color)
        {
            this.Connections = conn;
            this.ChartColor = color;
        }*/
        public ConnectionGroup(Dictionary<string, string[]> conn, Color color, bool dir)
        {
            this.Connections = conn;
            this.ChartColor = color;
            this.Direction = dir;
        }
    }
    public static class ArrayExtension
    {
        public static IEnumerable<T> Enumerate<T>(this T[,] target)
        {
            foreach (var item in target)
                yield return item;
        }
        public static bool IsInverseOf<T>(this T[] target, T[] compared)
        {
            if (target.Length != compared.Length)
                return false;
            for(int i = 0; i < target.Length; ++i)
            {
                if (!(target[i].Equals(compared[compared.Length - 1 - i])))
                    return false;
            }
            return true;
        }
    }
    public struct Zastavka
    {
        public int order;
        public double distance;
    }
    public static class ArrayCalculations
    {
        public static double[] Normalize(int[] arr, double minDiff)
        {
            double[] result = new double[arr.Length];
            for (int i = 0; i < arr.Length; ++i)
            {
                result[i] = (double)arr[i];
            }

            int firstInd = 0;
            int secondInd = 1;
            while (secondInd < arr.Length)
            {
                double differ = arr[secondInd] - arr[firstInd];
                if(0 <= differ && differ < minDiff && secondInd < arr.Length)
                {
                    while (0 <= differ && differ < minDiff)
                    {
                        secondInd++;
                        if (secondInd >= arr.Length)
                            break;
                        differ = arr[secondInd] - arr[firstInd];
                        
                    }
                    int count = secondInd - firstInd;
                    double median = firstInd - 1 + (count + 1) / 2.0;
                    for (int d = firstInd; d < secondInd; ++d)
                    {
                        result[d] += (d - median) * minDiff;
                        if (result[d] - result[d - 1] < minDiff)
                        {
                            result[d] = result[d - 1] + minDiff;
                        }
                    }
                    firstInd = secondInd-1;
                }
                else
                {
                    firstInd++;
                    secondInd++;
                }
            }
            return result;
        }
        public static double[] Normalize(double[] arr, double minDiff)
        {
            double[] result = new double[arr.Length];
            for (int i = 0; i < arr.Length; ++i)
            {
                result[i] = (double)arr[i];
            }

            int firstInd = 0;
            int secondInd = 1;
            while (secondInd < arr.Length)
            {
                double differ = arr[secondInd] - arr[firstInd];
                if (0 <= differ && differ < minDiff && secondInd < arr.Length)
                {
                    while (0 <= differ && differ < minDiff)
                    {
                        secondInd++;
                        if (secondInd >= arr.Length)
                            break;
                        differ = arr[secondInd] - arr[firstInd];

                    }
                    int count = secondInd - firstInd;
                    double median = firstInd - 1 + (count + 1) / 2.0;
                    for (int d = firstInd; d < secondInd; ++d)
                    {
                        result[d] += (d - median) * minDiff;
                        if (result[d] - result[d - 1] < minDiff)
                        {
                            result[d] = result[d - 1] + minDiff;
                        }
                    }
                    firstInd = secondInd - 1;
                }
                else
                {
                    firstInd++;
                    secondInd++;
                }
            }
            return result;
        }
    }
    
}
