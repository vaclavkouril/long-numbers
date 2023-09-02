using System;
using System.Numerics;

namespace LongNumberLib
{
    public class LongNumber
    {
        private static List<long> _primes = new List<long>();
        private List<long> _vector = new List<long>();

        private static long LongModulo(long dividend, long divisor)
        {
            //returns number in range 0 < divisor
            return (dividend % divisor + divisor) % divisor;
        }

        private static long StringModulo(String num, long a)
        {
            long result = 0;
            for (int i = 0; i < num.Length; i++)
            {
                //modulo numeral after numeral
                result = (result * 10 + num[i] - '0') % a;
            }

            return result;
        }

        private static bool IsGreaterOrEqual(string num1, string num2)
        {
            if (num1.Length != num2.Length) //checking lenght
                return num1.Length > num2.Length;

            for (int i = 0; i < num1.Length; i++) //checking each digit from left to right
            {
                if (num1[i] != num2[i])
                    return num1[i] > num2[i];
            }

            return true; //both same
        }

        public LongNumber(string bigNum, List<long> primes = null)
        {
            _primes = new List<long>();
            if (primes == null) // adding default values if none are present
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
                _vector[i] = (StringModulo(bigNum, _primes[i])); // constructing rezidual vector
        }

        public BigInteger ToBigInteger()
        {
            BigInteger result = _vector[0];
            while (true)
            {
                for (int j = 1; j < _primes.Count; j++)
                {
                    if (result % _primes[j] != _vector[j]) break;
                    if (j == _primes.Count - 1) return result; // found number
                }
                result += _primes[0];
            }
        }

        private static string StringNumAddition(string num1, string num2)
        {
            List<int> ForLoop(char[] biggerNum, char[] smallerNum)
            {
                int carry = 0;
                List<int> result = new List<int>();

                for (int i = 0; i < smallerNum.Length; i++) // adds numbers in the same place
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
                for (int i = startIndex; i < biggerNum.Length; i++) // adds the rest of numbers
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
                if (carry == 1) // if number gets more numerals than biggerNum 
                    result.Add(1);

                return result;
            }
            //Initialize and reverse num1 and num2
            char[] numArray1 = num1.ToCharArray();
            Array.Reverse(numArray1);

            char[] numArray2 = num2.ToCharArray();
            Array.Reverse(numArray2);

            List<int> reversedResult = new List<int>();
            
            if (num1.Length >= num2.Length)
                reversedResult = ForLoop(numArray1, numArray2);
            else
                reversedResult = ForLoop(numArray2, numArray1);
            
            //reverse result back to correct form
            int[] result = reversedResult.ToArray();
            Array.Reverse(result);

            return string.Join("", result);
        }

        public new string ToString()
        {
            string result = _vector[0].ToString();

            while (true)
            {
                //Check all modulus of _primes
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
            if (num1.ToBigInteger() < num2.ToBigInteger()) throw new Exception("LongNumber doesn't work with negative numbers");
            
            LongNumber num3 = new LongNumber("0");
            
            for (int i = 0; i < _primes.Count; i++)
                num3._vector[i] = LongModulo((num1._vector[i] - num2._vector[i]), _primes[i]);

            return num3;
        }

        public static LongNumber operator *(LongNumber num1, LongNumber num2)
        {
            static string Multiply(string num1, string num2)
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

            LongNumber num3 = new LongNumber("0",LongNumber._primes);

            for (int i = 0; i < _primes.Count; i++)
                num3._vector[i] = StringModulo(Multiply(num1._vector[i].ToString(), num2._vector[i].ToString()),
                    _primes[i]);

            return num3;
        }

        public static LongNumber operator /(LongNumber numerator, LongNumber nominator)
        {
            // Convert numerator and nominator to strings
            string numeratorStr = numerator.ToString();
            string nominatorStr = nominator.ToString();
            
            // Division by zero error
            if (nominatorStr == "0") 
                throw new Exception("Division by zero error");
            
            string iterations = "0";
            string resultStr = "0";
            while (true)
            {
                for (int i = 0; i < 10; i++)
                {
                    resultStr = LongNumber.StringNumAddition(resultStr, nominatorStr);
                    if (IsGreaterOrEqual(resultStr, numeratorStr))
                    {
                        iterations = LongNumber.StringNumAddition(iterations, i.ToString());
                        return new LongNumber(iterations, LongNumber._primes);
                    }
                }
                iterations = LongNumber.StringNumAddition(iterations, "10");
            }
        }

        public static LongNumber Power(LongNumber number, int exponent)
        {
            LongNumber result = new LongNumber("1", LongNumber._primes);
            if (exponent == 0)
                return result;
            
            // Perform exponentiation using multiplication
            for (int i = 0; i < exponent; i++)
                result *= number;

            return result;
        }
    }
}