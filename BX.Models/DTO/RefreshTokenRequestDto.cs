namespace BX.Models.Dto
{
    public class RefreshTokenRequestDto
    {
        public string UserId { get; set; }
        public required string RefreshToken { get; set; }
    }
}
