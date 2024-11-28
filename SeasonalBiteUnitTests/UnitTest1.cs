namespace SeasonalBiteUnitTests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var foo = 5;
        
        Assert.InRange(foo,2,10);
    }
}