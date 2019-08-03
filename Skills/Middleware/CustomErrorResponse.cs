namespace AccountDataExtractService.Middleware
{
    public class CustomErrorResponse
    {
        public int HttpStatusCode { get; set; }
        public string Message { get; set; }
    }
}