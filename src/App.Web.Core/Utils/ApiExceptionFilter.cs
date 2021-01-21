//using App.Service.Accounts;
//using App.SharedKernel.Exceptions;
//using App.SharedKernel.Guards;
//using System.Net;
//using System.Web.Http.ExceptionHandling;
//using System.Web.Http.Filters;
//using System.Web.Http.Results;

//namespace App.Web.Core.Utils
//{
//    public sealed class ApiExceptionFilter : ExceptionFilterAttribute
//    {
//        public class ApiError
//        {
//            public string Message { get; set; }
//        }

//        public override void OnException(ExceptionContext context)
//        {
//            var ex = context.Exception;

//            if (ex is AppException)
//            {
//                HandleError(ex as AppException, context);
//            }
//            base.OnException(context);
//        }

//        static void HandleError(AppException ex, ExceptionContext context)
//        {
//            var error = "";

//            if (ex is GuardValidationException)
//            {
//                error = (ex as GuardValidationException).Message;
//            }
//            if(ex is InvalidSubscriptionException)
//            {
//                error = (ex as InvalidSubscriptionException).Message;
//            }
//            if(ex is CoreHttpException)
//            {
//                error = (ex as CoreHttpException).Message;
//            }
//            else
//            {
//                context.Result = new RedirectResult($"/home/error?msg={error}");
//                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Redirect;
//            }
//        }
//    }
//}
