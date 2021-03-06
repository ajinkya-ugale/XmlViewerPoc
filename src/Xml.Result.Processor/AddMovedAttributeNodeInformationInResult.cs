﻿using System;
using System.Linq;
using System.Xml.Linq;
using Xml.Diff.Creation.CommonConstant;
using Xml.Diff.Creation.Utilities;

namespace Xml.Result.Processor
{
  public class AddMovedAttributeNodeInformationInResult
  {
    public XDocument MovedNodeAtr(XDocument diffXDoc, XDocument resultXDoc)
    {
      diffXDoc.Descendants(diffXDoc.Root.Name.Namespace + Immutables.REMOVE).Where(s => s.Attribute(Immutables.MATCH) != null && s.Attribute(Immutables.OPID) != null).ToList()
        .ForEach(item =>
        {
          string nodePosiiton = item.Attribute(Immutables.MATCH).Value;
          string parentPosition = item.Attribute(Immutables.CS_PARENT).Value;
          XNode nd = CommonUtilities.GetActualNd(parentPosition, item, resultXDoc);
          XElement requiredNd = (XElement)(nd as XElement).Nodes().ToList()[Convert.ToInt32(nodePosiiton) - 1];
          requiredNd.Add(new XAttribute("Moved", Immutables.TRUE), new XAttribute(Immutables.DELETED, Immutables.TRUE), new XAttribute(Immutables.MOVEDPOS, item.Attribute(Immutables.OPID).Value));
          diffXDoc.Descendants(diffXDoc.Root.Name.Namespace + Immutables.DESCRIPTOR).Single(p => p.Attribute(Immutables.OPID).Value == item.Attribute(Immutables.OPID).Value).Add(new XAttribute(Immutables.MOVEDPOS, item.Attribute(Immutables.OPID).Value));
        });
      return resultXDoc;
    }
  }
}
