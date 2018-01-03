using System.Xml.Linq;

namespace Xml.Result.Processor
{
    public class ResultXmlProcessor
    {
      private XDocument _diffXDoc;
      private XDocument _resultXDoc;
    public void StartResultProcessing(XDocument diffXDoc, XDocument resultXDoc)
    {
      AddCrestId addCrestId=new AddCrestId();
      _resultXDoc = addCrestId.AddCrestID(diffXDoc,resultXDoc);
      ChangeFunctionality changeFunctionality=new ChangeFunctionality();
      _resultXDoc = changeFunctionality.AddNodeAndAttributeInformation(diffXDoc, resultXDoc);
      DeleteFunctionality deleteFunctionality=new DeleteFunctionality();
      _resultXDoc = deleteFunctionality.AddMovedAndDeletedNodeAndAttributeInformation(diffXDoc, _resultXDoc);
      AddFunctionality addFunctionality=new AddFunctionality();
      _resultXDoc = addFunctionality.AddNodeAttributeAndNode(diffXDoc, _resultXDoc);
      ModificationFunctionalitiy modificationFunctionalitiy=new ModificationFunctionalitiy();
      SaveResultXML(modificationFunctionalitiy.ModifyResult(diffXDoc,_resultXDoc));
    }

    private void SaveResultXML(XDocument resultXDoc)
      {
        resultXDoc.Save(@"d:\SampleXML\result.xml");
      }
  }
}
