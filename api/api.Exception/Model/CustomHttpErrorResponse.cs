namespace api.Error.Model
{
    public class CustomHttpErrorResponse
    {
        public string? Message { get; set; }

        public string? Source { get; set; }
        
        public string? Exception { get; set; }
        
        public string? ErrorId { get; set; }
        
        public string? SupportMessage { get; set; }
        
        public int Status { get; set; }
    }
}
