using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Interfaces;
using eSusInsurers.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace eSusInsurers.Infrastructure.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly esusinsurer_nonprodContext _context;
        private IInsuranceProviderRepository _insuranceProviderRepository;
        private InsuranceProviderDocumentRepository _insuranceProviderDocumentRepository;
        private IUserRepository _userRepository;
        private IUserTypeRepository _userTypeRepository;
        private IEmailTemplateRepository _emailTemplateRepository;

        public UnitOfWork(esusinsurer_nonprodContext context)
        {
            try
            {
                _context = context;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IInsuranceProviderRepository InsuranceProviderRepository => _insuranceProviderRepository ??= new InsuranceProviderRepository(_context);

        public IInsuranceProviderDocumentRepository InsuranceProviderDocumentRepository => _insuranceProviderDocumentRepository ??= new InsuranceProviderDocumentRepository(_context);

        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context);

        public IUserTypeRepository UserTypeRepository => _userTypeRepository ??= new UserTypeRepository(_context);

        public IEmailTemplateRepository EmailTemplateRepository => _emailTemplateRepository ??= new EmailTemplateRepository(_context);

        public void SaveChanges()
            => _context.SaveChanges();

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
            => await _context.SaveChangesAsync(cancellationToken);


        public IDbContextTransaction BeginTransaction()
        {
            //It will Begin the transaction on the underlying store connection
            return _context.Database.BeginTransaction();
        }

        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            //It will Begin the transaction on the underlying store connection
            return _context.Database.BeginTransactionAsync(cancellationToken);
        }

        public void Commit()
        {
            _context.Database.CommitTransaction();
        }

        public Task CommitAsync(CancellationToken cancellationToken = default)
        {
            return _context.Database.CommitTransactionAsync(cancellationToken);
        }

        public void Rollback()
        {
            _context.Database.RollbackTransaction();
        }

        public Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            return _context.Database.RollbackTransactionAsync(cancellationToken);
        }
    }
}
