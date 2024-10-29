using CRMSystem.Application.Interfaces;
using CRMSystem.Domain.Entities;
using CRMSystem.Persistence.DataAccess;

namespace CRMSystem.Persistence.Repositories;

public class UserRepository : Repository<User> , IUserRepository
{
    public UserRepository(CRMDB context) : base(context)
    {
    }
}