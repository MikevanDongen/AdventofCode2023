namespace AdventofCode2023;

public class Day01 : BaseDay
{
    [Fact]
    public void Part1()
    {
        var result = Lines
            .Select(line =>
                int.Parse(string.Concat(line.First(c => c >= '0' && c <= '9'), line.Last(c => c >= '0' && c <= '9')))
                )
            .Sum();

        Assert.Equal(55607, result);
    }

    [Fact]
    public void Part2()
    {
        var results = new List<int>();
        foreach (var line in Lines)
        {
            var first = new List<KeyValuePair<int, int>>
            {
                new (1, line.IndexOf('1')),
                new (2, line.IndexOf('2')),
                new (3, line.IndexOf('3')),
                new (4, line.IndexOf('4')),
                new (5, line.IndexOf('5')),
                new (6, line.IndexOf('6')),
                new (7, line.IndexOf('7')),
                new (8, line.IndexOf('8')),
                new (9, line.IndexOf('9')),
                new (1, line.IndexOf("one")),
                new (2, line.IndexOf("two")),
                new (3, line.IndexOf("three")),
                new (4, line.IndexOf("four")),
                new (5, line.IndexOf("five")),
                new (6, line.IndexOf("six")),
                new (7, line.IndexOf("seven")),
                new (8, line.IndexOf("eight")),
                new (9, line.IndexOf("nine")),
            }.Where(x => x.Value > -1).OrderBy(x => x.Value);

            var last = new List<KeyValuePair<int, int>>
            {
                new (1, line.LastIndexOf('1')),
                new (2, line.LastIndexOf('2')),
                new (3, line.LastIndexOf('3')),
                new (4, line.LastIndexOf('4')),
                new (5, line.LastIndexOf('5')),
                new (6, line.LastIndexOf('6')),
                new (7, line.LastIndexOf('7')),
                new (8, line.LastIndexOf('8')),
                new (9, line.LastIndexOf('9')),
                new (1, line.LastIndexOf("one")),
                new (2, line.LastIndexOf("two")),
                new (3, line.LastIndexOf("three")),
                new (4, line.LastIndexOf("four")),
                new (5, line.LastIndexOf("five")),
                new (6, line.LastIndexOf("six")),
                new (7, line.LastIndexOf("seven")),
                new (8, line.LastIndexOf("eight")),
                new (9, line.LastIndexOf("nine")),
            }.Where(x => x.Value > -1).OrderByDescending(x => x.Value);

            results.Add(int.Parse(string.Concat(first.First().Key, last.First().Key)));
        }

        var result = results.Sum();
        Assert.Equal(55291, result);
    }
}
