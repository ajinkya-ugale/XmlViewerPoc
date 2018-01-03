using System.Xml.Linq;
using Xml.Diff.Creation.CommonConstant;

namespace Xml.Result.Processor
{
  public class ModificationFunctionalitiy
  {
    private XDocument _resultXDoc;

    public XDocument ModifyResult( XDocument resultXDoc)
    {
      RenameTagValueInResult renameTagValueInResult = new RenameTagValueInResult();
      _resultXDoc = renameTagValueInResult.RenameTag(resultXDoc);
      DeleteMovedAndDeletedAttributes deleteMovedAndDeletedAttributes = new DeleteMovedAndDeletedAttributes();
      _resultXDoc = deleteMovedAndDeletedAttributes.DeleteAttr(Immutables.CS_CRESTID, _resultXDoc);
      return deleteMovedAndDeletedAttributes.DeleteMovedNodeAtr(_resultXDoc);
    }
  }
}
