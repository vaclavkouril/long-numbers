using System;
using System.Reflection.Metadata;

namespace Long_Numbers
{
    public class LongNumber
    {
        static int[] Primes = {2,3,5,7,1000012337};
        static int modulo(String num, int a)
        {
            int result = 0;
            for (int i = 0; i < num.Length; i++)
            {
                result = (result * 10 + num[i] - '0') % a;
            }
            return result;
        }

        private int[] vector = new int[Primes.Length]; 
        public LongNumber(string bigNum)
        {
            for (int i = 0; i < Primes.Length; i++)
            {
                vector[i] = modulo(bigNum, Primes[i]);
            }
        }
        public LongNumber Plus(LongNumber a, LongNumber b)
        {
            Console.WriteLine("_");
            return a;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            LongNumber a = new LongNumber("10000123378483919101091398485u75202");
            Console.ReadLine();
        }
    }
}