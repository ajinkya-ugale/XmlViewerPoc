using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xml.Diff.Creation.CommonConstant;

namespace Xml.Result.Processor
{
  public class RenameTagValueInResult
  {
    public XDocument RenameTag(XDocument resultXDoc)//Done
    {
      resultXDoc.Descendants().Where(s => s.Attribute(Immutables.OLDNAME) != null && s.Attribute(Immutables.NEWNAME) != null).ToList()
        .ForEach(d =>
          {
            d.Add(new XAttribute(Immutables.TAGCHANGED, Immutables.TRUE));
            d.Name = d.Attribute(Immutables.NEWNAME).Value;
          }
        );
      return resultXDoc;
    }
  }
}
