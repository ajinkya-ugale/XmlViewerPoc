using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Xml.Diff.Creation.CommonConstant;
using Xml.Diff.Creation.Utilities;

namespace Xml.Diff.Processor
{
  public class DiffXmlProcessor
  {
    private XDocument _diffXDoc;
    public DiffXmlProcessor()
    {

    }

    public XDocument Process(XDocument diffXml)
    {
      Cs_BaseAttributeAdditionAtRootNodeInDiffXml csBaseAttributeAdditionAtRootNodeInDiffXml =
        new Cs_BaseAttributeAdditionAtRootNodeInDiffXml();
      _diffXDoc = csBaseAttributeAdditionAtRootNodeInDiffXml.AddCsBaseAttributeAtRootNode(diffXml);
      AddAttributeAtChildFunctionality addAttributeAtChildFunctionality=new AddAttributeAtChildFunctionality();
      return addAttributeAtChildFunctionality.AddAttribute(_diffXDoc);
    }
    protected XDocument IsWellFormedXml(string XmlData)
    {
      try
      {
        return XDocument.Parse(XmlData);
      }
      catch
      {
        return null;
      }
    }

    protected void AddCustomAttributeToNode(XElement xNode, string value)
    {
      xNode.Add(new XAttribute(CustomAttribute.ParentNodePath, value));
    }
  }
}
