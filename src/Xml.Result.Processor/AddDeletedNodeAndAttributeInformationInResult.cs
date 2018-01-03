using System;
using System.Linq;
using System.Xml.Linq;
using Xml.Diff.Creation.CommonConstant;
using Xml.Diff.Creation.Utilities;

namespace Xml.Result.Processor
{
  public class AddDeletedNodeAndAttributeInformationInResult
  {
    public XDocument DeleteNode(XDocument diffXDoc, XDocument resultXDoc)
    {
      diffXDoc.Descendants(diffXDoc.Root.Name.Namespace + Immutables.REMOVE).Where(s => s.Attribute(Immutables.MATCH) != null && s.Attribute(Immutables.OPID) == null).ToList()
        .ForEach(item =>
        {
          string nodeposiiton = item.Attribute(Immutables.MATCH).Value;
          if (nodeposiiton.Contains(Immutables.AT_SIGN))
          {
            //delete attr 
            string parentposition = item.Attribute(Immutables.CS_PARENT).Value;
            XNode nd = CommonUtilities.GetActualNd(parentposition, item, resultXDoc);
            (nd as XElement).Add(new XAttribute(Immutables.REMOVEATTR_ + (nd as XElement).Attribute(nodeposiiton.Replace(Immutables.AT_SIGN, "")).Name, (nd as XElement).Attribute(nodeposiiton.Replace(Immutables.AT_SIGN, "")).Value));
            (nd as XElement).Attribute((nd as XElement).Attribute(nodeposiiton.Replace(Immutables.AT_SIGN, "")).Name).Remove();
          }
          else
          {
            //delete node
            string parentposition = item.Attribute(Immutables.CS_PARENT).Value;
            XNode nd = CommonUtilities.GetActualNd(parentposition, item, resultXDoc);
            XElement requiredNd = (XElement)(nd as XElement).Nodes().ToList()[Convert.ToInt32(nodeposiiton) - 1];
            requiredNd.Add(new XAttribute(Immutables.DELETED, Immutables.TRUE));
          }
        });
      return resultXDoc;
    }
  }
}
