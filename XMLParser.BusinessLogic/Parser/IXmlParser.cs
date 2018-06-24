using XMLParser.Model.ViewModels;

namespace XMLParser.BusinessLogic.Parser {
    public interface IXmlParser {
        XmlParserResponse ParseXmlString(string content);
    }
}