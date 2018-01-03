using System.Xml.Linq;
using Xml.Diff.Creation.CommonConstant;

namespace Xml.Diff.Processor
{
  public class AddAttributeAtChildFunctionality
  {
    public XDocument AddAttribute(XDocument diffXdoc)
    {
      XDocument _diffXdoc;
      AddCs_ParentAttributeAndParentPointerAtChangeAndRemoveNode addCsParentAttributeAndParentPointerAtChangeAndRemove =
        new AddCs_ParentAttributeAndParentPointerAtChangeAndRemoveNode();
      _diffXdoc = addCsParentAttributeAndParentPointerAtChangeAndRemove
        .AddCsParentAttributeAtFamilyOfDeleteAndChangeNode(Immutables.CHANGE, diffXdoc);
      _diffXdoc = addCsParentAttributeAndParentPointerAtChangeAndRemove
        .AddCsParentAttributeAtFamilyOfDeleteAndChangeNode(Immutables.REMOVE, _diffXdoc);
      AtAddNodeAddCs_ParentAttributeAndParentPointer atAddNodeAddCsParentAttributeAndParentPointer =
        new AtAddNodeAddCs_ParentAttributeAndParentPointer();
      return atAddNodeAddCsParentAttributeAndParentPointer.AddCsParentAndParentPointerAtFamilyOfAddNode(_diffXdoc);
    }
  }
}
