using System.Linq;
using NUnit.Framework;
using System.Xml.Linq;
using Xml.Result.Processor;
using Xml.Diff.Creation.Utilities;
namespace Xml.Diff.Processor.Tests
{
  [TestFixture]
  public class When_diff_xml_has_arrived : DiffXmlProcessor
  {
    DiffXmlProcessor diffXmlProcessor = new DiffXmlProcessor();
    [Test]
    public void it_should_verify_diff_xml_is_valid()
    {
      var diffXmlData = $"<?xml version=\"1.0\" encoding=\"utf-16\"?><xd:xmldiff version=\"1.0\" srcDocHash=\"5708212576896487287\" options=\"None\" fragments=\"no\" xmlns:xd=\"http://www.microsoft.com/xmldiff\"><xd:node match=\"2\"><xd:node match=\"3\"/><xd:add><e>Some text 4</e><f>Some text 5</f></xd:add><xd:node match=\"4\"><xd:change match=\"1\">Changed text</xd:change><xd:remove match=\"2\"/></xd:node><xd:node match=\"5\"><xd:remove match=\"@secondAttr\"/><xd:add type=\"2\" name=\"newAttr\">new value</xd:add><xd:change match=\"@firstAttr\">changed attribute value</xd:change></xd:node><xd:remove match=\"6\" opid=\"1\"/><xd:add type=\"1\" name=\"p\"><xd:add type=\"1\" name=\"q\"><xd:add match=\"/2/6\" opid=\"1\"/></xd:add></xd:add></xd:node><xd:descriptor opid=\"1\" type=\"move\"/></xd:xmldiff>";


      //Act
      var xDocument = IsWellFormedXml(diffXmlData);

      Assert.IsNotNull(xDocument);
    }

    [Test]
    public void it_should_add_custom_attribute_to_node()
    {
      var xNode = new XElement("TestNode");
      var value = "1";

      //Act
      AddCustomAttributeToNode(xNode, value);

      Assert.AreEqual($"<TestNode {CustomAttribute.ParentNodePath}=\"{value}\" />", xNode.ToString());
    }

    [Test]
    public void it_should_get_all_xd_add_node_from_diff_xml()
    { }
    [Test]
    public void it_should_get_all_xd_change_node_diff_xml()
    { }

    [Test]
    public void FindRoot_method_should_add_cs_base_attribute()
    {
      XDocument diffXDoc = new XDocument();
      diffXDoc = XDocument.Parse("<xd:xmldiff version=\"1.0\" srcDocHash=\"1760821081405616385\" options=\"None\" fragments=\"no\" xmlns:xd=\"http://schemas.microsoft.com/xmltools/2002/xmldiff\"><xd:node match =\"1\"><xd:change match =\"1\" name =\"yy\"/><xd:node match =\"3\"/><xd:add><e>Some text 4 </e><f>Some text 5</f></xd:add><xd:node match =\"4\"><xd:change match =\"1\">Changed text</xd:change><xd:remove match =\"2\"/></xd:node><xd:node match =\"5\"><xd:remove match =\"@secondAttr\"/><xd:add type =\"2\" name =\"newAttr\">new value</xd:add><xd:change match =\"@firstAttr\">changed attribute value</xd:change></xd:node><xd:remove match =\"6\" opid =\"1\"/><xd:add type =\"1\" name =\"p\"><xd:add type =\"1\" name =\"q\"><xd:add match =\"/1/6\" opid =\"1\"/></xd:add></xd:add></xd:node><xd:descriptor opid =\"1\" type =\"move\"/></xd:xmldiff>");
      XDocument expectedResult = new XDocument();
      expectedResult = XDocument.Parse("<xd:xmldiff version=\"1.0\" srcDocHash=\"1760821081405616385\" options=\"None\" fragments=\"no\" xmlns:xd=\"http://schemas.microsoft.com/xmltools/2002/xmldiff\"><xd:node match =\"1\" cs_base =\"true\"><xd:change match =\"1\" name =\"yy\" /><xd:node match =\"3\" /><xd:add><e>Some text 4 </e><f>Some text 5</f></xd:add><xd:node match =\"4\"><xd:change match =\"1\">Changed text</xd:change><xd:remove match =\"2\" /></xd:node><xd:node match =\"5\"><xd:remove match =\"@secondAttr\" /><xd:add type =\"2\" name =\"newAttr\">new value</xd:add><xd:change match =\"@firstAttr\">changed attribute value</xd:change></xd:node><xd:remove match =\"6\" opid =\"1\" /><xd:add type =\"1\" name =\"p\"><xd:add type =\"1\" name =\"q\"><xd:add match =\"/1/6\" opid =\"1\" /></xd:add></xd:add></xd:node><xd:descriptor opid =\"1\" type =\"move\" /></xd:xmldiff>");
      Assert.AreEqual(expectedResult.ToString(), diffXmlProcessor.FindRootNode(diffXDoc).ToString());
    }

    [Test]
    public void FindRoot_method_should_not_return_null_document()
    {

      XDocument diffXDoc = new XDocument();
      diffXDoc = XDocument.Parse("<xd:xmldiff version=\"1.0\" srcDocHash=\"3651185247969030976\" options=\"None\" fragments=\"no\" xmlns:xd=\"http://schemas.microsoft.com/xmltools/2002/xmldiff\"><xd:node match =\"1\"><xd:change match =\"1\" name =\"yy\"/></xd:node></xd:xmldiff>");
      Assert.AreNotEqual(null, diffXmlProcessor.FindRootNode(diffXDoc).ToString());
    }

    [Test]
    public void when_FindChildFamily_method_call_it_should_add_cs_parent_attribute_and_parent_pointer()
    {
      XDocument diffXDoc = new XDocument();
      diffXDoc = XDocument.Parse("<xd:xmldiff version=\"1.0\" srcDocHash=\"1760821081405616385\" options=\"None\" fragments=\"no\" xmlns:xd=\"http://schemas.microsoft.com/xmltools/2002/xmldiff\"><xd:node match =\"1\" cs_base =\"true\"><xd:change match =\"1\" name =\"yy\" /><xd:node match =\"3\" /><xd:add><e>Some text 4</e><f>Some text 5</f></xd:add><xd:node match =\"4\"><xd:change match =\"1\">Changed text</xd:change><xd:remove match =\"2\" /></xd:node><xd:node match =\"5\"><xd:remove match =\"@secondAttr\" /><xd:add type =\"2\" name =\"newAttr\">new value</xd:add><xd:change match =\"@firstAttr\">changed attribute value</xd:change></xd:node><xd:remove match =\"6\" opid =\"1\" /><xd:add type =\"1\" name =\"p\"><xd:add type =\"1\" name =\"q\"><xd:add match =\"/1/6\" opid =\"1\" /></xd:add></xd:add></xd:node><xd:descriptor opid =\"1\" type =\"move\" /></xd:xmldiff>");
      XDocument expectedResult = new XDocument();
      expectedResult = XDocument.Parse("<xd:xmldiff version=\"1.0\" srcDocHash=\"1760821081405616385\" options=\"None\" fragments=\"no\" xmlns:xd=\"http://schemas.microsoft.com/xmltools/2002/xmldiff\"><xd:node match= \"1\" cs_base =\"true\"><xd:change match =\"1\" name =\"yy\" cs_parent =\"1\" /><xd:node match =\"3\" /><xd:add cs_parent =\"1/3\"><e>Some text 4</e><f>Some text 5</f></xd:add><xd:node match =\"4\"><xd:change match =\"1\" cs_parent =\"1/4\">Changed text</xd:change><xd:remove match =\"2\" cs_parent =\"1/4\" /></xd:node><xd:node match =\"5\"><xd:remove match =\"@secondAttr\" cs_parent =\"1/5\" /><xd:add type =\"2\" name =\"newAttr\" cs_parent =\"1/5\">new value</xd:add><xd:change match =\"@firstAttr\" cs_parent =\"1/5\">changed attribute value</xd:change></xd:node><xd:remove match =\"6\" opid =\"1\" cs_parent =\"1\" /><xd:add type =\"1\" name =\"p\" cs_parent =\"1/5\"><xd:add type =\"1\" name =\"q\"><xd:add match =\"/1/6\" opid =\"1\" /></xd:add></xd:add></xd:node><xd:descriptor opid =\"1\" type =\"move\" /></xd:xmldiff>");
      Assert.AreEqual(expectedResult.ToString(), diffXmlProcessor.FindChildFamily(diffXDoc).ToString());
    }
    [Test]
    public void when_FindChildFamily_method_call_it_should_add_cs_parent_attribute_and_parent_pointer_and_return_value_should_not_be_null()
    {
      XDocument diffXDoc = new XDocument();
      diffXDoc = XDocument.Parse("<xd:xmldiff version=\"1.0\" srcDocHash=\"1760821081405616385\" options=\"None\" fragments=\"no\" xmlns:xd=\"http://schemas.microsoft.com/xmltools/2002/xmldiff\"><xd:node match =\"1\" cs_base =\"true\"><xd:change match =\"1\" name =\"yy\" /><xd:node match =\"3\" /><xd:add><e>Some text 4</e><f>Some text 5</f></xd:add><xd:node match =\"4\"><xd:change match =\"1\">Changed text</xd:change><xd:remove match =\"2\" /></xd:node><xd:node match =\"5\"><xd:remove match =\"@secondAttr\" /><xd:add type =\"2\" name =\"newAttr\">new value</xd:add><xd:change match =\"@firstAttr\">changed attribute value</xd:change></xd:node><xd:remove match =\"6\" opid =\"1\" /><xd:add type =\"1\" name =\"p\"><xd:add type =\"1\" name =\"q\"><xd:add match =\"/1/6\" opid =\"1\" /></xd:add></xd:add></xd:node><xd:descriptor opid =\"1\" type =\"move\" /></xd:xmldiff>");
      Assert.AreNotEqual(null, diffXmlProcessor.FindChildFamily(diffXDoc).ToString());
    }
    [Test]
    public void when_GetParentPointer_Method_call_it_should_return_the_parent_pointer()
    {
      XElement diffXDoc = XElement.Parse("<xd:xmldiff version=\"1.0\" srcDocHash=\"3651185247969030976\" options=\"None\" fragments=\"no\" xmlns:xd=\"http://schemas.microsoft.com/xmltools/2002/xmldiff\"><xd:node match =\"1\" cs_base =\"true\"><xd:change match =\"1\" name =\"yy\" cs_parent =\"1\" /></xd:node></xd:xmldiff>");
      string expectedResult = "1";
      string z = CommonUtilities.GetParentpointer(diffXDoc.Descendants().First(x => x.Name.LocalName == "node")).ToString();
      Assert.AreEqual(expectedResult, z);
    }
    [Test]
    public void when_GetParentPointer_Method_call_it_should_not_return_the_null()
    {
      XElement diffXDoc = XElement.Parse("<xd:xmldiff version=\"1.0\" srcDocHash=\"3651185247969030976\" options=\"None\" fragments=\"no\" xmlns:xd=\"http://schemas.microsoft.com/xmltools/2002/xmldiff\"><xd:node match =\"1\" cs_base =\"true\"><xd:change match =\"1\" name =\"yy\" cs_parent =\"1\" /></xd:node></xd:xmldiff>");
      Assert.AreNotEqual(null, CommonUtilities.GetParentpointer(diffXDoc.Descendants().First(x => x.Name.LocalName == "node")));
    }
    [Test]
    public void when_GetBeforeIFActualNd_Method_call_it_should_return_the_()
    {
      AddCrestId addCrestId=new AddCrestId();
      XDocument resultDox = XDocument.Parse("<b><a>Some text 1</a></b>");
      string diffXDoc = "1";
      XElement expectedResult = XElement.Parse("<b><a>Some text 1</a></b>");
      Assert.AreEqual(expectedResult.ToString(), addCrestId.GetBeforeIFActualNd(diffXDoc, resultDox).ToString());
    }
    [Test]
    public void when_GetBeforeIFActualNd_Method_call_it_should_not_return_null()
    {
      AddCrestId addCrestId = new AddCrestId();
      XDocument resultDox = XDocument.Parse("<b><a>Some text 1</a></b>");
      string diffXDoc = "1";
      Assert.AreNotEqual(null, addCrestId.GetBeforeIFActualNd(diffXDoc, resultDox).ToString());
    }
    [Test]
    public void when_GetFamilyOfDeleteAndChange_Method_call_it_should_add_cs_parent_and_parent_pointer()
    {
      XDocument diffXDoc = XDocument.Parse("<xd:xmldiff version=\"1.0\" srcDocHash=\"3651185247969030976\" options=\"None\" fragments=\"no\" xmlns:xd=\"http://schemas.microsoft.com/xmltools/2002/xmldiff\"><xd:node match =\"1\" cs_base =\"true\"><xd:change match =\"1\" name =\"yy\" /></xd:node></xd:xmldiff>");
      XDocument expectedResult = XDocument.Parse("<xd:xmldiff version=\"1.0\" srcDocHash=\"3651185247969030976\" options=\"None\" fragments=\"no\" xmlns:xd=\"http://schemas.microsoft.com/xmltools/2002/xmldiff\"><xd:node match =\"1\" cs_base =\"true\"><xd:change match =\"1\" name =\"yy\" cs_parent =\"1\" /></xd:node></xd:xmldiff>");
      Assert.AreEqual(expectedResult.ToString(), diffXmlProcessor.GetFamilyOfDeleteAndChange("change", diffXDoc).ToString());
    }
    [Test]
    public void when_GetFamilyOfDeleteAndChange_Method_call_it_should_not_be_null()
    {
      XDocument diffXDoc = XDocument.Parse("<xd:xmldiff version=\"1.0\" srcDocHash=\"3651185247969030976\" options=\"None\" fragments=\"no\" xmlns:xd=\"http://schemas.microsoft.com/xmltools/2002/xmldiff\"><xd:node match =\"1\" cs_base =\"true\"><xd:change match =\"1\" name =\"yy\" /></xd:node></xd:xmldiff>");
      Assert.AreNotEqual(null, diffXmlProcessor.GetFamilyOfDeleteAndChange("change", diffXDoc).ToString());
    }
    [Test]
    public void when_GetFamilyOfAdd_Method_call_it_should_add_cs_base_and_pointer()
    {
      XDocument diffXDoc = XDocument.Parse("<xd:xmldiff version =\"1.0\" srcDocHash=\"3651185247969030976\" options=\"None\" fragments=\"no\" xmlns:xd=\"http://schemas.microsoft.com/xmltools/2002/xmldiff\"><xd:node match =\"1\" cs_base=\"true\"><xd:node match =\"1\" /><xd:add><b>Some text 2</b><c>Some text 3</c></xd:add></xd:node></xd:xmldiff>");
      XDocument expectedResult = XDocument.Parse("<xd:xmldiff version =\"1.0\" srcDocHash=\"3651185247969030976\" options=\"None\" fragments=\"no\" xmlns:xd=\"http://schemas.microsoft.com/xmltools/2002/xmldiff\"><xd:node match =\"1\" cs_base=\"true\"><xd:node match =\"1\" /><xd:add cs_parent=\"1/1\"><b>Some text 2</b><c>Some text 3</c></xd:add></xd:node></xd:xmldiff>");
      Assert.AreEqual(expectedResult.ToString(), diffXmlProcessor.GetFamilyOfAdd(diffXDoc).ToString());
    }
    [Test]
    public void when_GetFamilyOfAdd_Method_call_it_should_not_be_null()
    {
      XDocument diffXDoc = XDocument.Parse("<xd:xmldiff version =\"1.0\" srcDocHash=\"3651185247969030976\" options=\"None\" fragments=\"no\" xmlns:xd=\"http://schemas.microsoft.com/xmltools/2002/xmldiff\"><xd:node match =\"1\" cs_base=\"true\"><xd:node match =\"1\" /><xd:add><b>Some text 2</b><c>Some text 3</c></xd:add></xd:node></xd:xmldiff>");
      Assert.AreNotEqual(null, diffXmlProcessor.GetFamilyOfAdd(diffXDoc).ToString());
    }
    [Test]
    public void when_CheckPreviosSibling_Method_call_it_should_return_previous_sibling()
    {
      //XElement diffXDoc = XElement.Parse("<xd:add xmlns:xd=\"http://schemas.microsoft.com/xmltools/2002/xmldiff\"><e>Some text 4</e><f>Some text 5</f></xd:add>");
      XElement diffXDoc = XElement.Parse("<xd:xmldiff version=\"1.0\" srcDocHash=\"1760821081405616385\" options=\"None\" fragments=\"no\" xmlns:xd=\"http://schemas.microsoft.com/xmltools/2002/xmldiff\"><xd:node match=\"1\" cs_base=\"true\"><xd:change match=\"1\" name=\"yy\" cs_parent=\"1\" /><xd:node match=\"3\" /><xd:add><e>Some text 4</e><f>Some text 5</f></xd:add><xd:node match=\"4\"><xd:change match=\"1\" cs_parent=\"1/4\">Changed text</xd:change><xd:remove match=\"2\" cs_parent=\"1/4\" /></xd:node><xd:node match=\"5\"><xd:remove match=\"@secondAttr\" cs_parent=\"1/5\" /><xd:add type=\"2\" name=\"newAttr\">new value</xd:add><xd:change match=\"@firstAttr\" cs_parent=\"1/5\">changed attribute value</xd:change></xd:node><xd:remove match=\"6\" opid=\"1\" cs_parent=\"1\" /><xd:add type=\"1\" name=\"p\"><xd:add type=\"1\" name=\"q\"><xd:add match=\"/1/6\" opid=\"1\" /></xd:add></xd:add></xd:node><xd:descriptor opid=\"1\" type=\"move\" /></xd:xmldiff>");
      XElement expectedResult = XElement.Parse("<xd:node match=\"3\" xmlns:xd=\"http://schemas.microsoft.com/xmltools/2002/xmldiff\" />");
      //"<xd:node match=\"1\"  xmlns:xd=\"http://schemas.microsoft.com/xmltools/2002/xmldiff\" cs_base=\"true\"><xd:change match=\"1\" name=\"yy\" cs_parent=\"1\" /><xd:node match=\"3\" /><xd:add><e>Some text 4</e><f>Some text 5</f></xd:add></xd:node>");
      Assert.AreEqual(expectedResult.ToString(), diffXmlProcessor.CheckPreviosSibling(diffXDoc.Descendants().First(x => x.Name.LocalName == "add")).ToString());
    }
    [Test]
    public void when_CheckPreviosSibling_Method_call_it_should_not_be_null()
    {
      XElement diffXDoc = XElement.Parse("<xd:xmldiff version=\"1.0\" srcDocHash=\"1760821081405616385\" options=\"None\" fragments=\"no\" xmlns:xd=\"http://schemas.microsoft.com/xmltools/2002/xmldiff\"><xd:node match=\"1\" cs_base=\"true\"><xd:change match=\"1\" name=\"yy\" cs_parent=\"1\" /><xd:node match=\"3\" /><xd:add><e>Some text 4</e><f>Some text 5</f></xd:add><xd:node match=\"4\"><xd:change match=\"1\" cs_parent=\"1/4\">Changed text</xd:change><xd:remove match=\"2\" cs_parent=\"1/4\" /></xd:node><xd:node match=\"5\"><xd:remove match=\"@secondAttr\" cs_parent=\"1/5\" /><xd:add type=\"2\" name=\"newAttr\">new value</xd:add><xd:change match=\"@firstAttr\" cs_parent=\"1/5\">changed attribute value</xd:change></xd:node><xd:remove match=\"6\" opid=\"1\" cs_parent=\"1\" /><xd:add type=\"1\" name=\"p\"><xd:add type=\"1\" name=\"q\"><xd:add match=\"/1/6\" opid=\"1\" /></xd:add></xd:add></xd:node><xd:descriptor opid=\"1\" type=\"move\" /></xd:xmldiff>");
      Assert.AreNotEqual(null, diffXmlProcessor.CheckPreviosSibling(diffXDoc.Descendants().First(x => x.Name.LocalName == "add")).ToString());
    }

  }

  [TestFixture]
  public class When_Result_Xml_Processing_Start : ResultXmlProcessor
  {
    private ResultXmlProcessor resultXmlProcessor = new ResultXmlProcessor();
    XDocument diffXDoc = XDocument.Parse("<xd:xmldiff version=\"1.0\" srcDocHash=\"1760821081405616385\" options=\"None\" fragments=\"no\" xmlns:xd=\"http://schemas.microsoft.com/xmltools/2002/xmldiff\"><xd:node match=\"1\" cs_base=\"true\"><xd:change match=\"1\" name=\"yy\" cs_parent=\"1\" /><xd:node match=\"3\" /><xd:add cs_parent=\"1/3\"><e>Some text 4</e><f>Some text 5</f></xd:add><xd:node match=\"4\"><xd:change match=\"1\" cs_parent=\"1/4\">Changed text</xd:change><xd:remove match=\"2\" cs_parent=\"1/4\" /></xd:node><xd:node match=\"5\"><xd:remove match=\"@secondAttr\" cs_parent=\"1/5\" /><xd:add type=\"2\" name=\"newAttr\" cs_parent=\"1/5\">new value</xd:add><xd:change match=\"@firstAttr\" cs_parent=\"1/5\">changed attribute value</xd:change></xd:node><xd:remove match=\"6\" opid=\"1\" cs_parent=\"1\" /><xd:add type=\"1\" name=\"p\" cs_parent=\"1/5\"><xd:add type=\"1\" name=\"q\"><xd:add match=\"/1/6\" opid=\"1\" /></xd:add></xd:add></xd:node><xd:descriptor opid=\"1\" type=\"move\" /></xd:xmldiff>");
    [Test]
    public void when_GetActualNd_Method_call_it_should_return_paticular_node()
    {
      XElement xElement = XElement.Parse("<xd:change match=\"1\" name=\"yy\" cs_parent=\"1\" xmlns:xd=\"http://schemas.microsoft.com/xmltools/2002/xmldiff\" />");
      XNode expectedResult = XDocument.Parse("<b cs_crestid=\"1\"><a>Some text 1</a><b>Some text 2</b><c cs_crestid=\"3\">Some text 3</c><d cs_crestid=\"4\">Another text<fob /></d><x firstAttr=\"value1\" secondAttr=\"value2\" cs_crestid=\"5\" /><y><!--Any comments?--><z id=\"10\">Just another text</z></y></b>");
      XDocument resultXDoc = XDocument.Parse("<b cs_crestid=\"1\"><a>Some text 1</a><b>Some text 2</b><c cs_crestid=\"3\">Some text 3</c><d cs_crestid=\"4\">Another text<fob /></d><x firstAttr=\"value1\" secondAttr=\"value2\" cs_crestid=\"5\" /><y><!--Any comments?--><z id=\"10\">Just another text</z></y></b>");
      Assert.AreEqual(expectedResult.ToString(), CommonUtilities.GetActualNd("1", xElement, resultXDoc).ToString());
    }
    [Test]
    public void when_GetActualNd_Method_call_it_should_not_return_null()
    {
      XElement xElement = XElement.Parse("<xd:change match=\"1\" name=\"yy\" cs_parent=\"1\" xmlns:xd=\"http://schemas.microsoft.com/xmltools/2002/xmldiff\" />");
      XDocument resultXDoc = XDocument.Parse("<b cs_crestid=\"1\"><a>Some text 1</a><b>Some text 2</b><c cs_crestid=\"3\">Some text 3</c><d cs_crestid=\"4\">Another text<fob /></d><x firstAttr=\"value1\" secondAttr=\"value2\" cs_crestid=\"5\" /><y><!--Any comments?--><z id=\"10\">Just another text</z></y></b>");
      Assert.AreNotEqual(null, CommonUtilities.GetActualNd("1", xElement, resultXDoc).ToString());
    }

    [Test]
    public void when_GetActualNd_Method_call_and_it_contain_slash_in_parentpostion_parameter_it_should_return_paticular_node()
    {
      XElement xElement = XElement.Parse("<xd:change match=\"1\" cs_parent=\"1/4\" xmlns:xd=\"http://schemas.microsoft.com/xmltools/2002/xmldiff\">Changed text</xd:change>");
      XNode expectedResult = XDocument.Parse("<d cs_crestid=\"4\">Another text<fob /></d>");
      XDocument resultXDoc = XDocument.Parse("<b cs_crestid=\"1\"><a OldName=\"a\" NewName=\"yy\">Some text 1</a><b>Some text 2</b><c cs_crestid=\"3\">Some text 3</c><d cs_crestid=\"4\">Another text<fob /></d><x firstAttr=\"value1\" secondAttr=\"value2\" cs_crestid=\"5\" /><y><!--Any comments?--><z id=\"10\">Just another text</z></y></b>");
      Assert.AreEqual(expectedResult.ToString(), CommonUtilities.GetActualNd("1/4", xElement, resultXDoc).ToString());
    }
    [Test]
    public void when_GetActualNd_Method_call_and_it_contain_slash_in_parentpostion_parameter_it_should_not_return_null()
    {
      XElement xElement = XElement.Parse("<xd:change match=\"1\" cs_parent=\"1/4\" xmlns:xd=\"http://schemas.microsoft.com/xmltools/2002/xmldiff\">Changed text</xd:change>");
      XDocument resultXDoc = XDocument.Parse("<b cs_crestid=\"1\"><a OldName=\"a\" NewName=\"yy\">Some text 1</a><b>Some text 2</b><c cs_crestid=\"3\">Some text 3</c><d cs_crestid=\"4\">Another text<fob /></d><x firstAttr=\"value1\" secondAttr=\"value2\" cs_crestid=\"5\" /><y><!--Any comments?--><z id=\"10\">Just another text</z></y></b>");
      Assert.AreNotEqual(null, CommonUtilities.GetActualNd("1/4", xElement, resultXDoc).ToString());
    }
    [Test]
    public void when_ChangeAttrNode_Method_call_()//
    {
      ChangeAttributeOfNode changeAttributeOfNode = new ChangeAttributeOfNode();
      XDocument expectedResult = XDocument.Parse("<b cs_crestid=\"1\"><a OldName=\"a\" NewName=\"yy\">Some text 1</a><b>Some text 2</b><c cs_crestid=\"3\">Some text 3</c><d cs_crestid=\"4\">Another text<fob /></d><x firstAttr=\"value1\" secondAttr=\"value2\" cs_crestid=\"5\" /><y><!--Any comments?--><z id=\"10\">Just another text</z></y></b>");
      XDocument resultXDoc = XDocument.Parse("<b cs_crestid=\"1\"><a>Some text 1</a><b>Some text 2</b><c cs_crestid=\"3\">Some text 3</c><d cs_crestid=\"4\">Another text<fob /></d><x firstAttr=\"value1\" secondAttr=\"value2\" cs_crestid=\"5\" /><y><!--Any comments?--><z id=\"10\">Just another text</z></y></b>");
      Assert.AreEqual(expectedResult.ToString(), changeAttributeOfNode.ChangeAttrNode(diffXDoc, resultXDoc).ToString());
    }
    [Test]
    public void when_ChangeAttrNode_Method_call_it_should_not_return_null()//
    {
      ChangeAttributeOfNode changeAttributeOfNode = new ChangeAttributeOfNode();
      XDocument resultXDoc = XDocument.Parse("<b cs_crestid=\"1\"><a>Some text 1</a><b>Some text 2</b><c cs_crestid=\"3\">Some text 3</c><d cs_crestid=\"4\">Another text<fob /></d><x firstAttr=\"value1\" secondAttr=\"value2\" cs_crestid=\"5\" /><y><!--Any comments?--><z id=\"10\">Just another text</z></y></b>");
      Assert.AreNotEqual(null, changeAttributeOfNode.ChangeAttrNode(diffXDoc, resultXDoc).ToString());
    }
    [Test]
    public void when_ChangeNodeText_Method_call_()//
    {
      ChangeTheTextOfResultXml changeTheTextOfResultXml = new ChangeTheTextOfResultXml();
      XDocument expectedResult = XDocument.Parse("<b cs_crestid=\"1\"><a OldName=\"a\" NewName=\"yy\">Some text 1</a><b>Some text 2</b><c cs_crestid=\"3\">Some text 3</c><d cs_crestid=\"4\"><textchanged><del>Another text</del><ins>Changed text</ins></textchanged><fob /></d><x firstAttr=\"changed attribute value\" secondAttr=\"value2\" cs_crestid=\"5\" oldattr_firstAttr=\"value1\" /><y><!--Any comments?--><z id=\"10\">Just another text</z></y></b>");
      XDocument resultXDoc = XDocument.Parse("<b cs_crestid=\"1\"><a OldName=\"a\" NewName=\"yy\">Some text 1</a><b>Some text 2</b><c cs_crestid=\"3\">Some text 3</c><d cs_crestid=\"4\">Another text<fob /></d><x firstAttr=\"value1\" secondAttr=\"value2\" cs_crestid=\"5\" /><y><!--Any comments?--><z id=\"10\">Just another text</z></y></b>");
      Assert.AreEqual(expectedResult.ToString(), changeTheTextOfResultXml.ChangeNodeText(diffXDoc, resultXDoc).ToString());
    }
    [Test]
    public void when_ChangeNodeText_Method_call_it_should_not_return_null()//
    {
      ChangeTheTextOfResultXml changeTheTextOfResultXml = new ChangeTheTextOfResultXml();
      XDocument resultXDoc = XDocument.Parse("<b cs_crestid=\"1\"><a OldName=\"a\" NewName=\"yy\">Some text 1</a><b>Some text 2</b><c cs_crestid=\"3\">Some text 3</c><d cs_crestid=\"4\">Another text<fob /></d><x firstAttr=\"value1\" secondAttr=\"value2\" cs_crestid=\"5\" /><y><!--Any comments?--><z id=\"10\">Just another text</z></y></b>");
      Assert.AreNotEqual(null, changeTheTextOfResultXml.ChangeNodeText(diffXDoc, resultXDoc).ToString());
    }
    [Test]
    public void when_DeleteNode_Method_call_()//
    {
      AddDeletedNodeAndAttributeInformationInResult addDeletedNodeAndAttributeInformationInResult = new AddDeletedNodeAndAttributeInformationInResult();
      XDocument expectedResult = XDocument.Parse("<b cs_crestid=\"1\"><a OldName=\"a\" NewName=\"yy\">Some text 1</a><b>Some text 2</b><c cs_crestid=\"3\">Some text 3</c><d cs_crestid=\"4\"><textchanged><del>Another text</del><ins>Changed text</ins></textchanged><fob Deleted=\"true\" /></d><x firstAttr=\"changed attribute value\" cs_crestid=\"5\" oldattr_firstAttr=\"value1\" removeattr_secondAttr=\"value2\" /><y><!--Any comments?--><z id=\"10\">Just another text</z></y></b>");
      XDocument resultXDoc = XDocument.Parse("<b cs_crestid=\"1\"><a OldName=\"a\" NewName=\"yy\">Some text 1</a><b>Some text 2</b><c cs_crestid=\"3\">Some text 3</c><d cs_crestid=\"4\"><textchanged><del>Another text</del><ins>Changed text</ins></textchanged><fob /></d><x firstAttr=\"changed attribute value\" secondAttr=\"value2\" cs_crestid=\"5\" oldattr_firstAttr=\"value1\" /><y><!--Any comments?--><z id=\"10\">Just another text</z></y></b>");
      Assert.AreEqual(expectedResult.ToString(), addDeletedNodeAndAttributeInformationInResult.DeleteNode(diffXDoc, resultXDoc).ToString());
    }
    [Test]
    public void when_DeleteNode_Method_call_it_should_not_return_null()//
    {
      AddDeletedNodeAndAttributeInformationInResult addDeletedNodeAndAttributeInformationInResult = new AddDeletedNodeAndAttributeInformationInResult();
      XDocument resultXDoc = XDocument.Parse("<b cs_crestid=\"1\"><a OldName=\"a\" NewName=\"yy\">Some text 1</a><b>Some text 2</b><c cs_crestid=\"3\">Some text 3</c><d cs_crestid=\"4\"><textchanged><del>Another text</del><ins>Changed text</ins></textchanged><fob /></d><x firstAttr=\"changed attribute value\" secondAttr=\"value2\" cs_crestid=\"5\" oldattr_firstAttr=\"value1\" /><y><!--Any comments?--><z id=\"10\">Just another text</z></y></b>");
      Assert.AreNotEqual(null, addDeletedNodeAndAttributeInformationInResult.DeleteNode(diffXDoc, resultXDoc).ToString());
    }
    [Test]
    public void when_MovedNodeAtr_Method_call_()//
    {
      AddMovedAttributeNodeInformationInResult addMovedAttributeNodeInformationInResult = new AddMovedAttributeNodeInformationInResult();
      XDocument expectedResult = XDocument.Parse("<b cs_crestid=\"1\"><a OldName=\"a\" NewName=\"yy\">Some text 1</a><b>Some text 2</b><c cs_crestid=\"3\">Some text 3</c><d cs_crestid=\"4\"><textchanged><del>Another text</del><ins>Changed text</ins></textchanged><fob Deleted=\"true\" /></d><x firstAttr=\"changed attribute value\" cs_crestid=\"5\" oldattr_firstAttr=\"value1\" removeattr_secondAttr=\"value2\" /><y Moved=\"true\" Deleted=\"true\" movedpos=\"1\"><!--Any comments?--><z id=\"10\">Just another text</z></y></b>");
      XDocument resultXDoc = XDocument.Parse("<b cs_crestid=\"1\"><a OldName=\"a\" NewName=\"yy\">Some text 1</a><b>Some text 2</b><c cs_crestid=\"3\">Some text 3</c><d cs_crestid=\"4\"><textchanged><del>Another text</del><ins>Changed text</ins></textchanged><fob Deleted=\"true\" /></d><x firstAttr=\"changed attribute value\" cs_crestid=\"5\" oldattr_firstAttr=\"value1\" removeattr_secondAttr=\"value2\" /><y><!--Any comments?--><z id=\"10\">Just another text</z></y></b>");
      Assert.AreEqual(expectedResult.ToString(), addMovedAttributeNodeInformationInResult.MovedNodeAtr(diffXDoc, resultXDoc).ToString());
    }
    [Test]
    public void when_MovedNodeAtr_Method_call_not_return_null()//
    {
      AddMovedAttributeNodeInformationInResult addMovedAttributeNodeInformationInResult = new AddMovedAttributeNodeInformationInResult();
      XDocument diffXDoc = XDocument.Parse("<xd:xmldiff version=\"1.0\" srcDocHash=\"1760821081405616385\" options=\"None\" fragments=\"no\" xmlns:xd=\"http://schemas.microsoft.com/xmltools/2002/xmldiff\"><xd:node match=\"1\" cs_base=\"true\"><xd:change match=\"1\" name=\"yy\" cs_parent=\"1\" /><xd:node match=\"3\" /><xd:add cs_parent=\"1/3\"><e>Some text 4</e><f>Some text 5</f></xd:add><xd:node match=\"4\"><xd:change match=\"1\" cs_parent=\"1/4\">Changed text</xd:change><xd:remove match=\"2\" cs_parent=\"1/4\" /></xd:node><xd:node match=\"5\"><xd:remove match=\"@secondAttr\" cs_parent=\"1/5\" /><xd:add type=\"2\" name=\"newAttr\" cs_parent=\"1/5\">new value</xd:add><xd:change match=\"@firstAttr\" cs_parent=\"1/5\">changed attribute value</xd:change></xd:node><xd:remove match=\"6\" opid=\"1\" cs_parent=\"1\" /><xd:add type=\"1\" name=\"p\" cs_parent=\"1/5\"><xd:add type=\"1\" name=\"q\"><xd:add match=\"/1/6\" opid=\"1\" /></xd:add></xd:add></xd:node><xd:descriptor opid=\"1\" type=\"move\" /></xd:xmldiff>");
      XDocument resultXDoc = XDocument.Parse("<b cs_crestid=\"1\"><a OldName=\"a\" NewName=\"yy\">Some text 1</a><b>Some text 2</b><c cs_crestid=\"3\">Some text 3</c><d cs_crestid=\"4\"><textchanged><del>Another text</del><ins>Changed text</ins></textchanged><fob Deleted=\"true\" /></d><x firstAttr=\"changed attribute value\" cs_crestid=\"5\" oldattr_firstAttr=\"value1\" removeattr_secondAttr=\"value2\" /><y><!--Any comments?--><z id=\"10\">Just another text</z></y></b>");
      Assert.AreNotEqual(null, addMovedAttributeNodeInformationInResult.MovedNodeAtr(diffXDoc, resultXDoc).ToString());
    }
    [Test]
    public void when_AddNodeattr_Method_call_()//
    {
      AddNewAttributeAndAddedNewNodeInformationInResult addNewAttributeAndAddedNewNodeInformationInResult = new AddNewAttributeAndAddedNewNodeInformationInResult();
      XDocument expectedResult = XDocument.Parse("<b cs_crestid=\"1\"><a OldName=\"a\" NewName=\"yy\">Some text 1</a><b>Some text 2</b><c cs_crestid=\"3\">Some text 3</c><d cs_crestid=\"4\"><textchanged><del>Another text</del><ins>Changed text</ins></textchanged><fob Deleted=\"true\" /></d><x firstAttr=\"changed attribute value\" cs_crestid=\"5\" oldattr_firstAttr=\"value1\" removeattr_secondAttr=\"value2\" Addattrname_newAttr=\"new value\" /><y Moved=\"true\" Deleted=\"true\" movedpos=\"1\"><!--Any comments?--><z id=\"10\">Just another text</z></y></b>");
      XDocument resultXDoc = XDocument.Parse("<b cs_crestid=\"1\"><a OldName=\"a\" NewName=\"yy\">Some text 1</a><b>Some text 2</b><c cs_crestid=\"3\">Some text 3</c><d cs_crestid=\"4\"><textchanged><del>Another text</del><ins>Changed text</ins></textchanged><fob Deleted=\"true\" /></d><x firstAttr=\"changed attribute value\" cs_crestid=\"5\" oldattr_firstAttr=\"value1\" removeattr_secondAttr=\"value2\" /><y Moved=\"true\" Deleted=\"true\" movedpos=\"1\"><!--Any comments?--><z id=\"10\">Just another text</z></y></b>");
      Assert.AreEqual(expectedResult.ToString(), addNewAttributeAndAddedNewNodeInformationInResult.AddNodeattr(diffXDoc, resultXDoc).ToString());
    }
    [Test]
    public void when_AddNodeattr_Method_call_should_not_return_null()//
    {
      AddNewAttributeAndAddedNewNodeInformationInResult addNewAttributeAndAddedNewNodeInformationInResult = new AddNewAttributeAndAddedNewNodeInformationInResult();
      XDocument resultXDoc = XDocument.Parse("<b cs_crestid=\"1\"><a OldName=\"a\" NewName=\"yy\">Some text 1</a><b>Some text 2</b><c cs_crestid=\"3\">Some text 3</c><d cs_crestid=\"4\"><textchanged><del>Another text</del><ins>Changed text</ins></textchanged><fob Deleted=\"true\" /></d><x firstAttr=\"changed attribute value\" cs_crestid=\"5\" oldattr_firstAttr=\"value1\" removeattr_secondAttr=\"value2\" /><y Moved=\"true\" Deleted=\"true\" movedpos=\"1\"><!--Any comments?--><z id=\"10\">Just another text</z></y></b>");
      Assert.AreNotEqual(null, addNewAttributeAndAddedNewNodeInformationInResult.AddNodeattr(diffXDoc, resultXDoc).ToString());
    }
    [Test]
    public void when_AddSingleNode_Method_call_()//
    {
      AddNewAttributeAndAddedNewNodeInformationInResult addNewAttributeAndAddedNewNodeInformationInResult = new AddNewAttributeAndAddedNewNodeInformationInResult();
      XDocument expectedResult = XDocument.Parse("<b cs_crestid=\"1\"><a OldName=\"a\" NewName=\"yy\">Some text 1</a><b>Some text 2</b><c cs_crestid=\"3\">Some text 3</c><Addition><e>Some text 4</e><f>Some text 5</f></Addition><d cs_crestid=\"4\"><textchanged><del>Another text</del><ins>Changed text</ins></textchanged><fob Deleted=\"true\" /></d><x firstAttr=\"changed attribute value\" cs_crestid=\"5\" oldattr_firstAttr=\"value1\" removeattr_secondAttr=\"value2\" Addattrname_newAttr=\"new value\" /><Addition><p><q><y Moved=\"true\" Deleted=\"true\" movedpos=\"1\"><!--Any comments?--><z id=\"10\">Just another text</z></y></q></p></Addition><y Moved=\"true\" Deleted=\"true\" movedpos=\"1\"><!--Any comments?--><z id=\"10\">Just another text</z></y></b>");
      XDocument resultXDoc = XDocument.Parse("<b cs_crestid=\"1\"><a OldName=\"a\" NewName=\"yy\">Some text 1</a><b>Some text 2</b><c cs_crestid=\"3\">Some text 3</c><d cs_crestid=\"4\"><textchanged><del>Another text</del><ins>Changed text</ins></textchanged><fob Deleted=\"true\" /></d><x firstAttr=\"changed attribute value\" cs_crestid=\"5\" oldattr_firstAttr=\"value1\" removeattr_secondAttr=\"value2\" Addattrname_newAttr=\"new value\" /><y Moved=\"true\" Deleted=\"true\" movedpos=\"1\"><!--Any comments?--><z id=\"10\">Just another text</z></y></b>");
      Assert.AreEqual(expectedResult.ToString(), addNewAttributeAndAddedNewNodeInformationInResult.AddSingleNode(diffXDoc, resultXDoc).ToString());
    }
    [Test]
    public void when_AddSingleNode_Method_call_it_should_not_return_null()//
    {
      AddNewAttributeAndAddedNewNodeInformationInResult addNewAttributeAndAddedNewNodeInformationInResult = new AddNewAttributeAndAddedNewNodeInformationInResult();
      XDocument diffXDoc = XDocument.Parse("<xd:xmldiff version=\"1.0\" srcDocHash=\"1760821081405616385\" options=\"None\" fragments=\"no\" xmlns:xd=\"http://schemas.microsoft.com/xmltools/2002/xmldiff\"><xd:node match=\"1\" cs_base=\"true\"><xd:change match=\"1\" name=\"yy\" cs_parent=\"1\" /><xd:node match=\"3\" /><xd:add cs_parent=\"1/3\"><e>Some text 4</e><f>Some text 5</f></xd:add><xd:node match=\"4\"><xd:change match=\"1\" cs_parent=\"1/4\">Changed text</xd:change><xd:remove match=\"2\" cs_parent=\"1/4\" /></xd:node><xd:node match=\"5\"><xd:remove match=\"@secondAttr\" cs_parent=\"1/5\" /><xd:add type=\"2\" name=\"newAttr\" cs_parent=\"1/5\">new value</xd:add><xd:change match=\"@firstAttr\" cs_parent=\"1/5\">changed attribute value</xd:change></xd:node><xd:remove match=\"6\" opid=\"1\" cs_parent=\"1\" /><xd:add type=\"1\" name=\"p\" cs_parent=\"1/5\"><xd:add type=\"1\" name=\"q\"><xd:add match=\"/1/6\" opid=\"1\" /></xd:add></xd:add></xd:node><xd:descriptor opid=\"1\" type=\"move\" /></xd:xmldiff>");
      XDocument resultXDoc = XDocument.Parse("<b cs_crestid=\"1\"><a OldName=\"a\" NewName=\"yy\">Some text 1</a><b>Some text 2</b><c cs_crestid=\"3\">Some text 3</c><d cs_crestid=\"4\"><textchanged><del>Another text</del><ins>Changed text</ins></textchanged><fob Deleted=\"true\" /></d><x firstAttr=\"changed attribute value\" cs_crestid=\"5\" oldattr_firstAttr=\"value1\" removeattr_secondAttr=\"value2\" Addattrname_newAttr=\"new value\" /><y Moved=\"true\" Deleted=\"true\" movedpos=\"1\"><!--Any comments?--><z id=\"10\">Just another text</z></y></b>");
      Assert.AreNotEqual(null, addNewAttributeAndAddedNewNodeInformationInResult.AddSingleNode(diffXDoc, resultXDoc).ToString());
    }
    [Test]
    public void when_ProcessNode_Method_call_()//
    {
      AddNewAttributeAndAddedNewNodeInformationInResult addNewAttributeAndAddedNewNodeInformationInResult = new AddNewAttributeAndAddedNewNodeInformationInResult();
      XElement xElement = XElement.Parse("<xd:add type=\"1\" name=\"p\" cs_parent=\"1/5\" xmlns:xd=\"http://schemas.microsoft.com/xmltools/2002/xmldiff\"><xd:add type=\"1\" name=\"q\"><xd:add match=\"/1/6\" opid=\"1\" /></xd:add></xd:add>");
      XDocument expectedResult = XDocument.Parse("<b cs_crestid=\"1\"><a OldName=\"a\" NewName=\"yy\">Some text 1</a><b>Some text 2</b><c cs_crestid=\"3\">Some text 3</c><Addition><e>Some text 4</e><f>Some text 5</f></Addition><d cs_crestid=\"4\"><textchanged><del>Another text</del><ins>Changed text</ins></textchanged><fob Deleted=\"true\" /></d><x firstAttr=\"changed attribute value\" cs_crestid=\"5\" oldattr_firstAttr=\"value1\" removeattr_secondAttr=\"value2\" Addattrname_newAttr=\"new value\" /><Addition><p><q><y Moved=\"true\" Deleted=\"true\" movedpos=\"1\"><!--Any comments?--><z id=\"10\">Just another text</z></y></q></p></Addition><y Moved=\"true\" Deleted=\"true\" movedpos=\"1\"><!--Any comments?--><z id=\"10\">Just another text</z></y></b>");
      XDocument resultXDoc = XDocument.Parse("<b cs_crestid=\"1\"><a OldName=\"a\" NewName=\"yy\">Some text 1</a><b>Some text 2</b><c cs_crestid=\"3\">Some text 3</c><Addition><e>Some text 4</e><f>Some text 5</f></Addition><d cs_crestid=\"4\"><textchanged><del>Another text</del><ins>Changed text</ins></textchanged><fob Deleted=\"true\" /></d><x firstAttr=\"changed attribute value\" cs_crestid=\"5\" oldattr_firstAttr=\"value1\" removeattr_secondAttr=\"value2\" Addattrname_newAttr=\"new value\" /><y Moved=\"true\" Deleted=\"true\" movedpos=\"1\"><!--Any comments?--><z id=\"10\">Just another text</z></y></b>");
      Assert.AreEqual(expectedResult.ToString(), addNewAttributeAndAddedNewNodeInformationInResult.ProcessNode(xElement, diffXDoc, resultXDoc).ToString());
    }
    [Test]
    public void when_ProcessNode_Method_call_it_should_not_return_null()//
    {
      AddNewAttributeAndAddedNewNodeInformationInResult addNewAttributeAndAddedNewNodeInformationInResult = new AddNewAttributeAndAddedNewNodeInformationInResult();
      XElement xElement = XElement.Parse("<xd:add type=\"1\" name=\"p\" cs_parent=\"1/5\" xmlns:xd=\"http://schemas.microsoft.com/xmltools/2002/xmldiff\"><xd:add type=\"1\" name=\"q\"><xd:add match=\"/1/6\" opid=\"1\" /></xd:add></xd:add>");
      XDocument resultXDoc = XDocument.Parse("<b cs_crestid=\"1\"><a OldName=\"a\" NewName=\"yy\">Some text 1</a><b>Some text 2</b><c cs_crestid=\"3\">Some text 3</c><Addition><e>Some text 4</e><f>Some text 5</f></Addition><d cs_crestid=\"4\"><textchanged><del>Another text</del><ins>Changed text</ins></textchanged><fob Deleted=\"true\" /></d><x firstAttr=\"changed attribute value\" cs_crestid=\"5\" oldattr_firstAttr=\"value1\" removeattr_secondAttr=\"value2\" Addattrname_newAttr=\"new value\" /><y Moved=\"true\" Deleted=\"true\" movedpos=\"1\"><!--Any comments?--><z id=\"10\">Just another text</z></y></b>");
      Assert.AreNotEqual(null, addNewAttributeAndAddedNewNodeInformationInResult.ProcessNode(xElement, diffXDoc, resultXDoc).ToString());
    }
    [Test]
    public void when_RenameTag_Method_call_()//
    {
      RenameTagValueInResult renameTagValueInResult = new RenameTagValueInResult();
      XDocument expectedResult = XDocument.Parse("<b cs_crestid=\"1\"><yy OldName=\"a\" NewName=\"yy\" TagChanged=\"true\">Some text 1</yy><b>Some text 2</b><c cs_crestid=\"3\">Some text 3</c><Addition><e>Some text 4</e><f>Some text 5</f></Addition><d cs_crestid=\"4\"><textchanged><del>Another text</del><ins>Changed text</ins></textchanged><fob Deleted=\"true\" /></d><x firstAttr=\"changed attribute value\" cs_crestid=\"5\" oldattr_firstAttr=\"value1\" removeattr_secondAttr=\"value2\" Addattrname_newAttr=\"new value\" /><Addition><p><q><y Moved=\"true\" Deleted=\"true\" movedpos=\"1\"><!--Any comments?--><z id=\"10\">Just another text</z></y></q></p></Addition><y Moved=\"true\" Deleted=\"true\" movedpos=\"1\"><!--Any comments?--><z id=\"10\">Just another text</z></y></b>");
      XDocument resultXDoc = XDocument.Parse("<b cs_crestid=\"1\"><a OldName=\"a\" NewName=\"yy\">Some text 1</a><b>Some text 2</b><c cs_crestid=\"3\">Some text 3</c><Addition><e>Some text 4</e><f>Some text 5</f></Addition><d cs_crestid=\"4\"><textchanged><del>Another text</del><ins>Changed text</ins></textchanged><fob Deleted=\"true\" /></d><x firstAttr=\"changed attribute value\" cs_crestid=\"5\" oldattr_firstAttr=\"value1\" removeattr_secondAttr=\"value2\" Addattrname_newAttr=\"new value\" /><Addition><p><q><y Moved=\"true\" Deleted=\"true\" movedpos=\"1\"><!--Any comments?--><z id=\"10\">Just another text</z></y></q></p></Addition><y Moved=\"true\" Deleted=\"true\" movedpos=\"1\"><!--Any comments?--><z id=\"10\">Just another text</z></y></b>");
      Assert.AreEqual(expectedResult.ToString(), renameTagValueInResult.RenameTag(resultXDoc).ToString());
    }
    [Test]
    public void when_RenameTag_Method_call_it_should_not_return_null()//
    {
      RenameTagValueInResult renameTagValueInResult = new RenameTagValueInResult();
      XDocument resultXDoc = XDocument.Parse("<b cs_crestid=\"1\"><a OldName=\"a\" NewName=\"yy\">Some text 1</a><b>Some text 2</b><c cs_crestid=\"3\">Some text 3</c><Addition><e>Some text 4</e><f>Some text 5</f></Addition><d cs_crestid=\"4\"><textchanged><del>Another text</del><ins>Changed text</ins></textchanged><fob Deleted=\"true\" /></d><x firstAttr=\"changed attribute value\" cs_crestid=\"5\" oldattr_firstAttr=\"value1\" removeattr_secondAttr=\"value2\" Addattrname_newAttr=\"new value\" /><Addition><p><q><y Moved=\"true\" Deleted=\"true\" movedpos=\"1\"><!--Any comments?--><z id=\"10\">Just another text</z></y></q></p></Addition><y Moved=\"true\" Deleted=\"true\" movedpos=\"1\"><!--Any comments?--><z id=\"10\">Just another text</z></y></b>");
      Assert.AreNotEqual(null, renameTagValueInResult.RenameTag(resultXDoc).ToString());
    }
    [Test]
    public void when_DeleteAttr_Method_call_()//
    {
      DeleteMovedAndDeletedAttributes deleteMovedAndDeletedAttributes = new DeleteMovedAndDeletedAttributes();
      XDocument expectedResult = XDocument.Parse("<b><yy OldName=\"a\" NewName=\"yy\" TagChanged=\"true\">Some text 1</yy><b>Some text 2</b><c>Some text 3</c><Addition><e>Some text 4</e><f>Some text 5</f></Addition><d><textchanged><del>Another text</del><ins>Changed text</ins></textchanged><fob Deleted=\"true\" /></d><x firstAttr=\"changed attribute value\" oldattr_firstAttr=\"value1\" removeattr_secondAttr=\"value2\" Addattrname_newAttr=\"new value\" /><Addition><p><q><y Moved=\"true\" Deleted=\"true\" movedpos=\"1\"><!--Any comments?--><z id=\"10\">Just another text</z></y></q></p></Addition><y Moved=\"true\" Deleted=\"true\" movedpos=\"1\"><!--Any comments?--><z id=\"10\">Just another text</z></y></b>");
      XDocument resultXDoc = XDocument.Parse("<b cs_crestid=\"1\"><yy OldName=\"a\" NewName=\"yy\" TagChanged=\"true\">Some text 1</yy><b>Some text 2</b><c cs_crestid=\"3\">Some text 3</c><Addition><e>Some text 4</e><f>Some text 5</f></Addition><d cs_crestid=\"4\"><textchanged><del>Another text</del><ins>Changed text</ins></textchanged><fob Deleted=\"true\" /></d><x firstAttr=\"changed attribute value\" cs_crestid=\"5\" oldattr_firstAttr=\"value1\" removeattr_secondAttr=\"value2\" Addattrname_newAttr=\"new value\" /><Addition><p><q><y Moved=\"true\" Deleted=\"true\" movedpos=\"1\"><!--Any comments?--><z id=\"10\">Just another text</z></y></q></p></Addition><y Moved=\"true\" Deleted=\"true\" movedpos=\"1\"><!--Any comments?--><z id=\"10\">Just another text</z></y></b>");
      Assert.AreEqual(expectedResult.ToString(), deleteMovedAndDeletedAttributes.DeleteAttr("cs_crestid", resultXDoc).ToString());
    }
    [Test]
    public void when_DeleteAttr_Method_call_it_should_not_return_null()//
    {
      DeleteMovedAndDeletedAttributes deleteMovedAndDeletedAttributes = new DeleteMovedAndDeletedAttributes();
      XDocument resultXDoc = XDocument.Parse("<b cs_crestid=\"1\"><yy OldName=\"a\" NewName=\"yy\" TagChanged=\"true\">Some text 1</yy><b>Some text 2</b><c cs_crestid=\"3\">Some text 3</c><Addition><e>Some text 4</e><f>Some text 5</f></Addition><d cs_crestid=\"4\"><textchanged><del>Another text</del><ins>Changed text</ins></textchanged><fob Deleted=\"true\" /></d><x firstAttr=\"changed attribute value\" cs_crestid=\"5\" oldattr_firstAttr=\"value1\" removeattr_secondAttr=\"value2\" Addattrname_newAttr=\"new value\" /><Addition><p><q><y Moved=\"true\" Deleted=\"true\" movedpos=\"1\"><!--Any comments?--><z id=\"10\">Just another text</z></y></q></p></Addition><y Moved=\"true\" Deleted=\"true\" movedpos=\"1\"><!--Any comments?--><z id=\"10\">Just another text</z></y></b>");
      Assert.AreNotEqual(null, deleteMovedAndDeletedAttributes.DeleteAttr("cs_crestid", resultXDoc).ToString());
    }
    [Test]
    public void when_DeleteMovedNodeAtr_Method_call_()//
    {
      DeleteMovedAndDeletedAttributes deleteMovedAndDeletedAttributes = new DeleteMovedAndDeletedAttributes();
      XDocument expectedResult = XDocument.Parse("<b><yy OldName=\"a\" NewName=\"yy\" TagChanged=\"true\">Some text 1</yy><b>Some text 2</b><c>Some text 3</c><Addition><e>Some text 4</e><f>Some text 5</f></Addition><d><textchanged><del>Another text</del><ins>Changed text</ins></textchanged><fob Deleted=\"true\" /></d><x firstAttr=\"changed attribute value\" oldattr_firstAttr=\"value1\" removeattr_secondAttr=\"value2\" Addattrname_newAttr=\"new value\" /><Addition><p><q><y Moved=\"true\" movedpos=\"1\"><!--Any comments?--><z id=\"10\">Just another text</z></y></q></p></Addition><y Moved=\"true\" movedpos=\"1\"><!--Any comments?--><z id=\"10\">Just another text</z></y></b>");
      XDocument resultXDoc = XDocument.Parse("<b><yy OldName=\"a\" NewName=\"yy\" TagChanged=\"true\">Some text 1</yy><b>Some text 2</b><c>Some text 3</c><Addition><e>Some text 4</e><f>Some text 5</f></Addition><d><textchanged><del>Another text</del><ins>Changed text</ins></textchanged><fob Deleted=\"true\" /></d><x firstAttr=\"changed attribute value\" oldattr_firstAttr=\"value1\" removeattr_secondAttr=\"value2\" Addattrname_newAttr=\"new value\" /><Addition><p><q><y Moved=\"true\" Deleted=\"true\" movedpos=\"1\"><!--Any comments?--><z id=\"10\">Just another text</z></y></q></p></Addition><y Moved=\"true\" Deleted=\"true\" movedpos=\"1\"><!--Any comments?--><z id=\"10\">Just another text</z></y></b>");
      Assert.AreEqual(expectedResult.ToString(), deleteMovedAndDeletedAttributes.DeleteMovedNodeAtr(resultXDoc).ToString());
    }
    [Test]
    public void when_DeleteMovedNodeAtr_Method_call_it_should_not_return_null()//
    {
      DeleteMovedAndDeletedAttributes deleteMovedAndDeletedAttributes = new DeleteMovedAndDeletedAttributes();
      XDocument resultXDoc = XDocument.Parse("<b><yy OldName=\"a\" NewName=\"yy\" TagChanged=\"true\">Some text 1</yy><b>Some text 2</b><c>Some text 3</c><Addition><e>Some text 4</e><f>Some text 5</f></Addition><d><textchanged><del>Another text</del><ins>Changed text</ins></textchanged><fob Deleted=\"true\" /></d><x firstAttr=\"changed attribute value\" oldattr_firstAttr=\"value1\" removeattr_secondAttr=\"value2\" Addattrname_newAttr=\"new value\" /><Addition><p><q><y Moved=\"true\" Deleted=\"true\" movedpos=\"1\"><!--Any comments?--><z id=\"10\">Just another text</z></y></q></p></Addition><y Moved=\"true\" Deleted=\"true\" movedpos=\"1\"><!--Any comments?--><z id=\"10\">Just another text</z></y></b>");
      Assert.AreNotEqual(null, deleteMovedAndDeletedAttributes.DeleteMovedNodeAtr(resultXDoc).ToString());
    }
  }


}
