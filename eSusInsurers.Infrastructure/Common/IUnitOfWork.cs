using eSusInsurers.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace eSusInsurers.Infrastructure.Common
{
    public interface IUnitOfWork
    {
        IInsuranceProviderRepository InsuranceProviderRepository { get; }
        IInsuranceProviderDocumentRepository InsuranceProviderDocumentRepository { get; }
        IUserRepository UserRepository { get; }
        IUserTypeRepository UserTypeRepository { get; }
        IEmailTemplateRepository EmailTemplateRepository { get; }

        void SaveChanges();
        Task SaveChangesAsync(CancellationToken cancellationToken);
        IDbContextTransaction BeginTransaction();
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
        void Commit();
        void Rollback();
        Task CommitAsync(CancellationToken cancellationToken);
        Task RollbackAsync(CancellationToken cancellationToken);
    }
}
