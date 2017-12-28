using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xml.Diff.Creation.CommonConstant;

namespace Xml.Result.Processor
{
  public class ModificationFunctionalitiy
  {
    private XDocument _resultXDoc;

    public XDocument ModifyResult(XDocument diffXDoc, XDocument resultXDoc)
    {
      RenameTagValueInResult renameTagValueInResult = new RenameTagValueInResult();
      _resultXDoc = renameTagValueInResult.RenameTag(resultXDoc);
      DeleteMovedAndDeletedAttributes deleteMovedAndDeletedAttributes = new DeleteMovedAndDeletedAttributes();
      _resultXDoc = deleteMovedAndDeletedAttributes.DeleteAttr(Immutables.CS_CRESTID, _resultXDoc);
      return deleteMovedAndDeletedAttributes.DeleteMovedNodeAtr(_resultXDoc);
    }
  }
}
