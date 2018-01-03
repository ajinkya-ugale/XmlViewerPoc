using System.Linq;
using System.Xml.Linq;
using Xml.Diff.Creation.CommonConstant;

namespace Xml.Result.Processor
{
  public class DeleteMovedAndDeletedAttributes
  {
    public XDocument DeleteAttr(string p, XDocument resultXDoc)
    {
      resultXDoc.Descendants().Where(s => s.Attribute(p) != null).ToList()
        .ForEach(d =>
          {
            d.Attribute(p).Remove();
          }
        );
      return resultXDoc;
    }
    private void RenameAddedNd()
    {
      //resultXDoc.Descendants().Elements(CommonConstant.ADDITION).ToList()
      //  .ForEach(s => {
      //      s.Remove();
      //  });
    }
    public XDocument DeleteMovedNodeAtr(XDocument resultXDoc)
    {
      resultXDoc.Descendants().Where(s => s.Attribute("Moved") != null && s.Attribute(Immutables.DELETED) != null && s.Attribute("Moved").Value == Immutables.TRUE && s.Attribute(Immutables.DELETED).Value == Immutables.TRUE).ToList()
        .ForEach(d =>
          {
            d.Attribute(Immutables.DELETED).Remove();
          }
        );
      return resultXDoc;
    }
  }
}
