using System;
using System.Numerics;
using System.Reflection.Metadata;

namespace Long_Numbers
{
    public class LongNumber
    {
        static int[] Primes = {2,3,5,7,1000012337};
        private int[] vector = new int[Primes.Length];
        private byte sign = 0;
        private static int modulo(int dividend, int divisor)
        {
            return (dividend % divisor+divisor) % divisor;
        }
        static int modulo(String num, int a)
        {
            int result = 0;
            for (int i = 0; i < num.Length; i++)
            {
                result = (result * 10 + num[i] - '0') % a;
            }
            return result;
        }
        public LongNumber(string bigNum)
        {
            if (bigNum[0] == '-')
            {
                sign = 1;
                bigNum.Remove(0,1);
            }
            for (int i = 0; i < Primes.Length; i++)
            {
                vector[i] = modulo(bigNum, Primes[i]);
            }
        }

        public string ToString()
        {
            long product = 1;
            foreach (long prime in Primes)
            {
                product *= prime;
            }

            long[] resultDigits = new long[product.ToString().Length];

            for (int i = 0; i < Primes.Length; i++)
            {
                long pi = product / Primes[i];
                long xi = ModularMultiplicativeInverse(pi, Primes[i]);
                long contribution = vector[i] * xi * pi;

                for (int j = 0; j < resultDigits.Length; j++)
                {
                    resultDigits[j] += contribution % 10;
                    contribution /= 10;
                }
            }
            
            for (int i = 0; i < resultDigits.Length - 1; i++)
            {
                resultDigits[i + 1] += resultDigits[i] / 10;
                resultDigits[i] %= 10;
            }

            // Sestav výsledný řetězec
            string result = "";
            for (int i = resultDigits.Length - 1; i >= 0; i--)
            {
                result += resultDigits[i];
            }

            return result; 
        }

        static long ModularMultiplicativeInverse(long a, long mod)
        {
            for (long x = 1; x < mod; x++)
            {
                if ((a * x) % mod == 1)
                {
                    return x;
                }
            }
            return 1;
        }
        
        public static LongNumber operator + (LongNumber num1, LongNumber num2)
        {
            LongNumber num3 = new LongNumber("0");
            for (int i = 0; i < Primes.Length; i++)
            {
                num3.vector[i] = modulo((num1.vector[i] + num2.vector[i]),Primes[i]);
            }
            return num3;
        }
        
        public static LongNumber operator - (LongNumber num1, LongNumber num2)
        {
            LongNumber num3 = new LongNumber("0");
            for (int i = 0; i < Primes.Length; i++)
            {
                num3.vector[i] = modulo((num1.vector[i] - num2.vector[i]),Primes[i]);
            }
            return num3;
        }
        public static LongNumber operator * (LongNumber num1, LongNumber num2)
        {
            LongNumber num3 = new LongNumber("0");
            for (int i = 0; i < Primes.Length; i++)
            {
                num3.vector[i] = modulo((num1.vector[i] * num2.vector[i]),Primes[i]);
            }
            return num3;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            LongNumber a = new LongNumber("10000123378483919101091398485123345765432765432543254325432543254324375202");
            LongNumber b = new LongNumber("-10000123378483919101091398485752022134932038410847104872109389028310");
            LongNumber c = a + b;
            LongNumber d = a * b * c;
            LongNumber e = (d - a) * c;
            Console.WriteLine(e.ToString());
            Console.ReadLine();
        }
    }
}