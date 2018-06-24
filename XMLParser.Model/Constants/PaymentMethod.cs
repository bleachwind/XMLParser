using System.Xml.Serialization;

namespace XMLParser.Model.Constants {
    public enum PaymentMethod {
        [XmlEnum("")]
        Unknown = 0,
        [XmlEnum("personal card")]
        PersonalCard = 1
    }
}