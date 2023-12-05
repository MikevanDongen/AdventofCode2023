namespace AdventofCode2023;

public abstract class BaseDay
{
    private readonly string _inputFilename;
    private readonly Lazy<string[]> _lines;

    protected string[] Lines => _lines.Value;

    protected BaseDay()
    {
        _inputFilename = $"Inputs/{GetType().Name}.txt";
        _lines = new(() => File.ReadLines(_inputFilename).ToArray());
    }
}
