using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using XMLParser.BusinessLogic.Validation;
using XMLParser.Model.Tuples;

namespace XMLParser.Api.Attributes {
    [AttributeUsage(AttributeTargets.Method)]
    public class ValidRawXmlInRequestAttribute : ActionFilterAttribute {
        private static IRawXmlValidator RawXmlValidator => (IRawXmlValidator)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IRawXmlValidator));
        private const string RequestParameterName = "request";
        public override void OnActionExecuting(HttpActionContext actionContext) {
            var isArgumentExist = actionContext.ActionArguments.TryGetValue(RequestParameterName, out var arg);
            if (isArgumentExist) {
                var request = arg as XmlParserRequestBase;
                if (!RawXmlValidator.DoesGivenContentContainXmlString(request)) {
                    actionContext.Response = actionContext.Request.CreateErrorResponse(
                        HttpStatusCode.BadRequest, "request does not contain any xml node or in a bad format.");
                }
            } else {
                actionContext.Response = actionContext.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, "argument is not found.");
            }
        }
    }
}