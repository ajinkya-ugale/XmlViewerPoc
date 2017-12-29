using System.Linq;
using System.Xml.Linq;
using Xml.Diff.Creation.CommonConstant;
using Xml.Diff.Creation.Utilities;

namespace Xml.Diff.Processor
{
  public class AddCs_ParentAttributeAndParentPointerAtChangeAndRemoveNode
  {
    public XDocument AddCsParentAttributeAtFamilyOfDeleteAndChangeNode(string nodetype, XDocument diffXDoc)//Done
    {
      diffXDoc.Descendants(diffXDoc.Root.Name.Namespace + nodetype).ToList()
        .ForEach(d =>
        {
          string parentPointer = CommonUtilities.GetParentpointer(d.Parent);
          d.Add(new XAttribute(Immutables.CS_PARENT, parentPointer));
        });
      return diffXDoc;
    }
  }
}
