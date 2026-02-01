namespace PredictiveMaintenance.Application.DTOs.Common
{
    public class ErrorResponseDto
    {
        public string ErrorCode { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string? Details { get; set; } // opcional, para más información técnica
    }
}