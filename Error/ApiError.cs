using Newtonsoft.Json;
using System.Text.Json;

namespace webApi_build_Real.Error
{
    public class ApiError
    {
        public ApiError(int erroCode,string erroMessage, string errorDetails = null) 
        {
            ErrorCode=erroCode;
            ErrorMessage=erroMessage;
            ErrorDetails=errorDetails;
        }
        public int ErrorCode { get; set; } 
        public string ErrorMessage { get; set; }
        public string ErrorDetails { get; set; }
        public override string ToString()
        {
            return System.Text.Json.JsonSerializer.Serialize(this);      
        }
    }
}
