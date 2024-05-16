namespace DummyProduct.Models
{
    public class ServiceStatus
    {
        public bool IsSuccess { get; set; }
        public string SuccessMessage { get; set; }
        public Error Error { get; set; }

        public static ServiceStatus ErrorResult(Error error = null)
        {
            return new ServiceStatus
            {
                IsSuccess = false,
                Error = error ?? new Error()
            };
        }

        public static ServiceStatus<TResult> ErrorResult<TResult>(Error error = null, TResult result = null) where TResult : class
        {
            return new ServiceStatus<TResult>
            {
                IsSuccess = false,
                Error = error ?? new Error(),
                Result = result ?? default(TResult)
            };
        }

        public static ServiceStatus SuccessResult(string message = null)
        {
            return new ServiceStatus
            {
                IsSuccess = true,
                SuccessMessage = message
            };
        }

        public static ServiceStatus<TResult> SuccessObjectResult<TResult>(TResult result)
        {
            return new ServiceStatus<TResult>
            {
                IsSuccess = true,
                Result = result
            };
        }
    }

    public class ServiceStatus<TResult> : ServiceStatus
    {
        public TResult Result { get; set; }
    }
}
