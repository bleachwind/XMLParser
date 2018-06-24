using System.Xml.Serialization;
using XMLParser.Model.Tuples;

namespace XMLParser.Model.ViewModels {
    [XmlRoot("Root")]
    public class XmlParserResponse{
        [XmlElement("vendor")]
        public string Vender { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("date")]
        public string Date { get; set; }

        [XmlElement("expense")]
        public ExpenseClaim ExpenseClaim { get; set; }
    }
}