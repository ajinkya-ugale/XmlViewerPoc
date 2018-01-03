using System.Xml.Linq;

namespace Xml.Result.Processor
{
  public class AddFunctionality
  {
    private XDocument _resultXDoc;

    public XDocument AddNodeAttributeAndNode(XDocument diffXDoc, XDocument resultXDoc)
    {
      AddNewAttributeAndAddedNewNodeInformationInResult addNewAttributeAndAddedNewNodeInformationInResult=new AddNewAttributeAndAddedNewNodeInformationInResult();
      _resultXDoc = addNewAttributeAndAddedNewNodeInformationInResult.AddNodeattr(diffXDoc, resultXDoc);
      return addNewAttributeAndAddedNewNodeInformationInResult.AddSingleNode(diffXDoc,_resultXDoc);
    }
  }
}
