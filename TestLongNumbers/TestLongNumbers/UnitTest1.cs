using System.Numerics;
using LongNumberLib;

namespace TestLongNumbers;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Input_Normal()
    {
        //testing
        LongNumber x = new LongNumber("3752022412416460");
        Assert.Pass();
    }

    [Test]
    public void Input_Negative()
    {
        try
        {
            //testing
            LongNumber x = new LongNumber("-3752022412416460");

            Assert.Fail();
        }
        catch
        {
            Assert.Pass();
        }
    }

    [Test]
    public void ToString_Normal()
    {
        //var
        LongNumber x = new LongNumber("3752022412416460");

        //result
        const string result = "3752022412416460";

        //testing
        if (x.ToString() == result)
            Assert.Pass();
        else
            Assert.Fail();
    }

    [Test]
    public void ToBigInteger_Zero()
    {
        //var
        LongNumber x = new LongNumber("0");

        //result
        BigInteger result = 0;

        //testing
        if (x.ToBigInteger() == result)
            Assert.Pass();
        else
            Assert.Fail();
    }

    [Test]
    public void ToBigInteger_Normal()
    {
        //var
        LongNumber x = new LongNumber("3752022412416460");

        //result
        BigInteger result = 3752022412416460;

        //testing
        if (x.ToBigInteger() == result)
            Assert.Pass();
        else
            Assert.Fail();
    }

    [Test]
    public void ToString_Zero()
    {
        //var
        LongNumber x = new LongNumber("0");

        //result
        const string result = "0";

        //testing
        if (x.ToString() == result)
            Assert.Pass();
        else
            Assert.Fail();
    }

    [Test]
    public void Addition()
    {
        //var
        LongNumber a = new LongNumber("3752022412416460");
        LongNumber b = new LongNumber("5375241212474202");
        LongNumber c = new LongNumber("89429210844762");

        //result 
        BigInteger result = 9216692835735424;

        //operation
        a += b + c;

        //testing
        if (result == a.ToBigInteger())
            Assert.Pass();
        else
            Assert.Fail();
    }

    [Test]
    public void Subtraction_Normal()
    {
        //var
        LongNumber a = new LongNumber("5375241212474202");
        LongNumber b = new LongNumber("89429210844762");

        //result 
        BigInteger result = 5285812001629440;

        //operation
        a -= b;

        //testing
        if (result == a.ToBigInteger())
            Assert.Pass();
        else
            Assert.Fail();
    }

    [Test]
    public void Subtraction_BiggerFromSmaller()
    {
        //var
        LongNumber a = new LongNumber("5375241212474202");
        LongNumber b = new LongNumber("89429210844762");

        //testing
        try
        {
            b -= a;
            Assert.Fail();
        }
        catch
        {
            Assert.Pass();
        }
    }
    
    [Test]
    public void Subtraction_Addition()
    {
        //var
        LongNumber a = new LongNumber("9216692835735424");
        LongNumber b = new LongNumber("5375241212474202");
        LongNumber c = new LongNumber("89429210844762");

        //result 
        BigInteger result = 3752022412416460;

        //operation
        a -= b + c;

        //testing
        if (result == a.ToBigInteger())
            Assert.Pass();
        else
            Assert.Fail();
    }
    
    [Test]
    public void Multiplication()
    {
        //var
        LongNumber a = new LongNumber("89429210844762");
        LongNumber b = new LongNumber("124");
        //result 
        BigInteger result = 11089222144750488;

        //operation
        a *= b;

        //testing
        if (result == a.ToBigInteger())
            Assert.Pass();
        else
            Assert.Fail();
    }
    
    [Test]
    public void Multiplication_ZeroAndNumber()
    {
        //var
        LongNumber a = new LongNumber("8942921");
        LongNumber b = new LongNumber("0");
        //result 
        BigInteger result = 0;
        
        //operation
        a *= b;
        
        //testing
        if (result == a.ToBigInteger()) 
            Assert.Pass();
        else
            Assert.Fail();
    }
    
    [Test]
    public void Division()
    {
        //var
        LongNumber a = new LongNumber("89429210844762");
        LongNumber b = new LongNumber("12481632641282");
        //result 
        BigInteger result = 7;

        //operation
        a /= b;

        //testing
        if (result == a.ToBigInteger())
            Assert.Pass();
        else
            Assert.Fail();
    }
    
    [Test]
    public void Division_SmallNominator()
    {
        //var
        LongNumber a = new LongNumber("8942921");
        LongNumber b = new LongNumber("4");
        //result 
        BigInteger result = 2235730;

        //operation
        a /= b;

        //testing
        if (result == a.ToBigInteger())
            Assert.Pass();
        else
            Assert.Fail();
    }
    
    [Test]
    public void Division_ByZero()
    {
        //var
        LongNumber a = new LongNumber("53");
        LongNumber b = new LongNumber("0");
        
        //testing
        try
        {
            a /= b;
            Assert.Fail();
        }
        catch
        {
            Assert.Pass();
        }
    }
    
    [Test]
    public void Division_OfZero()
    {
        //var
        LongNumber a = new LongNumber("0");
        LongNumber b = new LongNumber("4438811924532");
        
        //result 
        BigInteger result = 0;
        
        //operation
        a /= b;
        
        //testing
        if (result == a.ToBigInteger()) 
            Assert.Pass();
        else
            Assert.Fail();
    }
    
    [Test]
    public void Power()
    {
        //var
        LongNumber a = new LongNumber("2");
        
        //result 
        BigInteger result = 536870912;
        
        //operation
        a = LongNumber.Power(a,29);
        
        //testing
        if (result == a.ToBigInteger()) 
            Assert.Pass();
        else
            Assert.Fail();
    }
}
