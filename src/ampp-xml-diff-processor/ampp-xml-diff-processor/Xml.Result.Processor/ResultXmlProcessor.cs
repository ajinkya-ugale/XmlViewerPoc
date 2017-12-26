using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Xml.Diff.Creation.CommonConstant;
using Xml.Diff.Creation.Utilities;

namespace Xml.Result.Processor
{
    public class ResultXmlProcessor
    {
      private XDocument _diffXDoc;
      //public XDocument StartResultProcessing(XDocument diffXDoc, XDocument r c)
      //{
      //_diffXDoc =   diffXDoc;

      //  return resultXDoc;
      //}
     
      private void SaveResultXML(XDocument resultXDoc)
      {
        resultXDoc.Save(@"d:\SampleXML\result.xml");
      }
  }
}
