using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using NUnit.Framework;
using Rhino.Mocks;

namespace Testing.Common
{
    public static class MvcAssert
    {
        private static IDictionary<HttpVerbs, Type> HttpVerbToAttributeDictionary { get; set; }

        static MvcAssert()
        {
            HttpVerbToAttributeDictionary = new Dictionary<HttpVerbs, Type>
			{
				{ HttpVerbs.Delete, typeof(HttpDeleteAttribute) },
				{ HttpVerbs.Get, typeof(HttpGetAttribute) },
				{ HttpVerbs.Post, typeof(HttpPostAttribute) },
				{ HttpVerbs.Put, typeof(HttpPutAttribute) }
			};
        }

        public static MethodInfo ActionExists(Controller controller, string actionName, params Type[] parameterTypes)
        {
            if (controller == null)
                throw new ArgumentNullException("controller");

            if (string.IsNullOrEmpty(actionName))
                throw new ArgumentNullException("actionName");


            MethodInfo action = controller.GetType().GetMethod(actionName, parameterTypes);
            Assert.IsNotNull(action, string.Format(CultureInfo.InvariantCulture, "The specified action '{0}' could not be found.", actionName));


            return action;
        }

        public static void IsAnEmptyView(ActionResult result)
        {
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsNullOrEmpty(((ViewResult)result).ViewName);
        }

        public static RedirectToRouteResult IsARedirectToRoute(this ActionResult result)
        {
            return result.IsInstanceOf<RedirectToRouteResult>();
        }

        public static RedirectResult IsARedirectResult(this ActionResult result)
        {
            return result.IsInstanceOf<RedirectResult>();
        }

        public static TResult IsInstanceOf<TResult>(this object result) where TResult : class
        {
            var castResult = result as TResult;

            Assert.IsNotNull(castResult, string.Format(CultureInfo.InvariantCulture, "result is not an instance of {0}", typeof(TResult).ToString()));

            return castResult;
        }

        public static void AssertIsRedirectTo(this ActionResult result, string expectedControllerName, string expectedActionName)
        {
            result.IsInstanceOf<RedirectToRouteResult>()
                .AssertHasRouteValue("controller", expectedControllerName)
                .AssertHasRouteValue("action", expectedActionName);
        }

        public static void AssertIsRedirectToResult(ActionResult result, string expectedResult)
        {
            Assert.That(((RedirectResult)result).Url, Is.EqualTo(expectedResult));
        }

        public static void AssertDoesNotHaveRouteValue(this ActionResult result, string keyName)
        {
            Assert.IsFalse(((RedirectToRouteResult)result).RouteValues.ContainsKey(keyName));
        }

        public static RedirectToRouteResult AssertHasRouteValue(this RedirectToRouteResult result, string keyName, object expectedKeyValue)
        {
            Assert.IsInstanceOf<RedirectToRouteResult>(result);
            Assert.AreEqual(expectedKeyValue, ((RedirectToRouteResult)result).RouteValues[keyName]);

            return result;
        }

        public static RedirectToRouteResult AssertIsRedirectTo<TControllerType>(this ActionResult result, Expression<Func<TControllerType, ActionResult>> action)
        {
            return AssertIsRedirectTo<TControllerType>(result, action, false);
        }

        private static MethodInfo GetMethodFromExpression(LambdaExpression expression)
        {
            return ((MethodCallExpression)expression.Body).Method;
        }

        public static MethodInfo GetMethodFromAction<T>(this T source, Expression<Action<T>> action)
        {
            return GetMethodFromExpression(action);
        }

        /// <summary>
        /// Asserts the is redirect to.
        /// </summary>
        /// <typeparam name="TControllerType">The type of the controller type.</typeparam>
        /// <param name="result">The result.</param>
        /// <param name="action">The action.</param>
        /// <param name="controllerTypeShouldBeNull">if set to <c>true</c> [controller type should be null].</param>
        /// <returns></returns>        
        public static RedirectToRouteResult AssertIsRedirectTo<TControllerType>(this ActionResult result, Expression<Func<TControllerType, ActionResult>> action, bool controllerTypeShouldBeNull)
        {
            //Probably an idea to assert that HttpPost attribute is NOT on the method too

            var redirectToRouteResult = result.IsARedirectToRoute();

            Assert.IsNotNull(redirectToRouteResult, "ActionResult is not a RedirectToRouteResult");

            var controllerName = Regex.Match(typeof(TControllerType).Name, "^(?<ControllerName>.+)Controller(`.*)?$")
                .Groups["ControllerName"].Value;

            var method = ((MethodCallExpression)action.Body).Method;

            var actionNameAttribute = (ActionNameAttribute)(from attribute in method.GetCustomAttributes(false)
                                                            where attribute is ActionNameAttribute
                                                            select attribute).FirstOrDefault();

            var actionName = actionNameAttribute != null ? actionNameAttribute.Name : method.Name;

            if (controllerTypeShouldBeNull)
            {
                redirectToRouteResult.AssertHasRouteValue("controller", null);
            }
            else
            {
                redirectToRouteResult.AssertHasRouteValue("controller", controllerName);
            }
            redirectToRouteResult.AssertHasRouteValue("action", actionName);

            return redirectToRouteResult;
        }

        public static RedirectResult AssertIsARedirectResult(this ActionResult source)
        {
            Assert.IsInstanceOf<RedirectResult>(source);
            return (RedirectResult)source;
        }

        public static RedirectResult To(this RedirectResult source, string expectedResult)
        {
            Assert.AreEqual(source.Url, expectedResult);

            return source;
        }

        public static ContentResult AssertIsContentResult(this ActionResult result, string expectedContent)
        {
            var contentResult = result as ContentResult;

            Assert.IsNotNull(contentResult, "Expected result to be an instance of ContentResult");
            Assert.AreEqual(expectedContent, contentResult.Content);

            return contentResult;
        }

        public static FileContentResult AssertIsFileContentResult(this ActionResult result, byte[] expectedContent, string expectedContentType)
        {
            var fileContentResult = result as FileContentResult;

            Assert.IsNotNull(fileContentResult, "Expected result to be an instance of ContentResult");
            Assert.AreEqual(expectedContent, fileContentResult.FileContents);
            Assert.AreEqual(expectedContentType, fileContentResult.ContentType);

            return fileContentResult;
        }

        public static FilePathResult AssertIsFilePathResult(this ActionResult result, string expectedPath)
        {
            var filePathResult = result as FilePathResult;

            Assert.IsNotNull(filePathResult, "Expected result to be an instance of FilePathResult");
            Assert.AreEqual(expectedPath, filePathResult.FileName);

            return filePathResult;
        }

        public static ContentResult WithContentType(this ContentResult result, string contentType)
        {
            Assert.AreEqual(contentType, result.ContentType);

            return result;
        }

        public static ViewResultBase AssertIsView(this ActionResult result, string viewName)
        {
            Assert.IsInstanceOf<ViewResultBase>(result);
            var viewResultBase = (ViewResultBase)result;
            Assert.AreEqual(viewName, viewResultBase.ViewName);
            return viewResultBase;
        }

        public static ViewResultBase AssertIsDefaultView(this ActionResult result)
        {
            return result.AssertIsView(string.Empty);
        }

        public static ViewResultBase ThatIsAPartialView(this ViewResultBase result)
        {
            Assert.IsInstanceOf<PartialViewResult>(result);
            return result;
        }

        public static ViewResultBase ThatIsAFullView(this ViewResultBase result)
        {
            Assert.IsInstanceOf<ViewResult>(result);
            return result;
        }

        public static ViewResultBase WithViewModel(this ViewResultBase result, object viewModel)
        {
            Assert.IsNotNull(viewModel);
            Assert.AreEqual(viewModel, result.ViewData.Model);
            return result;
        }

        public static ViewResultBase WithViewModel(this ViewResultBase result, Predicate<object> matcher)
        {
            Assert.IsTrue(matcher(result.ViewData.Model));
            return result;
        }

        public static ViewResultBase WithNoViewModel(this ViewResultBase result)
        {
            Assert.IsNull(result.ViewData.Model);
            return result;
        }

        public static HttpContextBase BuildHttpContextStub(bool isAjaxRequest)
        {
            var httpRequestBase = MockRepository.GenerateStub<HttpRequestBase>();
            if (isAjaxRequest)
            {
                httpRequestBase.Stub(r => r["X-Requested-With"]).Return("XMLHttpRequest");
            }

            var httpContextBase = MockRepository.GenerateStub<HttpContextBase>();
            httpContextBase.Stub(c => c.Request).Return(httpRequestBase);

            return httpContextBase;
        }

        public static TAttribute GetMethodAttribute<TController, TAttribute>(Expression<Func<TController, ActionResult>> action)
            where TAttribute : Attribute
            where TController : IController
        {
            var method = ((MethodCallExpression)action.Body).Method;

            return (TAttribute)GetMethodAttribute(action, typeof(TAttribute));
        }

        public static Attribute GetMethodAttribute<TController>(Expression<Func<TController, ActionResult>> action, Type attributeType)
            where TController : IController
        {
            var method = ((MethodCallExpression)action.Body).Method;

            return (Attribute)(from attribute in method.GetCustomAttributes(false)
                               where attributeType.IsAssignableFrom(attribute.GetType())
                               select attribute).FirstOrDefault();
        }

        public static void AssertMethodHasAttribute<TController>(this TController controller, Expression<Func<TController, ActionResult>> action, Type attributeType)
            where TController : IController
        {
            Assert.IsNotNull(GetMethodAttribute(action, attributeType));
        }

        public static void AssertMethodHasHttpVerb<TController>(this Expression<Func<TController, ActionResult>> action, HttpVerbs httpVerb)
            where TController : IController
        {
            foreach (var key in HttpVerbToAttributeDictionary.Keys)
            {
                if (key == httpVerb)
                {
                    Assert.IsNotNull(GetMethodAttribute(action, HttpVerbToAttributeDictionary[key]),
                        string.Format(CultureInfo.InvariantCulture, "Expected {0} to be applied to the action {1}", key, action));
                }
                else
                {
                    Assert.IsNull(GetMethodAttribute(action, HttpVerbToAttributeDictionary[key]),
                        string.Format(CultureInfo.InvariantCulture, "Expected {0} NOT to be applied to the action {1}", key, action));
                }
            }
        }

        public static void AssertMethodAcceptsHttpGet<TController>(this TController controller, Expression<Func<TController, ActionResult>> action) where TController : IController
        {
            Assert.NotNull(GetMethodAttribute<TController, HttpGetAttribute>(action));
            Assert.IsNull(GetMethodAttribute<TController, HttpPostAttribute>(action));
            Assert.IsNull(GetMethodAttribute<TController, HttpPutAttribute>(action));
            Assert.IsNull(GetMethodAttribute<TController, HttpDeleteAttribute>(action));
        }

        public static void AssertMethodAcceptsHttpPost<TController>(this TController controller, Expression<Func<TController, ActionResult>> action) where TController : IController
        {
            Assert.NotNull(GetMethodAttribute<TController, HttpPostAttribute>(action));
            Assert.IsNull(GetMethodAttribute<TController, HttpGetAttribute>(action));
            Assert.IsNull(GetMethodAttribute<TController, HttpPutAttribute>(action));
            Assert.IsNull(GetMethodAttribute<TController, HttpDeleteAttribute>(action));
        }

        public static Attribute GetAttribute(this ICustomAttributeProvider attributeProvider, Type attributeType)
        {
            //Not too keen on forcing the inherited bit (though it's usually ok)
            //this is done since the testing stuff insists on creating a mock of the class we're testing (violating our expectations)
            return (Attribute)(from attribute in attributeProvider.GetCustomAttributes(true)
                               where attributeType.IsAssignableFrom(attribute.GetType())
                               select attribute).FirstOrDefault();
        }

        public static TAttribute AssertIsDecoratedWithAttribute<TAttribute>(this ICustomAttributeProvider attributeProvider)
            where TAttribute : Attribute
        {
            var attribute = attributeProvider.GetAttribute(typeof(TAttribute));

            Assert.IsNotNull(attribute,
                string.Format(CultureInfo.InvariantCulture, "Expected attribute provider {0} to have been decorated with {1}",
                    attributeProvider,
                    typeof(TAttribute)));

            return (TAttribute)attribute;
        }

        public static T With<T>(this T objectToAssertAgainst, Func<T, bool> predicate, string testFailureMessage)
        {
            Assert.IsTrue(predicate(objectToAssertAgainst), testFailureMessage);

            return objectToAssertAgainst;
        }

        public static void AssertHasAttributeTargets(this ICustomAttributeProvider attributeProvider, AttributeTargets attributeTargets)
        {
            attributeProvider.AssertIsDecoratedWithAttribute<AttributeUsageAttribute>()
                .With(a => a.ValidOn == attributeTargets,
                    string.Format(CultureInfo.InvariantCulture, "Expected attribute targets of {0}",
                        attributeTargets));
        }

        public static void AssertMethodBodyIsEmpty(MethodBody methodBody)
        {
            var body = methodBody.GetILAsByteArray();
            var bodyBuilder = new StringBuilder();

            foreach (var value in body)
            {
                bodyBuilder.AppendFormat("{0} ", value);
            }
            //Assert that the method only has nops and ends in a ret
            Assert.IsTrue(
                body.LastOrDefault() == 42 && body.Take(body.Count() - 1).All(item => item == 0),
                string.Format(CultureInfo.InvariantCulture, "Method body is not empty ({0})", bodyBuilder));
        }

        public static TExpectedType AssertIs<TExpectedType>(this object source) where TExpectedType : class
        {
            var result = source as TExpectedType;

            Assert.IsNotNull(
                result,
                string.Format(CultureInfo.InvariantCulture, "source is not of the expectedResult type {0}", typeof(TExpectedType)));

            return result;
        }


        public static void AssertMethodIsEmpty<T>(this T source, Expression<Action<T>> action)
        {
            AssertMethodBodyIsEmpty(GetMethodFromExpression(action).GetMethodBody());
        }
    }

}
