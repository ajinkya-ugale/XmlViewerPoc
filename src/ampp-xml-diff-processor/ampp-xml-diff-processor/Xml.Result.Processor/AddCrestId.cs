using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Xml.Diff.Creation.CommonConstant;
using Xml.Diff.Creation.Utilities;

namespace Xml.Result.Processor
{
 public class AddCrestId
  {
    public void AddCrestID(XDocument diffXDoc)//Confuse
    {

      diffXDoc.Descendants(diffXDoc.Root.Name.Namespace + Immutables.NODE).ToList()
        .ForEach(d =>
        {
          String matchvalue = d.Attribute(Immutables.MATCH).Value;
          XElement resultDocNd = GetResultNodes(d);
          resultDocNd.Add(new XAttribute(Immutables.CS_CRESTID, matchvalue));
        });
    }
    public XElement GetResultNodes(XElement item)//ResultXDoc Issue
    {
      XDocument resultXDoc = new XDocument();
      string parentPointer = CommonUtilities.GetParentpointer(item);
      XElement nd = GetBeforeIFActualNd(parentPointer, resultXDoc);
      return nd;
    }
    public XElement GetBeforeIFActualNd(string parentposition, XDocument resultXDoc)//Done
    {
      XElement item = null;
      int counter = parentposition.Split(Immutables.FORWARD_SLASH).Length;
      if (parentposition.Contains(Immutables.FORWARD_SLASH) == false)
        counter = Convert.ToInt32(parentposition);
      for (int p = 0; p <= Convert.ToInt32(counter) - 1; p++)
      {
        if (p == 0)
        {
          item = resultXDoc.Root;
        }
        else
        {
          if (parentposition.Contains(Immutables.FORWARD_SLASH) == true)
          {
            string pos1 = parentposition.Split(Immutables.FORWARD_SLASH)[p];
            int pos = Convert.ToInt32(pos1) - 1;
            var x = item.Nodes().ToList()[pos];
            if (x.NodeType == XmlNodeType.Element)
              item = (XElement)x;
          }
          else
          {
            break;
          }
        }
        if (parentposition.Contains(Immutables.FORWARD_SLASH) == false)
          break;
      }
      return item;
    }
  }
}
