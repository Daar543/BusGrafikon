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
        public Dictionary<string, string[]> Connections { get; private set; }
        public Color ChartColor { get; private set; }
        public bool Direction { get; private set; }
        public string Name { get; private set; }
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
        public ConnectionGroup(Dictionary<string, string[]> conn, Color color, bool dir)
        {
            this.Connections = conn;
            this.ChartColor = color;
            this.Direction = dir;
        }
        
    }
    public struct Stop
    {
        public int Order;
        public double Distance;
        public static Stop[] InvertDistances(string[] names, Stop[] original)
        {
            Stop[] result = new Stop[original.Length];
            for(int i = 0; i < original.Length; ++i)
            {
                int order = original[i].Order;
                double distance = original[original.Length-i-1].Distance;
                result[i] = new Stop { Order = order, Distance = distance };
            }
            return result;
        }
        
    }

    public static class ArrayExtensions
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
        /// Checks whether the array is a continous substring of the right array 
        /// (e.g. {2,3,4} is a substr of {0,1,2,3,4,5})
        /// </summary>
        /// <param name="subString">Substring to be checked</param>
        /// <param name="fullString">Original string to be checked</param>
        /// <returns>True if first array is not empty/null and is a substring of the second array, otherwise false</returns>
        public static bool IsContinousSubstring<T>(T[] subString, T[] fullString, out int i, out int j)
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
            Array.Copy(fullString, i, checkArray, 0, j - i);
            return subString.Equals(checkArray);
        }
    }
    

    public static class ArrayCalculations
    {
        /// <summary>
        /// Finds highest value in the array and turns all other (positive) values into complement of that one
        /// </summary>
        /// <param name="target"></param>
        public static void MirrorPositives(this int[] target)
        {
            int max = int.MinValue;
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
        /// <summary>
        /// In a sorted array, spreads values that are too close together, so their arithmetic mean stays the same. 
        /// </summary>
        /// <param name="arr">Input array, changed in-place</param>
        /// <param name="minDiff">Required difference between numbers in original array</param>
        /// /// <param name="iterations">How many times will the whole sequence be re-arranged (due to fix the mistakes in previous iteration)</param>
        public static void NormalizeA(ref double[] arr, double minDiff, int iterations)
        {
            bool finished = true;
            int firstInd = 0;
            int secondInd = 1;

            //Idea: Find the longest sequence of numbers which are too close, and rearrange them
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
        /// <summary>
        /// Scales the array, so the last element is equal to maximumDistance, and also enforces minimal difference between the elements
        /// </summary>
        /// <param name="arr">Array to scale</param>
        /// <param name="minDiff">Minimum difference between adjanced elements</param>
        /// <param name="maximumDistance">What will the maximum element (last) be scaled to; set 0 to disable scaling</param>
        /// <returns>Rescaled array</returns>
        public static double[] Normalize(int[] arr, double minDiff, int maximumDistance)
        {
            double[] result = new double[arr.Length];
            List<double> tempResultList = new List<double>();
            List<int> tempIdx = new List<int>();

            int normalizingRatio = 1;
            if(maximumDistance > 0)
                normalizingRatio = maximumDistance / arr[arr.Length - 1];
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

            //As the "real" distance between stops is irrelevant, the unscaled numbers are not saved anywhere
            NormalizeA(ref tempResult, minDiff/normalizingRatio, 0);
            //Return the distances to their original size; they will be rescaled once it's time
            for (int i = 0; i + 1 < tempResult.Length; ++i)
            {
                if(tempResult[i] < 0)
                    tempResult[i] = 0;

                //If the distances could not be spread properly, shift the distances of following stops by the difference
                tempResult[i + 1] = Math.Max(tempResult[i] + minDiff/normalizingRatio, tempResult[i + 1]);
            }
            //The normalization is done at the end - so if the last element gets pushed away, it needs to get fixed
            if(normalizingRatio!=1)
            for(int i = 0; i < tempResult.Length; ++i)
            {
                tempResult[i] *= normalizingRatio;
            }


            for(int i = 0; i < tempResult.Length; ++i)
            {
                result[tempIdx[i]] = tempResult[i];
            }
            return result;
        }
    }
    
}
