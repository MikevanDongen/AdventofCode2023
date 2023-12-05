namespace AdventofCode2023;

public class Day05(ITestOutputHelper Output) : BaseDay
{
    [Fact]
    public void Part1()
    {
        var seeds = Lines[0][7..].Split(' ').Select(long.Parse).ToList();
        var mapperCollections = new List<List<Mapper>>
        {
            Lines[3..29].Select(Mapper.Parse).ToList(),
            Lines[32..50].Select(Mapper.Parse).ToList(),
            Lines[52..96].Select(Mapper.Parse).ToList(),
            Lines[98..119].Select(Mapper.Parse).ToList(),
            Lines[121..151].Select(Mapper.Parse).ToList(),
            Lines[154..203].Select(Mapper.Parse).ToList(),
            Lines[205..247].Select(Mapper.Parse).ToList(),
        };

        var locations = new List<long>();
        foreach (var seed in seeds)
        {
            var id = seed;
            foreach (var mapperCollection in mapperCollections)
            {
                foreach (var mapper in mapperCollection)
                {
                    if (id >= mapper.SourceRangeStart && id <= mapper.SourceRangeEnd)
                    {
                        id += mapper.Shift;
                        break;
                    }
                }
            }

            locations.Add(id);
        }

        var result = locations.Min();
        Assert.Equal(331445006, result);
    }

    [Fact]
    public async Task Part2()
    {
        var seeds = Lines[0][7..].Split(' ').Select(long.Parse).ToList();
        var mapperCollections = new List<List<Mapper>>
        {
            Lines[3..29].Select(Mapper.Parse).ToList(),
            Lines[32..50].Select(Mapper.Parse).ToList(),
            Lines[52..96].Select(Mapper.Parse).ToList(),
            Lines[98..119].Select(Mapper.Parse).ToList(),
            Lines[121..151].Select(Mapper.Parse).ToList(),
            Lines[154..203].Select(Mapper.Parse).ToList(),
            Lines[205..247].Select(Mapper.Parse).ToList(),
        };

        var locationTasks = new List<Task<long>>();
        for (int seedGroupIndex = 0; seedGroupIndex < seeds.Count; seedGroupIndex += 2)
        {
            var seedId = seeds[seedGroupIndex];
            var numberOfSeeds = seeds[seedGroupIndex + 1];

            locationTasks.Add(Task.Run(() =>
            {
                var nearbyLocation = long.MaxValue;
                for (int seedInGroupIndex = 0; seedInGroupIndex < numberOfSeeds; seedInGroupIndex++)
                {
                    var id = seedId + seedInGroupIndex;
                    foreach (var mapperCollection in mapperCollections)
                    {
                        foreach (var mapper in mapperCollection)
                        {
                            if (id >= mapper.SourceRangeStart && id <= mapper.SourceRangeEnd)
                            {
                                id += mapper.Shift;
                                break;
                            }
                        }
                    }

                    if (id < nearbyLocation)
                    {
                        nearbyLocation = id;
                    }
                }

                return nearbyLocation;
            }));
        }

        var locations = await Task.WhenAll(locationTasks.ToArray());
        Assert.Equal(6472060, locations.Min());
    }

    private class Mapper
    {
        public long SourceRangeStart { get; }
        public long SourceRangeEnd { get; }
        public long Shift { get; }

        private Mapper(long destinationRangeStart, long sourceRangeStart, long rangeLength)
        {
            SourceRangeStart = sourceRangeStart;
            SourceRangeEnd = sourceRangeStart + rangeLength - 1;
            Shift = destinationRangeStart - sourceRangeStart;
        }

        public static Mapper Parse(string line)
        {
            var split = line.Split(' ');
            return new Mapper(long.Parse(split[0]), long.Parse(split[1]), long.Parse(split[2]));
        }
    }
}
