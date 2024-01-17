using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eSusInsurers.Infrastructure.Repositories
{
    public class InsuranceProviderDocumentRepository : Repository<InsuranceProviderDocument>, IInsuranceProviderDocumentRepository
    {
        public InsuranceProviderDocumentRepository(DbContext context) : base(context)
        {
        }
    }
}
