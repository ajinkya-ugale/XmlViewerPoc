using System.Linq;
using System.Xml.Linq;
using Xml.Diff.Creation.CommonConstant;

namespace Xml.Diff.Processor
{
  public class Cs_BaseAttributeAdditionAtRootNodeInDiffXml
  {
    public XDocument AddCsBaseAttributeAtRootNode(XDocument diffXDoc)
    {
      diffXDoc.Descendants(diffXDoc.Root.Name.Namespace + Immutables.XMLDIFF).Descendants(diffXDoc.Root.Name.Namespace + Immutables.NODE).FirstOrDefault()
        .Add(new XAttribute(Immutables.CS_BASE, Immutables.TRUE));
      return diffXDoc;
    }
  }
}
