using System.Xml.Linq;

namespace Xml.Result.Processor
{
  public class ChangeFunctionality
  {
    private XDocument _resultXDoc;
    public XDocument AddNodeAndAttributeInformation(XDocument diffXDoc, XDocument resultXDoc)
    {
      ChangeAttributeOfNode changeAttributeOfNode=new ChangeAttributeOfNode();
      _resultXDoc = changeAttributeOfNode.ChangeAttrNode(diffXDoc, resultXDoc);
      ChangeTheTextOfResultXml changeTheTextOfResultXml=new ChangeTheTextOfResultXml();
      return changeTheTextOfResultXml.ChangeNodeText(diffXDoc,_resultXDoc);
    }
  }
}
