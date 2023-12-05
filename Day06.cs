namespace AdventofCode2023;

public class Day06(ITestOutputHelper Output) : BaseDay
{
    [Fact]
    public void Part1()
    {
        var result = Lines.Select(line => line.Length).Sum();

        Assert.Equal(00000, result);
    }

    [Fact]
    public void Part2()
    {
        var result = Lines.Select(line => line.Length).Sum();

        Assert.Equal(00000, result);
    }
}
