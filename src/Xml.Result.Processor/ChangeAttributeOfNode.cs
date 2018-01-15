using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Xml.Diff.Creation.CommonConstant;
using Xml.Diff.Creation.Utilities;

namespace Xml.Result.Processor
{
  public class ChangeAttributeOfNode
  {
    public XDocument ChangeAttrNode(XDocument diffXDoc, XDocument resultXDoc)
    {
      diffXDoc.Descendants(diffXDoc.Root.Name.Namespace + Immutables.CHANGE).Where(s => s.Attribute(Immutables.NAME) != null).ToList()
        .ForEach(item =>
          {
            string nodeposiiton = item.Attribute(Immutables.MATCH).Value;
            nodeposiiton = nodeposiiton.Split(Immutables.FORWARD_SLASH)[nodeposiiton.Split(Immutables.FORWARD_SLASH).Length - 1];
            if (nodeposiiton.All(Char.IsDigit))
            {

              string parentposition = item.Attribute(Immutables.CS_PARENT).Value;
              XNode nd = CommonUtilities.GetActualNd(parentposition, item, resultXDoc);

              XElement valnode = null;
              if (nd.NodeType == XmlNodeType.Element)
                valnode = (XElement)nd;
              XNode requiredNode = valnode.Nodes().ToList()[Convert.ToInt32(nodeposiiton) - 1];
              XElement mainnode = null;
              if (requiredNode.NodeType == XmlNodeType.Element)
                mainnode = (XElement)requiredNode;

              mainnode.Add(new XAttribute(Immutables.OLDNAME, mainnode.Name));
              mainnode.Add(new XAttribute(Immutables.NEWNAME, item.Attribute(Immutables.NAME).Value));

            }
          }
        );
      return resultXDoc;
    }
  }
}
