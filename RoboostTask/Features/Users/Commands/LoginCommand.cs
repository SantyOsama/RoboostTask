using MediatR;
using RoboostTask.DTOs.User;

namespace RoboostTask.Features.Users.Commands
{
    public class LoginCommand:IRequest<LoginResultDTO>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
