using System;
using System.Numerics;
using LongNumberLib;

namespace Long_Numbers
{
    /*
    public class LongNumber
    {
        public static List<long> _primes = new List<long>();
        private List<long> _vector = new List<long>();

        private static long LongModulo(long dividend, long divisor)
        {
            return (dividend % divisor + divisor) % divisor;
        }

        private static long StringModulo(String num, long a)
        {
            long result = 0;
            for (int i = 0; i < num.Length; i++)
            {
                result = (result * 10 + num[i] - '0') % a;
            }

            return result;
        }

        public LongNumber(string bigNum, List<long> primes = null)
        {
            _primes = new List<long>();
            if (primes == null)
            {
                long[] defaultPrimes = new long[]
                {
                    (1 << 30) - 1, (1 << 29) - 1, (1 << 27) - 1, (1 << 23) - 1, (1 << 19) - 1, (1 << 17) - 1,
                    (1 << 13) - 1, (1 << 11) - 1
                };
                for (int i = 0; i < defaultPrimes.Length; i++)
                    _primes.Add(defaultPrimes[i]);
            }
            else
                _primes = primes;

            for (int i = 0; i < _primes.Count; i++)
                _vector.Add(0);

            if (bigNum[0] == '-')
                throw new Exception("LongNumber class accepts only non-negative integers");

            for (int i = 0; i < _primes.Count; i++)
                _vector[i] = (StringModulo(bigNum, _primes[i]));
        }

        public BigInteger ToBigInteger()
        {
            BigInteger result = _vector[0];
            while (true)
            {
                for (int j = 1; j < _primes.Count; j++)
                {
                    if (result % _primes[j] != _vector[j]) break;
                    if (j == _primes.Count - 1) return result;
                }

                result += _primes[0];
            }
        }

        private string StringNumAddition(string num1, string num2)
        {
            List<int> ForLoop(char[] biggerNum, char[] smallerNum)
            {
                int carry = 0;
                List<int> result = new List<int>();

                for (int i = 0; i < smallerNum.Length; i++)
                {
                    int numeral = (int)Char.GetNumericValue(biggerNum[i]) + (int)Char.GetNumericValue(smallerNum[i]) +
                                  carry;
                    if (numeral >= 10)
                    {
                        numeral -= 10;
                        carry = 1;
                    }
                    else
                        carry = 0;

                    result.Add(numeral);
                }

                int startIndex = smallerNum.Length;
                for (int i = startIndex; i < biggerNum.Length; i++)
                {
                    int numeral = (int)Char.GetNumericValue(biggerNum[i]) + carry;
                    if (numeral >= 10)
                    {
                        numeral -= 10;
                        carry = 1;
                    }
                    else
                        carry = 0;

                    result.Add(numeral);
                }
                if (carry == 1) 
                    result.Add(1);

                return result;
            }

            char[] numArray1 = num1.ToCharArray();
            Array.Reverse(numArray1);

            char[] numArray2 = num2.ToCharArray();
            Array.Reverse(numArray2);

            List<int> reversedResult = new List<int>();

            if (num1.Length >= num2.Length)
                reversedResult = ForLoop(numArray1, numArray2);
            else
                reversedResult = ForLoop(numArray2, numArray1);

            int[] result = reversedResult.ToArray();
            Array.Reverse(result);

            return string.Join("", result);
        }

        public new string ToString()
        {
            string result = _vector[0].ToString();

            while (true)
            {
                for (int j = 1; j < _primes.Count; j++)
                {
                    if (StringModulo(result, _primes[j]) != _vector[j]) break;
                    if (j == _primes.Count - 1) return result;
                }

                result = StringNumAddition(result, _primes[0].ToString());
            }
        }


        // Operators

        public static LongNumber operator +(LongNumber num1, LongNumber num2)
        {
            LongNumber num3 = new LongNumber("0");

            for (int i = 0; i < _primes.Count; i++)
                num3._vector[i] = LongModulo((num1._vector[i] + num2._vector[i]), _primes[i]);

            return num3;
        }

        public static LongNumber operator -(LongNumber num1, LongNumber num2)
        {
            LongNumber num3 = new LongNumber("0");

            for (int i = 0; i < _primes.Count; i++)
                num3._vector[i] = LongModulo((num1._vector[i] - num2._vector[i]), _primes[i]);

            return num3;
        }

        public static LongNumber operator *(LongNumber num1, LongNumber num2)
        {
            static string multiply(string num1, string num2)
            {
                if (num1.Length == 0 || num2.Length == 0)
                    return "0";
                int[] result = new int[num1.Length + num2.Length];

                // indexes for both numbers
                int iN1 = 0;
                int iN2 = 0;
                int i;

                // Go from right to left in num1
                for (i = num1.Length - 1; i >= 0; i--)
                {
                    int carry = 0;
                    int n1 = num1[i] - '0';
                    iN2 = 0;

                    // Go from right to left in num2            
                    for (int j = num2.Length - 1; j >= 0; j--)
                    {
                        int n2 = num2[j] - '0';
                        int sum = n1 * n2 + result[iN1 + iN2] + carry;
                        carry = sum / 10;
                        result[iN1 + iN2] = sum % 10;
                        iN2++;
                    }

                    if (carry > 0)
                        result[iN1 + iN2] += carry;
                    iN1++;
                }

                i = result.Length - 1;
                while (i >= 0 && result[i] == 0)
                    i--;

                if (i == -1)
                    return "0"; // either integer was 0

                String s = "";

                while (i >= 0)
                    s += (result[i--]);

                return s;
            }

            LongNumber num3 = new LongNumber("0");

            for (int i = 0; i < _primes.Count; i++)
                num3._vector[i] = StringModulo(multiply(num1._vector[i].ToString(), num2._vector[i].ToString()),
                    _primes[i]);

            return num3;
        }

        public static LongNumber operator /(LongNumber numerator, LongNumber nominator)
        {
            // LongNumber conversion
            int[] numeratorDigits = Array.ConvertAll(numerator.ToString().ToCharArray(), c => (int)char.GetNumericValue(c));
            int[] nominatorDigits = Array.ConvertAll(nominator.ToString().ToCharArray(), c => (int)char.GetNumericValue(c));
            
            int[] quotient = new int[numeratorDigits.Length];
            int remainder = 0;

            for (int i = 0; i < numeratorDigits.Length; i++)
            {
                int currentDigit = numeratorDigits[i] + remainder * 10;
                quotient[i] = currentDigit / nominatorDigits[0];
                remainder = currentDigit % nominatorDigits[0];
                
                for (int j = 0; j < numeratorDigits.Length - 1; j++)
                {
                    numeratorDigits[j] = currentDigit % 10;
                    currentDigit = numeratorDigits[j + 1] + currentDigit / 10;
                }
            }
            
            string resultStr = string.Join("", quotient);
            LongNumber result =  new LongNumber(resultStr,LongNumber._primes);
            
            return result;            
        }
        public static LongNumber Power(LongNumber number, int exponent)
        {
            LongNumber result = new LongNumber("1", LongNumber._primes);
            if (exponent == 0) 
                return result;
            
            for (int i = 0;i<exponent;i++) 
                result *= number;
            
            return result;
        }
        */
}

internal class Program
{
    static void Main(string[] args)
    {
        LongNumber a = new LongNumber("3752022412416460");
        LongNumber b = new LongNumber("53752412124742020");
        a += b;
        Console.WriteLine(a.ToBigInteger().ToString());
    }
}
