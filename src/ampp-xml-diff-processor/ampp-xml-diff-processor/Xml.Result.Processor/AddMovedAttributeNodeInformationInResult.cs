using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xml.Diff.Creation.CommonConstant;
using Xml.Diff.Creation.Utilities;

namespace Xml.Result.Processor
{
  public class AddMovedAttributeNodeInformationInResult
  {
    public XDocument MovedNodeAtr(XDocument diffXDoc, XDocument resultXDoc)//Done
    {
      diffXDoc.Descendants(diffXDoc.Root.Name.Namespace + Immutables.REMOVE).Where(s => s.Attribute(Immutables.MATCH) != null && s.Attribute(Immutables.OPID) != null).ToList()
        .ForEach(item =>
        {
          string nodeposiiton = item.Attribute(Immutables.MATCH).Value;
          string parentposition = item.Attribute(Immutables.CS_PARENT).Value;
          XNode nd = CommonUtilities.GetActualNd(parentposition, item, resultXDoc);
          XElement requiredNd = (XElement)(nd as XElement).Nodes().ToList()[Convert.ToInt32(nodeposiiton) - 1];
          requiredNd.Add(new XAttribute("Moved", Immutables.TRUE), new XAttribute(Immutables.DELETED, Immutables.TRUE), new XAttribute(Immutables.MOVEDPOS, item.Attribute(Immutables.OPID).Value));
          diffXDoc.Descendants(diffXDoc.Root.Name.Namespace + Immutables.DESCRIPTOR).Single(p => p.Attribute(Immutables.OPID).Value == item.Attribute(Immutables.OPID).Value).Add(new XAttribute(Immutables.MOVEDPOS, item.Attribute(Immutables.OPID).Value));
        });
      return resultXDoc;
    }
  }
}
