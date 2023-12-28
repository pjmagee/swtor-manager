namespace SwtorHelper.Domain.Tracker;

public struct CharacterName : IEquatable<CharacterName>
{
    public string Value { get; set;  }
    
    public CharacterName(string value)
    {
        Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    public bool Equals(CharacterName other) => Value.Equals(other.Value);
}