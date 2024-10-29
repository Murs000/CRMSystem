using CRMSystem.Application.Interfaces;
using MediatR;

namespace CRMSystem.Application.Features.Commands.User.Update
{
    public class RegisterUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserManager _userManager;

        public RegisterUserCommandHandler(IUserRepository userRepository,IUserManager userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userId = _userManager.GetCurrentUserId();
            var user = await _userRepository.GetAsync(u => u.Id == userId);

            user.Name = request.Name;
            user.Surname = request.Surname;

            _userRepository.Update(user);

            await _userRepository.SaveAsync();

            return true;
        }
    }
}