using System.Linq;
using System.Xml.Linq;
using Xml.Diff.Creation.CommonConstant;
using Xml.Diff.Creation.Utilities;

namespace Xml.Result.Processor
{
  public class AddNewAttributeAndAddedNewNodeInformationInResult
  {
    public XDocument AddNodeattr(XDocument diffXDoc, XDocument resultXDoc)
    {
      diffXDoc.Descendants(diffXDoc.Root.Name.Namespace + Immutables.ADD).Where(s => s.Attribute(Immutables.TYPE) != null && s.Attribute(Immutables.TYPE).Value == "2" && s.Attribute(Immutables.PROCESSED) == null).ToList()
        .ForEach(item =>
        {
          string parentposition = item.Attribute(Immutables.CS_PARENT).Value;
          XElement nd = CommonUtilities.GetActualNd(parentposition, item, resultXDoc) as XElement;
          nd.Add(new XAttribute(Immutables.ADDATTRNAME_ + item.Attribute(Immutables.NAME).Value, item.Value));
          item.Add(new XAttribute(Immutables.PROCESSED, Immutables.TRUE));
        });
      return resultXDoc;
    }

    public XDocument AddSingleNode(XDocument diffXDoc, XDocument resultXDoc)
    {
      diffXDoc.Descendants(diffXDoc.Root.Name.Namespace + Immutables.ADD).Where(s => s.Attribute(Immutables.TYPE) == null && s.Attribute(Immutables.OPID) == null && s.Attribute(Immutables.NAME) == null).ToList()
        .ForEach(item =>
        {
          string parentposition = item.Attribute(Immutables.CS_PARENT).Value;
          XElement xNode = CommonUtilities.GetActualNd(parentposition, item, resultXDoc) as XElement;
          xNode.AddAfterSelf(new XElement(Immutables.ADDITION, item.Descendants().ToList()));
          item.Add(new XAttribute(Immutables.PROCESSED, Immutables.TRUE));
        });

      diffXDoc.Descendants(diffXDoc.Root.Name.Namespace + Immutables.ADD).Where(s => s.Attribute(Immutables.TYPE) != null && s.Attribute(Immutables.TYPE).Value == Immutables.ONE && s.Attribute(Immutables.PROCESSED) == null).ToList()
        .ForEach(item =>
        {
          if (item.Descendants(diffXDoc.Root.Name.Namespace + Immutables.ADD).ToList().Count > 0)
          {
            ProcessNode(item, diffXDoc, resultXDoc);
          }
          else
          {
            string parentposition = item.Attribute(Immutables.CS_PARENT).Value;
            XElement nd = CommonUtilities.GetActualNd(parentposition, item, resultXDoc) as XElement;
            nd.AddAfterSelf(new XElement(Immutables.ADDITION, new XElement(item.Attribute(Immutables.NAME).Value, "")));
            item.Add(new XAttribute(Immutables.PROCESSED, Immutables.TRUE));
          }

        });
      return resultXDoc;
    }

    public XDocument ProcessNode(XElement item, XDocument diffXDoc, XDocument resultXDoc)
    {
      if (item.Attribute(Immutables.PROCESSED) == null)
      {
        XElement xElement = new XElement(item.Attribute(Immutables.NAME).Value, "");
        item.Add(new XAttribute(Immutables.PROCESSED, Immutables.TRUE));

        item.Descendants(diffXDoc.Root.Name.Namespace + Immutables.ADD).ToList()
          .ForEach(s =>
          {
            //single node or moved node///////////////////////////////////////////////
            if (s.Attribute(Immutables.TYPE) != null && s.Attribute(Immutables.TYPE).Value == Immutables.ONE && s.Attribute(Immutables.PROCESSED) == null && s.Attribute(Immutables.TYPE) != null)
            {
              if (xElement.Descendants().Elements() != null)
              {
                if (xElement.Elements().Count() > 0)
                  xElement.Elements().Last().Add(new XElement(s.Attribute(Immutables.NAME).Value), "");
                else
                  xElement.Add(new XElement(s.Attribute(Immutables.NAME).Value), "");

                s.Add(new XAttribute(Immutables.PROCESSED, Immutables.TRUE));
              }
              else
              {
                if (xElement.Elements().Count() > 0)
                  xElement.Elements().Last().Add(new XElement(s.Attribute(Immutables.NAME).Value), "");
                else
                  xElement.Add(new XElement(s.Attribute(Immutables.NAME).Value), "");
                s.Add(new XAttribute(Immutables.PROCESSED, Immutables.TRUE));
              }
            }
            else if (s.Attribute(Immutables.OPID).Value == Immutables.ONE)
            {
              var x = resultXDoc.Descendants().Where(a => a.Attribute(Immutables.MOVEDPOS) != null && a.Attribute(Immutables.MOVEDPOS).Value == s.Attribute(Immutables.OPID).Value.ToString()).FirstOrDefault();
              if (xElement.Elements().Count() > 0)
                xElement.Elements().Last().Add(x);
              else
                xElement.Add(x);

              s.Add(new XAttribute(Immutables.PROCESSED, Immutables.TRUE));
            }
          });
        string parentposition = item.Attribute(Immutables.CS_PARENT).Value;
        XElement nd = CommonUtilities.GetActualNd(parentposition, item, resultXDoc) as XElement;
        nd.AddAfterSelf(new XElement(Immutables.ADDITION, xElement));
      }
      return resultXDoc;
    }

    private void AddMovedTextInformation(XElement xElement, XElement s)
    {
      if (xElement.Descendants().Elements() != null)
      {
        if (xElement.Elements().Count() > 0)
          xElement.Elements().Last().Add(new XElement(s.Attribute(Immutables.NAME).Value), "");
        else
          xElement.Add(new XElement(s.Attribute(Immutables.NAME).Value), "");

        s.Add(new XAttribute(Immutables.PROCESSED, Immutables.TRUE));
      }
      else
      {
        if (xElement.Elements().Count() > 0)
          xElement.Elements().Last().Add(new XElement(s.Attribute(Immutables.NAME).Value), "");
        else
          xElement.Add(new XElement(s.Attribute(Immutables.NAME).Value), "");
        s.Add(new XAttribute(Immutables.PROCESSED, Immutables.TRUE));
      }
    }
  }
}
