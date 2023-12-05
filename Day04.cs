namespace AdventofCode2023;

public partial class Day04(ITestOutputHelper Output) : BaseDay
{
    private const int _numbersOfUniqueCards = 189;

    [GeneratedRegex(@"Card +(\d+): +(\d+) +(\d+) +(\d+) +(\d+) +(\d+) +(\d+) +(\d+) +(\d+) +(\d+) +(\d+) \| +(\d+) +(\d+) +(\d+) +(\d+) +(\d+) +(\d+) +(\d+) +(\d+) +(\d+) +(\d+) +(\d+) +(\d+) +(\d+) +(\d+) +(\d+) +(\d+) +(\d+) +(\d+) +(\d+) +(\d+) +(\d+) +(\d+) +(\d+) +(\d+) +(\d+)", RegexOptions.Compiled)]
    private static partial Regex ScratchcardRegex();

    [Fact]
    public void Part1()
    {
        var regex = ScratchcardRegex();
        var result = Lines.Select(line =>
        {
            var groups = ((IList<Group>)regex.Match(line).Groups).ToArray();
            return CalculateWorth(groups[2..12].Select(g => int.Parse(g.Value)).ToList(), groups[12..].Select(g => int.Parse(g.Value)).ToList());
        }).Sum();

        Assert.Equal(21105, result);
    }

    [Fact]
    public void Part2()
    {
        var regex = ScratchcardRegex();
        var cards = Lines.Select(line =>
        {
            var groups = ((IList<Group>)regex.Match(line).Groups).ToArray();
            var id = int.Parse(groups[1].Value);
            var numberOfWinningNumbers = CalculateNumberOfWinningNumbers(groups[2..12].Select(g => int.Parse(g.Value)).ToList(), groups[12..].Select(g => int.Parse(g.Value)).ToList());

            return (id, numberOfWinningNumbers);
        }).ToDictionary(k => k.id, v => v.numberOfWinningNumbers);

        var scores = new Dictionary<int, int>();
        foreach (var card in cards.Reverse())
        {
            var score = 1;
            var numberOfNewCards = int.Min(card.Value, _numbersOfUniqueCards - card.Key);
            for (int i = 0; i < numberOfNewCards; i++)
            {
                score += scores[card.Key + i + 1];
            }

            scores[card.Key] = score;
        }

        var result = scores.Values.Sum();
        Assert.Equal(5329815, result);
    }

    private int CalculateWorth(List<int> winningNumbers, List<int> myNumbers)
    {
        return myNumbers.Aggregate(0, (score, myNumber) => winningNumbers.Contains(myNumber) ? (score == 0 ? 1 : score * 2) : score);
    }

    private int CalculateNumberOfWinningNumbers(List<int> winningNumbers, List<int> myNumbers)
    {
        return myNumbers.Aggregate(0, (score, myNumber) => winningNumbers.Contains(myNumber) ? score + 1 : score);
    }
}
