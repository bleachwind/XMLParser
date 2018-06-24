using XMLParser.Model.Tuples;

namespace XMLParser.BusinessLogic.Validation {
    public interface IRawXmlValidator {
        /// <summary>
        /// use regex to find valid xml nodes
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        bool DoesGivenContentContainXmlString(XmlParserRequestBase request);
    }
}
