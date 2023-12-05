namespace AdventofCode2023;

public class Day02(ITestOutputHelper Output) : BaseDay
{
    [Fact]
    public void Part1()
    {
        var thresholdRed = 12;
        var thresholdGreen = 13;
        var thresholdBlue = 14;

        var result = Lines
            .Select(Game.Parse)
            .Where(g => g.Test(thresholdRed, thresholdGreen, thresholdBlue))
            .Sum(g => g.Id);

        Assert.Equal(2285, result);
    }

    [Fact]
    public void Part2()
    {
        var result = Lines
            .Select(Game.Parse)
            .Sum(g => g.CalculatePower());

        Assert.Equal(77021, result);
    }

    private class Game
    {
        public int Id { get; }
        public List<Set> Sets { get; }

        private Game(int id, List<Set> sets)
        {
            Id = id;
            Sets = sets;
        }

        public static Game Parse(string line) => new(
            id: int.Parse(line[5..line.IndexOf(':')]),
            sets: line[(line.IndexOf(':') + 2)..].Split(';').Select(Set.Parse).ToList());

        public bool Test(int thresholdRed, int thresholdGreen, int thresholdBlue) =>
            Sets.TrueForAll(set => set.Test(thresholdRed, thresholdGreen, thresholdBlue));

        public int CalculatePower() =>
            Sets.Aggregate(Set.Aggregate).Power;
    }

    private class Set
    {
        public int Red { get; }
        public int Green { get; }
        public int Blue { get; }
        public int Power => Red * Green * Blue;

        private Set(int red, int green, int blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        public static Set Parse(string line)
        {
            var colors = line.Split(',');
            return new Set(
                red: ParseColor(Array.Find(colors, c => c.Contains("red"))?.TrimStart()),
                green: ParseColor(Array.Find(colors, c => c.Contains("green"))?.TrimStart()),
                blue: ParseColor(Array.Find(colors, c => c.Contains("blue"))?.TrimStart()));
        }

        private static int ParseColor(string? v) => v switch
        {
            null => 0,
            _ => int.Parse(v[..v.IndexOf(' ')])
        };

        public bool Test(int thresholdRed, int thresholdGreen, int thresholdBlue) =>
            Red <= thresholdRed && Green <= thresholdGreen && Blue <= thresholdBlue;

        public static Set Aggregate(Set set1, Set set2)
            => new(int.Max(set1.Red, set2.Red), int.Max(set1.Green, set2.Green), int.Max(set1.Blue, set2.Blue));
    }
}
