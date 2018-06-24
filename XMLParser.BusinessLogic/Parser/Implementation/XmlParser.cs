using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using XMLParser.Model.Constants;
using XMLParser.Model.ViewModels;

namespace XMLParser.BusinessLogic.Parser.Implementation {
    public class XmlParser : IXmlParser{
        public XmlParserResponse ParseXmlString(string content) {
            var sanitizedContent = content.Replace(Environment.NewLine, " ");
            var regex = new Regex(RegexPatterns.XmlNodePattern);
            var matches = regex.Matches(sanitizedContent);
            if (matches.Count == 0) throw new XmlException("unable to deserialize given xml to desired object.");
            XElement root;
            if (matches.Count > 1) {
                //no root, need manually add root element
                var xmlElements = new List<XElement>();
                foreach (Match match in matches) {
                    xmlElements.Add(XElement.Parse(match.ToString(), LoadOptions.PreserveWhitespace));
                }
                root = new XElement("Root");
                root.Add(xmlElements);
            } else {
                root = XElement.Parse(matches[0].ToString(), LoadOptions.PreserveWhitespace);
            }
            var response = DeserializeXmlToObject(root);
            if (response == null) {
                throw new XmlException("unable to deserialize given xml to desired object.");
            }
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(response.ExpenseClaim, new ValidationContext(response.ExpenseClaim), results, true)) {
                throw new ValidationException(string.Join(",", results.Select(x => x.ErrorMessage)));
            }
            return response;
        }

        private static XmlParserResponse DeserializeXmlToObject(XNode xmlElement) {
            var stringBuilder = new StringBuilder();
            var xmlWriterSettings = new XmlWriterSettings {
                OmitXmlDeclaration = true,
                Indent = true,
                ConformanceLevel = ConformanceLevel.Auto
            };
            using (var xmlWriter = XmlWriter.Create(stringBuilder, xmlWriterSettings)) {
                xmlElement.WriteTo(xmlWriter);
            }
            using (var stringReader = new StringReader(stringBuilder.ToString())) {
                var serializer = new XmlSerializer(typeof(XmlParserResponse));
                return (XmlParserResponse)serializer.Deserialize(stringReader);
            }
        }
    }
}