namespace SwtorHelper.Data;

public class SettingsManager
{
	public static readonly string Settings = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData, System.Environment.SpecialFolderOption.None), "SWTOR", "swtor", "settings");

	public static DirectoryInfo SettingsDirectory => new DirectoryInfo(Settings);

	public static IEnumerable<Character> EnumerateCharacters()
	{
		foreach (FileInfo file in SettingsDirectory.EnumerateFiles("*PlayerGUIState.ini"))
		{
			var lines = File.ReadAllLines(file.FullName);
			
			Dictionary<string, string?> settings = lines
					.Where(line => line.Contains(" = "))
					.ToDictionary(keySelector => keySelector.Split("=", StringSplitOptions.TrimEntries)[0]!, x =>
					{
						var values = x.Split("=", StringSplitOptions.TrimEntries);
						return values.ElementAtOrDefault(1);
					});

			foreach (var key in settings.Keys)
				if (key.StartsWith("GroupFinder"))
					settings.Remove(key);

			yield return new Character { Name = file.Name.Split('_')[1], FileInfo = file, CharacterSettings = new CharacterSettings(settings) };
		}
	}

	public static async Task Apply(IDictionary<string, string?> settings)
	{
		foreach (var character in EnumerateCharacters())
		{
			await character.SetAsync(settings.Where(kvp => !kvp.Key.StartsWith("GroupFinder_")).ToDictionary(x => x.Key, x => x.Value));
		}
	}

	public static async Task ApplyAsync(string key, string? value)
	{
		foreach (var character in EnumerateCharacters())
		{
			await character.SetAsync(key, value);
		}
	}

	public static IEnumerable<Setting> EnumerateSettings(IEnumerable<Character> characters)
	{		
		foreach (var key in characters.SelectMany(x => x.CharacterSettings.Settings.Keys).Distinct())
		{
			Dictionary<string, List<CharacterSettingView>> dictionary = new Dictionary<string, List<CharacterSettingView>>();

			foreach (var character in characters)
			{
				if (!dictionary.TryGetValue(key, out var settingCharacters))
				{
					dictionary[key] = new List<CharacterSettingView>();
				}
				
				var value = character.CharacterSettings.Settings.ContainsKey(key) ? character.CharacterSettings.Settings[key] : null;

				dictionary[key].Add(new CharacterSettingView(character, key,  value));
			}

			yield return new Setting(dictionary);
		}
	}
}