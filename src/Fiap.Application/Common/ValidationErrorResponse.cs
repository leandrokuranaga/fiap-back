namespace Fiap.Application.Common
{
    public class ValidationErrorResponse
    {
        public string Message { get; set; }
        public Dictionary<string, string[]> Errors { get; set; }
    }
}
