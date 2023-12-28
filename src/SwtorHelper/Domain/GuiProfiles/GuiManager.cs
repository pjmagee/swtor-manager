using System.Xml.Linq;

namespace SwtorHelper.Data;

public static class GuiManager
{
    public static readonly string Settings = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData, System.Environment.SpecialFolderOption.None), "SWTOR", "swtor", "settings", "GUIProfiles");

    public static DirectoryInfo ProfilesDirectory => new DirectoryInfo(Settings);


    public static IEnumerable<GuiProfile> EnumerateGuiProfiles()
    {
        return ProfilesDirectory
            .EnumerateFiles("*.xml", new EnumerationOptions() { RecurseSubdirectories = true })
            .AsParallel()
            .Select(fi =>
            {
                using (var stream = fi.OpenRead())
                {
                    return new GuiProfile
                    {
                        File = fi,
                        XDocument = XDocument.Load(stream, LoadOptions.None)
                    };
                }
            });
    }
}