namespace RoboostTask.DTOs.User
{
    public class LoginResultDTO
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string Username { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
