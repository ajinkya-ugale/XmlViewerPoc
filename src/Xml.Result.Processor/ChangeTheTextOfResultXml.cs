using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Xml.Diff.Creation.CommonConstant;
using Xml.Diff.Creation.Utilities;

namespace Xml.Result.Processor
{
  public class ChangeTheTextOfResultXml
  {
    public XDocument ChangeNodeText(XDocument diffXDoc, XDocument resultXDoc)//Done
    {
      diffXDoc.Descendants(diffXDoc.Root.Name.Namespace + Immutables.CHANGE).Where(s => s.Attribute(Immutables.MATCH) != null && s.Attribute(Immutables.NAME) == null).ToList()
          .ForEach(item =>
          {
            string nodeposiiton = item.Attribute(Immutables.MATCH).Value;
            nodeposiiton = nodeposiiton.Split(Immutables.FORWARD_SLASH)[nodeposiiton.Split(Immutables.FORWARD_SLASH).Length - 1];
            if (nodeposiiton.All(Char.IsDigit))
            {
              ChangeNodeTextWhenNodePositionIsDigit(resultXDoc, item, nodeposiiton);
            }
            else
            {
              ChangeNodeTextWhenNodePositionIsNotDigit(resultXDoc, item);
            }
          });
      return resultXDoc;
    }

    private void ChangeNodeTextWhenNodePositionIsNotDigit(XDocument resultXDoc, XElement item)
    {
      if (item.Attribute(Immutables.MATCH).Value.Contains(Immutables.AT_SIGN) == true)
      {
        string atrname = item.Attribute(Immutables.MATCH).Value.Replace(Immutables.AT_SIGN, "");
        string parentposition = item.Attribute(Immutables.CS_PARENT).Value;
        XNode nd = CommonUtilities.GetActualNd(parentposition, item, resultXDoc);
        if (nd != null)
        {
          XElement valnode = null;
          if (nd.NodeType == XmlNodeType.Element)
            valnode = (XElement)nd;
          valnode.Add(new XAttribute(Immutables.OLDATTR_ + atrname, valnode.Attribute(atrname).Value));
          valnode.SetAttributeValue(atrname, item.Value);
        }
      }
    }

    private void ChangeNodeTextWhenNodePositionIsDigit(XDocument resultXDoc, XElement item, string nodeposiiton)
    {
      string parentposition = item.Attribute(Immutables.CS_PARENT).Value;
      XNode nd = CommonUtilities.GetActualNd(parentposition, item, resultXDoc);
      XElement valnode = null;
      if (nd.NodeType == XmlNodeType.Element)
        valnode = (XElement)nd;
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
