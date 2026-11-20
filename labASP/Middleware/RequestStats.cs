using System.Collections.Concurrent;

namespace labASP.Middleware;

public class RequestStats                 
{
    private long _total;
    // odczyt stanu licznika
    private readonly ConcurrentDictionary<string, long> _pathCounts = new(StringComparer.OrdinalIgnoreCase);
    public long Total => Interlocked.Read(ref _total);
    // inkrementacja licznika
    public long Hit(string path)
    {
        var normalizedPath = string.IsNullOrWhiteSpace(path) ? "/" : path;
        _pathCounts.AddOrUpdate(normalizedPath, 1, (_, current) => current + 1);
        return Interlocked.Increment(ref _total);
    }
    public IReadOnlyList<KeyValuePair<string, long>> GetTopPaths(int count = 5)
    {
        return _pathCounts
            .OrderByDescending(kvp => kvp.Value)
            .ThenBy(kvp => kvp.Key)
            .Take(count)
            .ToList();
    }
}

