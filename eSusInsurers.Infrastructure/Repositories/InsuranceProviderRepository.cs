using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eSusInsurers.Infrastructure.Repositories
{
    public class InsuranceProviderRepository : Repository<InsuranceProvider>, IInsuranceProviderRepository
    {
        public InsuranceProviderRepository(DbContext context) : base(context)
        {
        }
    }
}
