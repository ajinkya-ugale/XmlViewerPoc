using System.Configuration;
using System.Xml;
namespace Xml.Diff.Creation
{
  public class XmlDiffCreation
  {
    string originalFileName = "";
    string compareFileName = "";
    string _diffFileName = "difffilename";
    NameTable NS;

    public void CreateDiff(string orignalFile, string compareFile)
    {
      InitializeVar(_diffFileName);
      DeleteExistingFile(_diffFileName);
      DiffXml diffXml = new DiffXml();
      diffXml.CreateDiff(orignalFile, compareFile, _diffFileName);
    }
    public void DeleteExistingFile(string diffFileName)
    {
      if (System.IO.File.Exists(diffFileName))
        System.IO.File.Delete(diffFileName);
    }
    public void InitializeVar(string diffFileName)
    {

      diffFileName = ConfigurationManager.AppSettings[diffFileName];
    }
  }
}
