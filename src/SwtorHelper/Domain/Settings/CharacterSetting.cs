using System.Diagnostics.CodeAnalysis;

namespace SwtorHelper.Data;

public class CharacterSetting
{
    public required string Key { get; init; }
    
    [AllowNull]
    public string? Value { get; set; }
}