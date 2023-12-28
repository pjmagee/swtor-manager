namespace SwtorHelper.Data;

public class Character
{
    public string Name { get; set; }
    public FileInfo FileInfo { get; set; }
    public CharacterSettings CharacterSettings { get; set; }
	
    public async Task SetAsync(string key, string? value)
    {
        string[] lines = await File.ReadAllLinesAsync(FileInfo.FullName);
        IDictionary<string, string> settings = lines.Where(line => line.Contains(" = ")).ToDictionary(x => x.Split(" = ", StringSplitOptions.TrimEntries)[0], x => x.Split(" = ", StringSplitOptions.TrimEntries)[1]);

        if (string.IsNullOrWhiteSpace(value))
        {
            settings.Remove(key);
        }
        else
        {
            settings[key] = value;
        }

        await File.WriteAllLinesAsync(FileInfo.FullName, new[] { "[Settings]" }.Concat(settings.Select(setting => $"{setting.Key} = {setting.Value}")));
    }

    public async Task SetAsync(IDictionary<string, string?> overwrite)
    {
        string[] lines = await File.ReadAllLinesAsync(FileInfo.FullName);
        IDictionary<string, string> settings = lines.Where(line => line.Contains(" = ")).ToDictionary(x => x.Split(" = ", StringSplitOptions.TrimEntries)[0], x => x.Split(" = ", StringSplitOptions.TrimEntries)[1]);

        foreach (var key in settings.Keys)
        {
            if (overwrite.TryGetValue(key, out var value))
            {
                if (value is not null) settings[key] = value;
                else settings.Remove(key);
            }
        }

        await File.WriteAllLinesAsync(FileInfo.FullName, new[] { "[Settings]" }.Concat(settings.Select(setting => $"{setting.Key} = {setting.Value}")));
    }
}