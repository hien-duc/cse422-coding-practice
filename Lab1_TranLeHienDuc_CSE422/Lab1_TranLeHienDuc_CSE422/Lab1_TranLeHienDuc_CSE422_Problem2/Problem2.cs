using System;

namespace Lab1_TranLeHienDuc_CSE422.Problem2
{
    public class Problem2
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Enter the dividend:");
                int dividend = int.Parse(Console.ReadLine() ?? "0");
                Console.WriteLine("Enter the divisor:");
                int divisor = int.Parse(Console.ReadLine() ?? "0");

                if (divisor == 0)
                {
                    Console.WriteLine("Error: Cannot divide by zero");
                    return;
                }

                int result = FindDivide(dividend, Math.Abs(divisor));
                result = divisor < 0 ? -result : result;
                
                Console.WriteLine($"The result: {result}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Please enter valid numbers");
            }
        }

        static int FindDivide(int dividend, int divisor)
        {
            if (divisor == 0) return 0;
            
            int result = 0;
            int absDividend = Math.Abs(dividend);
            
            while (absDividend >= divisor)
            {
                absDividend -= divisor;
                result++;
            }
            
            return dividend < 0 ? -result : result;
        }
    }
}
