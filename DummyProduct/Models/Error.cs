namespace DummyProduct.Models
{
    public class Error
    {
        /// <summary>
        /// HTTP Status Code
        /// </summary>
        public int HttpStatusCode { get; set; } = 500;

        /// <summary>
        /// Error code to be consumed by Developer
        /// </summary>
        public int Code { get; set; } = 1000;

        /// <summary>
        /// Error message to be consumed by User
        /// </summary>
        public string Message { get; set; } = "Something went wrong";

        public static Error Create(int code, string message)
        {
            return new Error
            {
                Code = code,
                HttpStatusCode = 500,
                Message = message
            };
        }

        public static Error Create(int httpStatusCode, int code, string message)
        {
            return new Error
            {
                HttpStatusCode = httpStatusCode,
                Code = code,
                Message = message
            };
        }

        public Error SetCode(string code)
        {
            this.Code = int.TryParse(code, out var codeNumber) ? codeNumber : this.Code;
            return this;
        }

        public Error SetMessage(string message)
        {
            this.Message = !string.IsNullOrWhiteSpace(message) ? message : this.Message;
            return this;
        }

        public Error AddMessage(string message)
        {
            this.Message = $"{this.Message}. " + message;
            return this;
        }
    }
}
