using CRMSystem.Application.Interfaces;
using CRMSystem.Domain.Entities;
using CRMSystem.External.Helpers;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace CRMSystem.Application.Features.Commands.Auth.Register
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailManager _emailService;

        public RegisterUserCommandHandler(IUserRepository userRepository, IEmailManager emailService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
        }

        public async Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = new Domain.Entities.User();

            // Hash password
            (string hash, string salt) = PasswordHelper.HashPassword(request.Password);
            user.SetPassword(hash, salt);

            // Generate OTP
            var otp = OTPHelper.GenerateOTP();
            user.OTP = otp;
            var otpExpireDate = DateTime.UtcNow.AddHours(4).AddMinutes(10);

            // Set Details
            user.SetDetails(request.Username, request.Email, request.Name, request.Surname)
                .SetPassword(hash, salt)
                .SetOtp(otp, otpExpireDate)
                .SetCreationCredentials(null);

            // Save user and customer data
            await _userRepository.AddAsync(user);
            await _userRepository.SaveAsync();

            // Send OTP email
            await _emailService.SendEmailAsync(user.Email, "OTP message", otp);

            return true;
        }
    }
}