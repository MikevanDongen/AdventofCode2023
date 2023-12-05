namespace AdventofCode2023;

public class Day03(ITestOutputHelper Output) : BaseDay
{
    private const int _mapWidth = 140;
    private const int _mapHeight = 140;

    private static readonly char[] _digits = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9'];
    private static readonly char[] _symbols = ['-', '@', '*', '/', '&', '#', '%', '+', '=', '$'];

    [Fact]
    public void Part1()
    {
        var result = 0;
        for (int lineIndex = 0; lineIndex < _mapHeight; lineIndex++)
        {
            var line = Lines[lineIndex];

            for (int charIndex = line.IndexOfAny(_digits); charIndex > -1;)
            {
                var extractedNumber = line[charIndex..].TakeWhile(c => _digits.Contains(c)).ToArray();
                var isAdjacentToSymbol = IsAdjacentToSymbol(lineIndex, charIndex, charIndex + extractedNumber.Length - 1);
                if (isAdjacentToSymbol)
                {
                    var number = int.Parse(string.Join("", extractedNumber));
                    result += number;
                }

                var skip = charIndex + extractedNumber.Length + 1;
                if (skip >= _mapWidth)
                {
                    break;
                }

                charIndex = line.IndexOfAny(_digits, skip);
            }
        }

        Assert.Equal(530495, result);
    }

    [Fact]
    public void Part2()
    {
        var result = 0;
        for (int lineIndex = 0; lineIndex < _mapHeight; lineIndex++)
        {
            var line = Lines[lineIndex];

            for (int charIndex = line.IndexOf('*'); charIndex > -1;)
            {
                var adjacentNumbers = GetAdjacentNumbers(lineIndex, charIndex).ToArray();
                if (adjacentNumbers.Length == 2)
                {
                    result += adjacentNumbers[0] * adjacentNumbers[1];
                }

                var skip = charIndex + 1;
                if (skip >= _mapWidth)
                {
                    break;
                }

                charIndex = line.IndexOf('*', skip);
            }
        }

        Assert.Equal(80253814, result);
    }

    private IEnumerable<int> GetAdjacentNumbers(int lineIndex, int charIndex)
    {
        if (charIndex > 0 && GetNumber(Lines[lineIndex][..charIndex].Reverse(), true, out int number))
        {
            yield return number;
        }

        if (charIndex < _mapWidth - 1 && GetNumber(Lines[lineIndex][(charIndex + 1)..], false, out number))
        {
            yield return number;
        }

        if (lineIndex > 0)
        {
            if (_digits.Contains(Lines[lineIndex - 1][charIndex]))
            {
                var extractedNumber = Lines[lineIndex - 1][..charIndex].Reverse().TakeWhile(c => _digits.Contains(c)).Reverse()
                    .Concat(Lines[lineIndex - 1][charIndex..]).TakeWhile(c => _digits.Contains(c));

                yield return int.Parse(string.Join("", extractedNumber));
            }
            else
            {
                if (charIndex > 0 && GetNumber(Lines[lineIndex - 1][..charIndex].Reverse(), true, out number))
                {
                    yield return number;
                }

                if (charIndex < _mapWidth - 1 && GetNumber(Lines[lineIndex - 1][(charIndex + 1)..], false, out number))
                {
                    yield return number;
                }
            }
        }

        if (lineIndex < _mapHeight - 1 && Lines[lineIndex + 1][int.Max(0, charIndex - 1)..int.Min(_mapWidth - 1, charIndex + 2)].IndexOfAny(_digits) > -1)
        {
            if (_digits.Contains(Lines[lineIndex + 1][charIndex]))
            {
                var extractedNumber = Lines[lineIndex + 1][..charIndex].Reverse().TakeWhile(c => _digits.Contains(c)).Reverse()
                    .Concat(Lines[lineIndex + 1][charIndex..]).TakeWhile(c => _digits.Contains(c));

                yield return int.Parse(string.Join("", extractedNumber));
            }
            else
            {
                if (charIndex > 0 && GetNumber(Lines[lineIndex + 1][..charIndex].Reverse(), true, out number))
                {
                    yield return number;
                }

                if (charIndex < _mapWidth - 1 && GetNumber(Lines[lineIndex + 1][(charIndex + 1)..], false, out number))
                {
                    yield return number;
                }
            }
        }
    }

    private static bool GetNumber(IEnumerable<char> input, bool inputIsReversed, out int number)
    {
        if (_digits.Contains(input.First()))
        {
            var extractedNumber = input.TakeWhile(c => _digits.Contains(c));
            if (inputIsReversed)
            {
                extractedNumber = extractedNumber.Reverse();
            }

            number = int.Parse(string.Join("", extractedNumber));

            return true;
        }

        number = 0;
        return false;
    }

    private bool IsAdjacentToSymbol(int lineIndex, int charIndexStart, int charIndexEnd)
    {
        if (charIndexStart > 0 && _symbols.Contains(Lines[lineIndex][charIndexStart - 1]))
        {
            return true;
        }

        if (charIndexEnd < _mapWidth - 1 && _symbols.Contains(Lines[lineIndex][charIndexEnd + 1]))
        {
            return true;
        }

        if (lineIndex > 0 && Lines[lineIndex - 1][int.Max(0, charIndexStart - 1)..int.Min(_mapWidth - 1, charIndexEnd + 2)].IndexOfAny(_symbols) > -1)
        {
            return true;
        }

        if (lineIndex < _mapHeight - 1 && Lines[lineIndex + 1][int.Max(0, charIndexStart - 1)..int.Min(_mapWidth - 1, charIndexEnd + 2)].IndexOfAny(_symbols) > -1)
        {
            return true;
        }

        return false;
    }
}
