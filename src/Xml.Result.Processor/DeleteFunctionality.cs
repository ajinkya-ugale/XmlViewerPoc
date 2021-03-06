﻿using System.Xml.Linq;
namespace Xml.Result.Processor
{
  public class DeleteFunctionality
  {
    private XDocument _resultXDoc;
    public XDocument AddMovedAndDeletedNodeAndAttributeInformation(XDocument diffXDoc, XDocument resultXDoc)
    {
      AddDeletedNodeAndAttributeInformationInResult addDeletedNodeAndAttributeInformationInResult = new AddDeletedNodeAndAttributeInformationInResult();
      _resultXDoc = addDeletedNodeAndAttributeInformationInResult.DeleteNode(diffXDoc, resultXDoc);
      AddMovedAttributeNodeInformationInResult addMovedAttributeNodeInformationInResult = new AddMovedAttributeNodeInformationInResult();
      return addMovedAttributeNodeInformationInResult.MovedNodeAtr(diffXDoc, _resultXDoc);
    }

  }
}
