namespace SwtorHelper.Data;

public class Setting : Dictionary<string, List<CharacterSettingView>>
{
    public Setting(IDictionary<string, List<CharacterSettingView>> settings) : base(settings)
    {

    }
}