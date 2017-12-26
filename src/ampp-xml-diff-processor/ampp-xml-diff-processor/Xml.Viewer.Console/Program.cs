using System;
using System.Xml.Linq;

namespace Xml.Viewer.Console
{
  class Program
  {
    XDocument modifiedXmlDoc = new XDocument();
    XDocument sourceXmlDoc = new XDocument();
    public Program(XDocument originalXmlDoc, XDocument changedXmlDoc)
    {
      modifiedXmlDoc = changedXmlDoc;
      sourceXmlDoc = originalXmlDoc;
    }
    static void Main(string[] args)
    {

    }
  }
}
