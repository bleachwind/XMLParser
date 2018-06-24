using System.Xml.Serialization;

namespace XMLParser.Model.Constants {
    public enum CostCentre {
        [XmlEnum("")]
        Unknown = 0,
        [XmlEnum("DEV002")]
        DEV002 = 1
    }
}