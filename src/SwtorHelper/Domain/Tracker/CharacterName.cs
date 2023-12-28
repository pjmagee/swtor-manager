namespace SwtorHelper.Domain.Tracker;

public struct CharacterName : IEquatable<CharacterName>, IFormattable
{
    private string Value { get; set;  }
    
    public static implicit operator string(CharacterName characterName) => characterName.Value;
    public static implicit operator CharacterName(string value) => new(value);
    
    public CharacterName(string value)
    {
        Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    public override string ToString()
    {
        return Value;
    }

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        return Value;
    }

    public bool Equals(CharacterName other) => Value.Equals(other.Value);
}