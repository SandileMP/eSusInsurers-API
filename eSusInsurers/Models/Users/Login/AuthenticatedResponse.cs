namespace eSusInsurers.Models.Users.Login
{
    public class AuthenticatedResponse
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public int UserId { get; set; }
    }
}
