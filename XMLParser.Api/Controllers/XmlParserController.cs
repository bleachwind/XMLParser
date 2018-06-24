using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml;
using XMLParser.Api.Attributes;
using XMLParser.BusinessLogic.Parser;
using XMLParser.Model.ViewModels;

namespace XMLParser.Api.Controllers {
    [RoutePrefix("api/xmlparser")]
    public class XmlParserController : ApiController {
        private static IXmlParser XmlParser =>
            (IXmlParser)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IXmlParser));

        [AcceptVerbs("POST")]
        [Route("process")]
        [ValidRawXmlInRequest]
        public HttpResponseMessage ProcessRequest(XmlParserRequest request) {
            try {
                var response = XmlParser.ParseXmlString(request.Content);
                return Request.CreateResponse(HttpStatusCode.OK, response);

            } catch (Exception e) {
                if (e is XmlException || e is InvalidOperationException || e is ValidationException) {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        $"{e.Message}, given content in a bad format and cannot parse to xml.");
                }
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}
