using System;
using System.Text.RegularExpressions;
using XMLParser.Model.Constants;
using XMLParser.Model.Tuples;

namespace XMLParser.BusinessLogic.Validation.Implementation {
    public class RawXmlValidator : IRawXmlValidator {
        public bool DoesGivenContentContainXmlString(XmlParserRequestBase request) {
            var testContent = request.Content.Replace(Environment.NewLine, " ");
            var regexCloseTag = new Regex(RegexPatterns.XmlCloseTagPattern).Matches(testContent);
            var regexOpenTag= new Regex(RegexPatterns.XmlOpenTagPattern).Matches(testContent);
            var regex = new Regex(RegexPatterns.XmlNodePattern).Matches(testContent);

            return regexCloseTag.Count > 0 && regexOpenTag.Count > 0 && regexCloseTag.Count == regexOpenTag.Count &&
                   regex.Count > 0;
        }
    }
}