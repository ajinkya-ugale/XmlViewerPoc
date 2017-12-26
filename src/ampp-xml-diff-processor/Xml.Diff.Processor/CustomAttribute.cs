using System.Xml;
using System.Xml.Linq;

namespace Xml.Diff.Processor
{
  public class CustomAttribute
  {
    public const string ParentNodePath = "ParentNodePath";
    public const string originalFileName = "";
    public const string compareFileName = "";
    public const string diffFileName = "";
    public static XDocument originalXDoc = new XDocument();
    public static XDocument compareXDoc = new XDocument();
    public static XDocument resultXDoc = new XDocument();
    public static XDocument diffXDoc = new XDocument();
    public NameTable NS;
  }
}
