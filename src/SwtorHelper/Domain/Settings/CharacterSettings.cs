namespace SwtorHelper.Data;

public class CharacterSettings 
{	
    public IDictionary<string, string?> Settings { get; set; }

    public CharacterSettings(IDictionary<string, string?> settings)
    {
        Settings = settings;			
    }
}