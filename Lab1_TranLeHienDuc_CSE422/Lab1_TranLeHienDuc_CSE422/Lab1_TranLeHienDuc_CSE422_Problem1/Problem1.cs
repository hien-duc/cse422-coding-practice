using System;

namespace Lab1_TranLeHienDuc_CSE422.Problem1
{
    public class Problem1
    {
        private const int _maxTotalLength = 2000;
        private const int _maxValue = 1000000;

        static void Main(string[] args)
        {
            try
            {
                int firstLength = GetArrayLength("first");
                int secondLength = GetArrayLength("second");
                ValidateArrayLengths(firstLength, secondLength);

                int[] firstArray = ReadArray("first", firstLength);
                int[] secondArray = ReadArray("second", secondLength);

                double median = FindMedian(firstArray, secondArray);
                Console.WriteLine($"Median: {median:F5}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Please enter valid numbers.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static int GetArrayLength(string arrayName)
        {
            Console.WriteLine($"Enter the length of {arrayName} array:");
            return int.Parse(Console.ReadLine() ?? "0");
        }

        static void ValidateArrayLengths(int firstLength, int secondLength)
        {
            int totalLength = firstLength + secondLength;
            if (totalLength < 1 || totalLength > _maxTotalLength)
            {
                throw new ArgumentException($"Sum of array lengths must be between 1 and {_maxTotalLength}");
            }
        }

        static int[] ReadArray(string arrayName, int length)
        {
            Console.WriteLine($"Enter {length} space-separated elements for {arrayName} array:");
            string[] inputs = (Console.ReadLine() ?? string.Empty).Split(' ');

            if (inputs.Length != length)
            {
                throw new ArgumentException($"Expected {length} elements, but got {inputs.Length}");
            }

            int[] array = new int[length];
            for (int i = 0; i < length; i++)
            {
                if (!int.TryParse(inputs[i], out array[i]))
                {
                    throw new FormatException($"Invalid number at position {i + 1}");
                }

                if (Math.Abs(array[i]) > _maxValue)
                {
                    throw new ArgumentException($"Number at position {i + 1} exceeds ±{_maxValue}");
                }
            }

            return array;
        }

        static double FindMedian(int[] firstArray, int[] secondArray)
        {
            if (firstArray == null || secondArray == null)
            {
                throw new ArgumentNullException("Arrays cannot be null");
            }

            int m = firstArray.Length;
            int n = secondArray.Length;

            if (m > n)
            {
                return FindMedian(secondArray, firstArray);
            }

            int low = 0;
            int high = m;

            while (low <= high)
            {
                int partitionX = (low + high) / 2;
                int partitionY = (m + n + 1) / 2 - partitionX;

                int leftX = partitionX == 0 ? int.MinValue : firstArray[partitionX - 1];
                int rightX = partitionX == m ? int.MaxValue : firstArray[partitionX];
                int leftY = partitionY == 0 ? int.MinValue : secondArray[partitionY - 1];
                int rightY = partitionY == n ? int.MaxValue : secondArray[partitionY];

                if (leftX <= rightY && leftY <= rightX)
                {
                    if ((m + n) % 2 == 0)
                    {
                        return (Math.Max(leftX, leftY) + Math.Min(rightX, rightY)) / 2.0;
                    }
                    return Math.Max(leftX, leftY);
                }

                if (leftX > rightY)
                {
                    high = partitionX - 1;
                }
                else
                {
                    low = partitionX + 1;
                }
            }

            throw new ArgumentException("Input arrays are not sorted");
        }
    }
}