using System;
using System.Numerics;
using LongNumberLib;

namespace Long_Numbers;

internal class Program
{
    static void Main(string[] args)
    {
        LongNumber a = new LongNumber("3752022412416460");
        LongNumber b = new LongNumber("5375241212474202");
        LongNumber c = new LongNumber("89429210844762");
        LongNumber d = new LongNumber("2");
        a += b + c;
        b -= c;
        d = LongNumber.Power(d, 29);   
        LongNumber g = b / d;
        LongNumber f = c * new LongNumber("124");
        
        if( "9216692835735424" == a.ToBigInteger().ToString())
            Console.WriteLine("Test 1: Addition; 9216692835735424");
        else Console.WriteLine("Test 1: Addition; Failed");

        if( "5285812001629440" == b.ToBigInteger().ToString())
            Console.WriteLine("Test 2: Subtraction; 5285812001529440");
        else Console.WriteLine("Test 2: Subtraction; Failed");
        
        if( "536870912" == d.ToString())
            Console.WriteLine("Test 3: Power; 536870912");
        else Console.WriteLine("Test 3: Power; Failed");
        
        if( "11089222144750488" == f.ToBigInteger().ToString())
            Console.WriteLine("Test 5: Multiplication; 536870912");
        else Console.WriteLine("Test 5: Multiplication; Failed");
        
        if( "9845592" == g.ToString())
            Console.WriteLine("Test 6: Division; 9845592");
        else Console.WriteLine("Test 6: Division; Failed");
    }
}
