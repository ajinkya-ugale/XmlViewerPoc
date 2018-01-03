using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Microsoft.XmlDiffPatch;

namespace Xml.Diff.Creation
{
  public class DiffXml
  {
    
    public void CreateDiff(string originalFile,string compareFile,string diffFileName)
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
