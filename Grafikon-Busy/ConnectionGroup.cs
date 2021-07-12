﻿using System;
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

        /// <summary>
        /// Enumerate 2D array by rows
        /// </summary>
        /// <typeparam name="T">Type of items in the array</typeparam>
        /// <param name="arr">2D array</param>
        /// <returns></returns>
        public static IEnumerable<T> Enumerate<T>(this T[,] arr)
        {
            foreach (var item in arr)
                yield return item;
        }
        /// <summary>
        /// Compares arrays on equality by each element
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="compared"></param>
        /// <returns></returns>
        public static bool Equals<T>(this T[]target, T[] compared)
        {
            if (target.Length != compared.Length)
                return false;
            for (int i = 0; i < target.Length; ++i)
            {
                if (!(target[i].Equals(compared[i])))
                    return false;
            }
            return true;
        }
        /// <summary>
        /// Compares arrays on equality by mirrored elements
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="compared"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Finds highest value in the array and turns all other (positive) values into complement of that one
        /// </summary>
        /// <param name="target"></param>
        public static void MirrorPositives(this int[] target)
        {
            int max = int.MinValue;
            for(int i = 0; i < target.Length; ++i)
            {
                if (target[i] > max) max = target[i];
            }
            if (max < 0)
                return;
            for (int i = 0; i < target.Length; ++i)
            {
                if (target[i] >= 0)
                    target[i] = max - target[i];
            }
        }
        /// <summary>
        /// Finds highest value in the array and turns all other (positive) values into complement of that one
        /// </summary>
        /// <param name="target"></param>
        public static void MirrorPositives(this double[] target)
        {
            double max = double.MinValue;
            for (int i = 0; i < target.Length; ++i)
            {
                if (target[i] > max) max = target[i];
            }
            if (max < 0)
                return;
            for (int i = 0; i < target.Length; ++i)
            {
                if (target[i] >= 0)
                    target[i] = max - target[i];
            }
        }
    }
    public struct Stop
    {
        public int Order;
        public double Distance;
    }

    public static class ArrayCalculations
    {
        /// <summary>
        /// In a sorted array, spreads values that are too close together, so their arithmetic mean stays the same. 
        /// Does not fix numbers that came too close to each other during this algorithm.
        /// </summary>
        /// <param name="arr">Input array, changed in-place</param>
        /// <param name="minDiff">Required difference between numbers in original array</param>
        public static void NormalizeA(ref double[] arr, double minDiff, int iterations)
        {
            bool finished = true;
            int firstInd = 0;
            int secondInd = 1;
            while (secondInd < arr.Length)
            {
                double differ = arr[secondInd] - arr[firstInd];
                if(0 <= differ && differ < minDiff && secondInd < arr.Length)
                {
                    finished = false;
                    double avg = arr[firstInd];
                    while (0 <= differ && differ < minDiff)
                    {
                        avg += arr[secondInd];
                        secondInd++;
                        if (secondInd >= arr.Length)
                            break;
                        differ = arr[secondInd] - arr[firstInd];
                        
                    }
                    int count = secondInd - firstInd;
                    avg /= count;
                    double median = firstInd - 1 + (count + 1) / 2.0;
                    for (int d = firstInd; d < secondInd; ++d)
                    {
                        arr[d] = avg + (d - median) * minDiff;
                    }
                    firstInd = secondInd-1;
                }
                else
                {
                    firstInd++;
                    secondInd++;
                }
            }
            if (!finished && iterations < arr.Length)
            {
                NormalizeA(ref arr, minDiff, iterations+1);
            }
            return;
        }
        public static double[] Normalize(int[] arr, double minDiff)
        {
            double[] result = new double[arr.Length];
            List<double> tempResultList = new List<double>();
            List<int> tempIdx = new List<int>();
            for (int i = 0; i < arr.Length; ++i)
            {
                result[i] = (double)(arr[i]);
                if(arr[i] >= 0)
                {
                    tempResultList.Add(arr[i]);
                    tempIdx.Add(i);
                }
            }
            var tempResult = tempResultList.ToArray();
            NormalizeA(ref tempResult, minDiff, 0);
            /*for(int spread = 1; spread < tempResult.Length; ++spread)
            {
                for(int starting = 0; starting + spread < tempResult.Length; ++starting)
                {
                    NormalizeX(ref tempResult, starting, starting + spread, minDiff);
                }
            }*/
            for(int i = 0; i + 1 < tempResult.Length; ++i)
            {
                if(tempResult[i] < 0)
                    tempResult[i] = 0;
                tempResult[i + 1] = Math.Max(tempResult[i] + minDiff, tempResult[i + 1]);
            }


            for(int i = 0; i < tempResult.Length; ++i)
            {
                result[tempIdx[i]] = tempResult[i];
            }
            return result;
        }
        public static void NormalizeX(ref double[]arr, int start, int stop, double minDiff)
        {
            double minimum = arr[start];
            double final = arr[stop];
            double maxdif = (stop - start) * minDiff;
            if (start == stop)
            {
                return;
            }
            else if (minimum + maxdif > final) //If the spacing is not tight enough
            {
                for(int i = 1; i <= stop-start; ++i)
                {
                    arr[i + start] = minimum + i * minDiff;
                }
                return;
            }
            /*else
            {
                if(NormalizeX(ref arr, start, stop - 1, minDiff))
                {
                    NormalizeX(ref arr, stop, arr.Length - 1, minDiff);
                    return true;
                }
                else
                {
                    return false;
                }
            }*/
            
        }
        public static bool Equals<T>(this T[] original, T[] compared)
        {
            if (compared is null)
                return false;
            if (original.Length != compared.Length)
                return false;
            for (int i = 0; i < original.Length; ++i)
            {
                if (!original[i].Equals(compared[i]))
                    return false;
            }
            return true;
        }
        /// <summary>
        /// Checks whether the array is a continous substring of the right array 
        /// (e.g. {2,3,4} is a substr of {0,1,2,3,4,5})
        /// </summary>
        /// <param name="subString">Substring to be checked</param>
        /// <param name="fullString">Original string to be checked</param>
        /// <returns>True if first array is not empty/null and is a substring of the second array, otherwise false</returns>
        public static bool IsContinousSubstring<T>(T[]subString, T[]fullString, out int i, out int j)
        {
            i = 0;
            j = 0;
            if (subString is null || subString.Length == 0)
                return false;
            i = 0;
            T start = subString[0];
            while (!fullString[i].Equals(start))
                i++;
            j = fullString.Length - 1;
            T end = subString[subString.Length - 1];
            while (!fullString[j].Equals(end))
                j--;
            if (i >= j)
                return false;
            T[] checkArray = new T[j - i];
            Array.Copy(fullString,i,checkArray,0,j-i);
            return subString.Equals(checkArray);
        }
    }
    
}
