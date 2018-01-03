
using System.Configuration;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Microsoft.XmlDiffPatch;
namespace Xml.Diff.Creation
{
  public class XmlDiffCreation
  {
    string originalFileName = "";
    string compareFileName = "";
    string _diffFileName = "difffilename";
    XDocument originalXDoc = new XDocument();
    XDocument compareXDoc = new XDocument();
    XDocument resultXDoc = new XDocument();
    XDocument diffXDoc = new XDocument();
    NameTable NS;

    public void CreateDiff(string orignalFile, string compareFile)
    {
      InitializeVar(_diffFileName);
      DeleteExistingFile(_diffFileName);
      DiffXml diffXml=new DiffXml();
      diffXml.CreateDiff(orignalFile,compareFile,_diffFileName);
    }
    public void DeleteExistingFile(string diffFileName)
    {
      if (System.IO.File.Exists(diffFileName) == true)
        System.IO.File.Delete(diffFileName);
    }
    
    public void CreateXDoc()
    {
      originalXDoc = XDocument.Load(originalFileName);
      resultXDoc = originalXDoc;
      diffXDoc = XDocument.Load(_diffFileName);
    }

    public void InitializeVar(string diffFileName)
    {

      diffFileName = ConfigurationManager.AppSettings[diffFileName];
    }
  }
}
