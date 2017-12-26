
using System.Xml;
using System.Xml.Linq;

namespace Xml.Diff.Creation
{
    public class XmlDiffCreation
    {
    string originalFileName = "";
      string compareFileName = "";
      string diffFileName = "";
      XDocument originalXDoc = new XDocument();
      XDocument compareXDoc = new XDocument();
      XDocument resultXDoc = new XDocument();
      XDocument diffXDoc = new XDocument();
      NameTable NS;
    public XmlDiffCreation()
      {
          
      }
      //public void InitializeVar(string diffFileName)
      //{
      //  diffFileName = ConfigurationManager.AppSettings["difffilename"];
      //}
      public void DeleteExistingFile(string diffFileName)
      {
        if (System.IO.File.Exists(diffFileName) == true)
          System.IO.File.Delete(diffFileName);
      }
      //public void CreateDiff()
      //{
      //  XmlTextWriter tw = new XmlTextWriter(new StreamWriter(diffFileName));
      //  tw.Formatting = Formatting.Indented;
      //  try
      //  {
      //    XmlDiff diff = new XmlDiff();
      //    diff.Compare(originalFileName, compareFileName, false, tw);
      //  }
      //  catch { }
      //  finally { tw.Close(); }
      //}
      public void CreateXDoc()
      {
        originalXDoc = XDocument.Load(originalFileName);
        resultXDoc = originalXDoc;
        diffXDoc = XDocument.Load(diffFileName);
      }


  }
}
