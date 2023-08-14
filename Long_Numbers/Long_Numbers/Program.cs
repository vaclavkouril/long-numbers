using System;

namespace Long_Numbers
{
    public interface LongNumber
    {
        public void LongNumber(string bigNum);
        public LongNumber Plus(LongNumber num1, LongNumber num2);
    }
    public class Long_number : LongNumber
    {
        public void LongNumber(string bigNum)
        {
            
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
            Console.WriteLine("Hello World!");
        }
    }
}