using System.Linq;
using System.Xml.Linq;
using Xml.Diff.Creation.CommonConstant;
using Xml.Diff.Creation.Utilities;

namespace Xml.Diff.Processor
{
 public class AtAddNodeAddCs_ParentAttributeAndParentPointer
 {
   private XDocument _diffXDoc;
    public XDocument AddCsParentAndParentPointerAtFamilyOfAddNode(XDocument diffXDoc)//Done
    {
      _diffXDoc = diffXDoc;
      diffXDoc.Descendants(diffXDoc.Root.Name.Namespace + Immutables.ADD).ToList()
        .ForEach(d =>
        {
          XElement parentNode = GetAddNodeParent(d);
          string parentPointer = CommonUtilities.GetParentpointer(parentNode);
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
          parentNd = _diffXDoc.Descendants(_diffXDoc.Root.Name.Namespace + Immutables.NODE).Where(x => (string)x.Attribute(Immutables.CS_BASE) == Immutables.TRUE).FirstOrDefault();
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
