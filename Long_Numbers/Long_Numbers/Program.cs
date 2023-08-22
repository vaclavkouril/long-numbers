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

        private string StringNumAddition(string num1, string num2)
        {
            List<int> forLoop(char[] biggerNum, char[] smallerNum)
            {
                int carry = 0;
                List<int> result = new List<int>();
                for (int i = 0;i < smallerNum.Length; i++)
                {
                    int numeral = (int)Char.GetNumericValue(biggerNum[i]) + (int)Char.GetNumericValue(smallerNum[i]) + carry;
                    if (numeral >= 10)
                    {
                        numeral -= 10;
                        carry = 1;
                    }
                    else carry = 0;
                    result.Add(numeral);
                }

                if (smallerNum.Length < biggerNum.Length)
                {
                    result.Add((carry + (int)Char.GetNumericValue(biggerNum[smallerNum.Length])));
                    int index = smallerNum.Length+1;
                    for (int i = index;i < biggerNum.Length; i++)
                    {
                        result.Add((int)Char.GetNumericValue(biggerNum[i]));
                    }
                }
                else if (carry==1) result.Add(1);
                return result;
            }
            char[] numArray1 = num1.ToCharArray();
            Array.Reverse(numArray1);
            char[] numArray2 = num2.ToCharArray();
            Array.Reverse(numArray2);
            List<int> reversedResult = new List<int>();
            if (num1.Length >= num2.Length)
                reversedResult = forLoop(numArray1, numArray2);
            else reversedResult = forLoop(numArray2, numArray1);
            int[] result = reversedResult.ToArray();
            Array.Reverse(result);
            return string.Join("",result);
        }
        public string ToString()
        {
            string result = vector[0].ToString();
            while (true)
            {
                for (int j = 1; j < Primes.Length; j++)
                {
                    if (modulo(result, Primes[j]) != vector[j]) break;
                    if (j == Primes.Length - 1) return result;
                }

                result = StringNumAddition(result, Primes[0].ToString());
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
            LongNumber g = new LongNumber("619342000");
            g += f;
            Console.WriteLine(g.ToBigInteger().ToString());
            Console.WriteLine(g.ToString());
            Console.ReadLine();
        }
    }
}