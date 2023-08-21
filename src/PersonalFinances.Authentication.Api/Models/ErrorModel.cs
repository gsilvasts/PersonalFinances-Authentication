using System.Diagnostics.CodeAnalysis;

namespace PersonalFinances.Authentication.Api.Models
{
    [ExcludeFromCodeCoverage]
    public class ErrorModel
    {
        public class ErrorDetails
        {
            public int StatusCode { get; set; }
            public string? Message { get; set; }
        }

        public List<ErrorDetails> Errors { get; set; } = new List<ErrorDetails>();

        public ErrorModel(ErrorDetails error)
        {
            Errors.Add(error);
        }

        public ErrorModel(IEnumerable<ErrorDetails> errors)
        {
            Errors.AddRange(errors);
        }
    }
}
