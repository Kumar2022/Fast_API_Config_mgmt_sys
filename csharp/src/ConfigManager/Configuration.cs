namespace ConfigManager;

public class Configuration
{
    private readonly Dictionary<string, string> _settings = new();

    public void LoadFromFile(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"Configuration file not found: {path}");
        }

        var json = File.ReadAllText(path);
        var data = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        if (data == null)
        {
            throw new InvalidOperationException("Configuration file is empty or malformed");
        }
        foreach (var kv in data)
        {
            _settings[kv.Key] = kv.Value;
        }
    }

    public string? this[string key]
    {
        get => _settings.TryGetValue(key, out var value) ? value : null;
        set
        {
            if (value == null)
            {
                _settings.Remove(key);
            }
            else
            {
                _settings[key] = value;
            }
        }
    }
}
