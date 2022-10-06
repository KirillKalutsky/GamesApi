namespace GamesApi.Models
{
    public class ApiResponse
    {
        public object Data { get; set; }
        public string ErrorMessage { get; set; }
        public int StatusCode { get; set; }
    }
}
