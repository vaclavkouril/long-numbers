using System;
using System.Numerics;
using LongNumberLib;

namespace Long_Numbers;

internal class Program
{
    static void Main(string[] args)
    {
        LongNumber a = new LongNumber("37520224124164609");
        LongNumber b = new LongNumber("53752412124742020");
        LongNumber c = new LongNumber("89429210844762");
        a += b + c;
        Console.WriteLine(a.ToBigInteger().ToString());
    }
}
