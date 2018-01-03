using System.IO;
using System.Xml;
using Microsoft.XmlDiffPatch;

namespace Xml.Diff.Creation
{
  public class DiffXml
  {

    public void CreateDiff(string originalFile, string compareFile, string diffFileName)
    {
      XmlTextWriter tw = new XmlTextWriter(new StreamWriter(diffFileName));
      tw.Formatting = Formatting.Indented;
      try
      {
        XmlDiff diff = new XmlDiff();
        diff.Compare(originalFile, compareFile, false, tw);
      }
      catch { }
      finally { tw.Close(); }
    }

  }
}
