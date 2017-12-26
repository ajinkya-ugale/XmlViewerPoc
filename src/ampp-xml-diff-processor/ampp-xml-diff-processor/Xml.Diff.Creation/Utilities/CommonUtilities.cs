using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xml.Diff.Creation.CommonConstant;

namespace Xml.Diff.Creation.Utilities
{
  public class CommonUtilities
  {
    public static XNode GetActualNd(string parentposition, XElement item,XDocument resultXDoc)
    {
      XNode Nd = null;
      if (parentposition.Contains("/") == false)
      {
        Nd = resultXDoc.Descendants().Where(s => s.Attribute("cs_crestid").Value == parentposition).First();
      }
      else
      {
        int counter = Convert.ToInt32(parentposition.Split('/').Length) - 1;
        for (int p = 0; p <= Convert.ToInt32(counter); p++)
        {
          if (p == 0)
            Nd = resultXDoc.Descendants().Where(s => s.Attribute("cs_crestid").Value == parentposition.Split('/')[p]).FirstOrDefault();
          else
            Nd = (resultXDoc.Descendants().Where(s => s.Attribute("cs_crestid") != null && s.Attribute("cs_crestid").Value == parentposition.Split('/')[p].ToString()).FirstOrDefault());
        }

      }
      return Nd;
    }
    public static string GetParentpointer(XElement xNode)//Done
    {
      string resultPointer = "";
      while (xNode.Name.LocalName == Immutables.NODE)
      {
        resultPointer = xNode.Attribute(Immutables.MATCH).Value + Immutables.FORWARD_SLASH + resultPointer;
        xNode = xNode.Parent;
      }
      return resultPointer.Trim(Immutables.FORWARD_SLASH);
    }
  }
}
