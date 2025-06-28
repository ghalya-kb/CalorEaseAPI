namespace Entities.DTOs
{
    public class ResetPasswordDto
    {
        public string Email { get; set; }
        public string Token { get; set; } // Encoded
        public string NewPassword { get; set; }
    }
}
