namespace SwtorHelper.Data;

public class CharacterSettingView
{	
    public Character Character { get; set; }
    public string Key { get; set; }
    public string? Value { get; set; }

    public CharacterSettingView(Character character, string key, string? value)
    {
        this.Character = character;
        this.Key = key;
        this.Value = value;
    }
	
    public Task ApplyAsync() => Character.SetAsync(Key, Value);
}