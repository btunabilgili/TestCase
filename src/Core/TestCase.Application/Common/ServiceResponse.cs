namespace TestCase.Application.Common
{
    public class ServiceResponse<T>
    {
        public bool IsSuccess
        {
            get
            {
                return Error == null;
            }
        }
        public string? Error { get; set; }
        public string? StackTrace { get; set; }
        public T? Value { get; set; }
    }

    public static class ServiceResponseExtensions
    {
        public static ServiceResponse<T> ToServiceResponse<T>(this T value)
        {
            return new ServiceResponse<T> { Value = value };
        }

        public static ServiceResponse<T> ToServiceResponse<T>(this Exception exception)
        {
            return new ServiceResponse<T>
            {
                Value = default,
                Error = exception.Message,
                StackTrace = exception.StackTrace
            };
        }
    }
}
