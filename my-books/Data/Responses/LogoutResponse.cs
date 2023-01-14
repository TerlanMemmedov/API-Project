using System.Text.Json.Serialization;

namespace my_books.Data.Responses
{
    public class LogoutResponse
    {
        [JsonIgnore]
        public bool Success { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string ErrorCode { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Error { get; set; }


    }
}
