using SwtorLogParser.Model;

namespace SwtorHelper.Domain.Tracker;

public struct CharacterLocationEvent
{
    public GameObject Event { get; set; }
    public DateTime Timestamp { get; set; }

    public CharacterLocationEvent(CombatLogLine line)
    {
        ArgumentNullException.ThrowIfNull(line, nameof(line));
        ArgumentNullException.ThrowIfNull(line.Action, nameof(line.Action));
        
        Timestamp = line.TimeStamp;
        Event = line.Action.Event;
    }
}