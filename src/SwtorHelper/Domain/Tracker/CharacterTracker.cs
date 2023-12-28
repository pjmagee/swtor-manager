using System.Collections.Concurrent;
using SwtorHelper.Data;
using SwtorLogParser.Model;
using SwtorLogParser.Monitor;

namespace SwtorHelper.Domain.Tracker;

public class CharacterTracker
{
    private readonly SettingsManager _settingsManager;
    private readonly CombatLogs _combatLogsService;
    private readonly ConcurrentDictionary<CharacterName, SortedList<DateTime, CharacterLocationEvent>> _characterLocationEvents = new();
    
    public ConcurrentDictionary<CharacterName, SortedList<DateTime, CharacterLocationEvent>> CharacterLocationEvents => _characterLocationEvents;

    public CharacterTracker(CombatLogs combatLogs, SettingsManager settingsManager)
    {
        _combatLogsService = combatLogs;
        _settingsManager = settingsManager;
    }
    public void Refresh()
    {
        _characterLocationEvents.Clear();
       
        foreach (var character in SettingsManager.EnumerateCharacters())
        {
            _characterLocationEvents.TryAdd(character.Name, new SortedList<DateTime, CharacterLocationEvent>());
        }

        var logs = _combatLogsService.EnumerateCombatLogs().ToList();

        Parallel.ForEach(logs, log =>
        {
            foreach (var item in log.EnumerateLogLines())
            {
                CombatLogLine? line = CombatLogLine.Parse(item);

                if (line is not null && line.Source?.IsLocalPlayer == true && line.Action?.Event.Name?.Equals("AreaEntered") == true)
                {
                    if (_characterLocationEvents.TryGetValue(line.Source.Name!, out var locationEvents))
                    {
                        lock(locationEvents)
                        {
                            var locationEvent = new CharacterLocationEvent(line);
                            locationEvents.Add(locationEvent.Timestamp, locationEvent);
                        }
                    }
                }
            }
        });
    }
}