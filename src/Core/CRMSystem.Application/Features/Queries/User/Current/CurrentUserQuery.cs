using CRMSystem.Application.Features.Queries.User.ViewModels;
using MediatR;

namespace CRMSystem.Application.Features.Queries.User.Current;

public class CurrentUserQuery : IRequest<UserDto>
{
}