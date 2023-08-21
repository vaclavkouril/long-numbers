using System;
using System.Numerics;

namespace Long_Numbers
{
    public class LongNumber
    {
        public static long[] Primes = { (1<<30)-1,(1<<29)-1,(1<<27)-1, (1<<23)-1, (1<<19)-1 };
        public long[] vector = new long[Primes.Length];
        private byte sign = 0;

        private static long modulo(long dividend, long divisor)
        {
            return (dividend % divisor + divisor) % divisor;
        }

        static long modulo(String num, long a)
        {
            long result = 0;
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
                bigNum.Remove(0, 1);
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

        public BigInteger ToBigInteger()
        {
            BigInteger result = vector[0];
            while (true)
            {
                for (int j = 1; j < Primes.Length; j++)
                {
                    if (result % Primes[j] != vector[j]) break;
                    if (j == Primes.Length - 1) return result;
                }

                result += Primes[0];
            }
        }
        
        
        // Operators

        public static LongNumber operator +(LongNumber num1, LongNumber num2)
        {
            LongNumber num3 = new LongNumber("0");
            for (int i = 0; i < Primes.Length; i++)
            {
                num3.vector[i] = modulo((num1.vector[i] + num2.vector[i]), Primes[i]);
            }

            return num3;
        }

        public static LongNumber operator -(LongNumber num1, LongNumber num2)
        {
            LongNumber num3 = new LongNumber("0");
            for (int i = 0; i < Primes.Length; i++)
            {
                num3.vector[i] = modulo((num1.vector[i] - num2.vector[i]), Primes[i]);
            }

            return num3;
        }

        public static LongNumber operator *(LongNumber num1, LongNumber num2)
        {
            LongNumber num3 = new LongNumber("0");
            for (int i = 0; i < Primes.Length; i++)
            {
                num3.vector[i] = modulo((num1.vector[i] * num2.vector[i]), Primes[i]);
            }

            return num3;
        }

    }

    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            LongNumber a = new LongNumber("10000123378483919101091398485123345765432765432543254325432543254324375202");
            LongNumber b = new LongNumber("-10000123378483919101091398485752022134932038410847104872109389028310");
            LongNumber c = a + b;
            LongNumber d = a * b * c;
            LongNumber e = (d - a) * c;
            */
            LongNumber f = new LongNumber("620100000");
            Console.WriteLine(f.ToBigInteger().ToString());
            Console.ReadLine();
        }
    }
}