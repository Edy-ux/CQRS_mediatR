namespace CQRS_mediatR.Application.DTOs
{
    public class ErrorResponse
    {
        public string Message { get; set; } = string.Empty;
        public string? Details { get; set; }
        public string? ErrorCode { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public List<string> ValidationErrors { get; set; } = new();
    }

    public class ValidationErrorResponse
    {
        public string Message { get; set; } = "Erro de validação";
        public List<ValidationError> Errors { get; set; } = new();
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }

    public class ValidationError
    {
        public string Field { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}