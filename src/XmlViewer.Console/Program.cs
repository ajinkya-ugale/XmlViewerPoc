using System.Xml.Linq;
using Xml.Diff.Creation;
using Xml.Result.Processor;
using Xml.Diff.Processor;


namespace XmlViewer.Console
{
  class Program
  {
    static void Main(string[] args)
    {
      XDocument originalXDoc = new XDocument();
      XDocument compareXDoc = new XDocument();
      XDocument resultXDoc = new XDocument();
      XDocument diffXDoc;
      string orignalFilePath = "D:\\XML_Viewer\\1.xml";
      string compareFilePath = "D:\\XML_Viewer\\2.xml";
      XmlDiffCreation xmlDiffCreation = new XmlDiffCreation();
      xmlDiffCreation.CreateDiff(orignalFilePath, compareFilePath);
      originalXDoc = XDocument.Load(orignalFilePath);
      resultXDoc = originalXDoc;
      diffXDoc = XDocument.Load("D:\\SampleXML\\diff.out");
      DiffXmlProcessor diffXmlProcessor = new DiffXmlProcessor();
      diffXDoc = diffXmlProcessor.Process(diffXDoc);
      ResultXmlProcessor resultXmlProcessor = new ResultXmlProcessor();
      resultXmlProcessor.StartResultProcessing(diffXDoc, resultXDoc);
    }
  }
}
