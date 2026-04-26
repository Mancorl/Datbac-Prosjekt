using System.Diagnostics;

public static class MeasurementsLogger
{
    private static readonly object LockObj = new();

    public static void Log(string app, string scenario, long elapsedMs)
    {
        var path = Path.Combine(FileSystem.AppDataDirectory, "Measurements.csv");

        var line = $"{DateTime.UtcNow:o},{app},{scenario},{elapsedMs}";

        lock (LockObj)
        {
            if (!File.Exists(path))
            {
                File.AppendAllText(path, "Timestamp,App,Scenario,Latency(ms)\n");
                Console.WriteLine($"Measurement file: {path}");

            }

            File.AppendAllText(path, line + "\n");
        }
    }
}
