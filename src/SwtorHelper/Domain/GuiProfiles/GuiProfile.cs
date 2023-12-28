using System.Xml.Linq;

namespace SwtorHelper.Data;

public class GuiProfile
{
    public FileInfo File { get; set; }
    public XDocument XDocument { get; set; }
}