using MediatR;
using RoboostTask.DTOs.User;
using RoboostTask.Enums;

namespace RoboostTask.Features.Users.Commands
{
    public class RegisterCommand : IRequest<RegisterResultDTO>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public RolesEnum.UserRole Role { get; set; }
    }
}
