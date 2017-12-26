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
    XDocument diffXDoc = new XDocument();
    public DiffXmlProcessor()
    {

    }

    public XDocument Process(XDocument diffXml, XDocument SourceXml)
    {
      XDocument diffXDoc = FindRootNode(SourceXml);

      return new XDocument();
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
    public void CreateXDoc(string originalFileName, string diffFileName)
    {
      XDocument originalXDoc = new XDocument();
      XDocument compareXDoc = new XDocument();
      XDocument resultXDoc = new XDocument();
      //  XDocument diffXDoc = new XDocument();
      originalXDoc = XDocument.Load(originalFileName);
      resultXDoc = originalXDoc;
      diffXDoc = XDocument.Load(diffFileName);
      FindRootNode(diffXDoc);
    }
    public XDocument FindRootNode(XDocument diffXDoc)//Done
    {
      diffXDoc.Descendants(diffXDoc.Root.Name.Namespace + Immutables.XMLDIFF).Descendants(diffXDoc.Root.Name.Namespace + Immutables.NODE).FirstOrDefault()
        .Add(new XAttribute(Immutables.CS_BASE, Immutables.TRUE));
      return diffXDoc;
    }
    public XDocument FindChildFamily(XDocument diffXDoc)//Done
    {
      diffXDoc = GetFamilyOfAdd(GetFamilyOfDeleteAndChange(Immutables.REMOVE, GetFamilyOfDeleteAndChange(Immutables.CHANGE, diffXDoc)));
      return diffXDoc;
    }
    public XDocument GetFamilyOfDeleteAndChange(string nodetype, XDocument diffXDoc)//Done
    {
      diffXDoc.Descendants(diffXDoc.Root.Name.Namespace + nodetype).ToList()
        .ForEach(d =>
        {
          string parentPointer = CommonUtilities.GetParentpointer(d.Parent);
          d.Add(new XAttribute(Immutables.CS_PARENT, parentPointer));
        });
      return diffXDoc;
    }

    public XDocument GetFamilyOfAdd(XDocument diffXDoc)//Done
    {
      diffXDoc.Descendants(diffXDoc.Root.Name.Namespace + Immutables.ADD).ToList()
        .ForEach(d =>
        {
          XElement parentNode = GetAddNodeParent(d);
          string parentPointer =CommonUtilities.GetParentpointer(parentNode);
          if (parentPointer != "")
            d.Add(new XAttribute(Immutables.CS_PARENT, parentPointer));
        });
      return diffXDoc;
    }
    
    public XElement GetAddNodeParent(XElement item)//Done
    {

      XElement parentNd = null;
      if (item.PreviousNode != null)
      {
        var x = (XElement)(item.PreviousNode);
        if (x.Name != Immutables.NODE)
          parentNd = CheckPreviosSibling(item);
        else
          parentNd = (XElement)(item.PreviousNode);
      }
      else
      {
        if (item.Parent.Attribute(Immutables.CS_BASE) == null)
          parentNd = item.Parent;
        else
          parentNd = diffXDoc.Descendants(diffXDoc.Root.Name.Namespace + Immutables.NODE).Where(x => (string)x.Attribute(Immutables.CS_BASE) == Immutables.TRUE).FirstOrDefault();
      }
      return parentNd;
    }
    public XElement CheckPreviosSibling(XElement item)//Done
    {
      XElement parentNd = null;
      if (item.PreviousNode != null)
        parentNd = (XElement)item.PreviousNode;
      while (parentNd.Name.LocalName != Immutables.NODE)
      {
        if (parentNd.PreviousNode != null)
          parentNd = (XElement)parentNd.PreviousNode;
        else
          parentNd = parentNd.Parent;
      }
      return parentNd;
    }
   
   
  }
}
