using SwtorLogParser.Model;

namespace SwtorHelper.Domain.Tracker;

public struct CharacterLocationEvent
{
    public LineAction Action { get; set; }
    public DateTime Timestamp { get; set; }

    public CharacterLocationEvent(CombatLogLine line)
    {
        ArgumentNullException.ThrowIfNull(line, nameof(line));
        ArgumentNullException.ThrowIfNull(line.Action, nameof(line.Action));
        
        Timestamp = line.TimeStamp;
        Action = line.Action;
    }
}