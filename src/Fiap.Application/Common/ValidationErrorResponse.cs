using System.Diagnostics.CodeAnalysis;

namespace Fiap.Application.Common
{
    [ExcludeFromCodeCoverage]
    public class ValidationErrorResponse
    {
        public string Message { get; set; }
        public Dictionary<string, string[]> Errors { get; set; }
    }
}
