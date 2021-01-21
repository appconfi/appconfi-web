
namespace App.SharedKernel.Exceptions
{
    public class CoreHttpException : AppException
    {
        public int StatusCode { get; set; }

        public CoreHttpException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
    public class EntityNotFoundException : CoreHttpException
    {
        public EntityNotFoundException(string message = "The requested resource could not be found but may be available in the future")
            : base(404, message)
        {
        }
    }

    public class BadRequestException : CoreHttpException
    {
        public BadRequestException(string message = "Bad Request")
            : base(400, message)
        {
        }
    }

    public class UnauthorizedException : CoreHttpException
    {
        public UnauthorizedException(string message = "Unauthorized")
            : base(401, message)
        {
        }
    }

    public class ForbiddenException : CoreHttpException
    {
        public ForbiddenException(string message = "Forbidden")
            : base(403, message)
        {
        }
    }
}
