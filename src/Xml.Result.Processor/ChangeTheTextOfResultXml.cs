using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Xml.Diff.Creation.CommonConstant;
using Xml.Diff.Creation.Utilities;

namespace Xml.Result.Processor
{
  public class ChangeTheTextOfResultXml
  {
    private XDocument _resultXDoc;
    public XDocument ChangeNodeText(XDocument diffXDoc, XDocument resultXDoc)
    {
      _resultXDoc = resultXDoc;
      diffXDoc.Descendants(diffXDoc.Root.Name.Namespace + Immutables.CHANGE).Where(s => s.Attribute(Immutables.MATCH) != null && s.Attribute(Immutables.NAME) == null).ToList()
          .ForEach(item =>
          {
            string nodeposiiton = item.Attribute(Immutables.MATCH).Value;
            nodeposiiton = nodeposiiton.Split(Immutables.FORWARD_SLASH)[nodeposiiton.Split(Immutables.FORWARD_SLASH).Length - 1];
            if (nodeposiiton.All(Char.IsDigit))
            {
              ChangeNodeTextWhenNodePositionIsDigit(item, nodeposiiton);
            }
            else
            {
              ChangeNodeTextWhenNodePositionIsNotDigit(item);
            }
          });
      return _resultXDoc;
    }

    private void ChangeNodeTextWhenNodePositionIsNotDigit(XElement item)
    {
      if (item.Attribute(Immutables.MATCH).Value.Contains(Immutables.AT_SIGN))
      {
        string atrname = item.Attribute(Immutables.MATCH).Value.Replace(Immutables.AT_SIGN, "");
        string parentposition = item.Attribute(Immutables.CS_PARENT).Value;
        XNode xNode = CommonUtilities.GetActualNd(parentposition, item, _resultXDoc);
        if (xNode != null)
        {
          XElement valnode = null;
          if (xNode.NodeType == XmlNodeType.Element)
            valnode = (XElement)xNode;
          valnode.Add(new XAttribute(Immutables.OLDATTR_ + atrname, valnode.Attribute(atrname).Value));
          valnode.SetAttributeValue(atrname, item.Value);
        }
      }
    }

    private void ChangeNodeTextWhenNodePositionIsDigit(XElement item, string nodeposiiton)
    {
      string parentposition = item.Attribute(Immutables.CS_PARENT).Value;
      XNode xNode = CommonUtilities.GetActualNd(parentposition, item, _resultXDoc);
      XElement valnode = null;
      if (xNode.NodeType == XmlNodeType.Element)
        valnode = (XElement)xNode;
      XNode requiredNd = valnode.Nodes().ToList()[Convert.ToInt32(nodeposiiton) - 1];
      if (requiredNd.NodeType == XmlNodeType.Text)
      {
        XElement element = new XElement(Immutables.TEXTCHANGED, new XElement(Immutables.DEL, requiredNd),
          new XElement(Immutables.INS, item.Value));
        valnode.Nodes().ToList()[Convert.ToInt32(nodeposiiton) - 1].ReplaceWith(element);
      }
    }
  }
}
