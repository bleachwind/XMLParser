using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XMLParser.BusinessLogic.Parser;
using XMLParser.BusinessLogic.Parser.Implementation;
using XMLParser.Model.ViewModels;

namespace XMLParser.BusinessLogicTests.Parser.Implementation {
    [TestClass]
    public class XmlParserTests {
        protected IXmlParser XmlParser { get; set; }
        [TestInitialize]
        public void Initialize() {
            XmlParser = new XmlParser();
        }

        [TestMethod]
        [ExpectedException(typeof(XmlException))]
        public void ParseBadXmlStringTest() {
            var contents = new[] { "", "<abc>", "abc", "<abc></abc>", "<abc>abc</abc>",
                "Please create a reservation at the <vendor>Viaduct Steakhouse</vendor> " +
                "our <description>development team’s project end celebration dinner</description> " +
                "on <date>Tuesday 27 April 2017</date>. We expect to arrive around 7.15pm. Approximately " +
                "12 people but I’ll confirm exact numbers closer to the day. Please create an expense claim " +
                "for the below. Relevant details are marked up as requested… <expense><cost_centre>DEV002</cost_centre> " +
                "<total><payment_method>personal card</payment_method></expense>",
                "Please create a reservation at the <vendor>Viaduct Steakhouse</vendor " +
                "our <description>development team’s project end celebration dinner</description> " +
                "on <date>Tuesday 27 April 2017</date>. We expect to arrive around 7.15pm. Approximately " +
                "12 people but I’ll confirm exact numbers closer to the day. Please create an expense claim " +
                "for the below. Relevant details are marked up as requested… <expense><cost_centre>DEV002</cost_centre> " +
                "<total><payment_method>personal card</payment_method></expense>"
            };
            var results = new List<XmlParserResponse>();
            foreach (var content in contents) {
                results.Add(XmlParser.ParseXmlString(content));
            }
            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ParseBadNumberFormatXmlStringTest() {
            var content =
                "Please create a reservation at the <vendor>Viaduct Steakhouse</vendor> " +
                "our <description>development team’s project end celebration dinner</description> " +
                "on <date>Tuesday 27 April 2017</date>. We expect to arrive around 7.15pm. Approximately " +
                "12 people but I’ll confirm exact numbers closer to the day. Please create an expense claim " +
                "for the below. Relevant details are marked up as requested… <expense><cost_centre>DEV002</cost_centre> " +
                "<total>a</total><payment_method>personal card</payment_method></expense>";
            var result = XmlParser.ParseXmlString(content);
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ParseMissingNodeXmlStringTest() {
            var content =
                "Please create a reservation at the <vendor>Viaduct Steakhouse</vendor> " +
                "our <description>development team’s project end celebration dinner</description> " +
                "on <date>Tuesday 27 April 2017</date>. We expect to arrive around 7.15pm. Approximately " +
                "12 people but I’ll confirm exact numbers closer to the day. Please create an expense claim " +
                "for the below. Relevant details are marked up as requested… <expense><cost_centre>DEV002</cost_centre> " +
                "<payment_method>personal card</payment_method></expense>";
            var result = XmlParser.ParseXmlString(content);
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void ParseXmlStringTest() {
            var content = 
                "Please create a reservation at the <vendor>Viaduct Steakhouse</vendor> " +
                "our <description>development team’s project end celebration dinner</description> " +
                "on <date>Tuesday 27 April 2017</date>. We expect to arrive around 7.15pm. Approximately " +
                "12 people but I’ll confirm exact numbers closer to the day. Please create an expense claim " +
                "for the below. Relevant details are marked up as requested… <expense><cost_centre>DEV002</cost_centre> " +
                " <total>890.55</total><payment_method>personal card</payment_method></expense>";
            var result = XmlParser.ParseXmlString(content);
            Assert.AreEqual(890.55m, result.ExpenseClaim.Total);
        }
    }
}