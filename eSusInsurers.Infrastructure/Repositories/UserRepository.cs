using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eSusInsurers.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly DbSet<User> _users;

        public UserRepository(DbContext context) : base(context)
        {
            _users = context.Set<User>();
        }

        public async Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken)
        {
            return await _users.Include(x => x.UserType).FirstOrDefaultAsync(x => x.UserName == userName, cancellationToken);
        }
    }
}
