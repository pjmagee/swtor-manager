namespace SwtorHelper.Data;

public class SettingsApplier
{
    public async Task ApplyAsync(string key, string? value)
    {
        foreach(var item in SettingsManager.EnumerateCharacters())
        {
            await item.SetAsync(key, value);
        }
    }
	
    public async Task ApplyAsync(IDictionary<string, string?> settings)
    {
        foreach (var item in SettingsManager.EnumerateCharacters())
        {
            await item.SetAsync(settings);
        }
    }
}