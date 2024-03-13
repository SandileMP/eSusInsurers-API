using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eSusInsurers.Infrastructure.Repositories
{
    public class EmailTemplateRepository : Repository<EmailTemplate>, IEmailTemplateRepository
    {
        private readonly DbSet<EmailTemplate> _emailTemplates;

        public EmailTemplateRepository(DbContext context) : base(context)
        {
            _emailTemplates = context.Set<EmailTemplate>();
        }

        public async Task<EmailTemplate?> GetByEventNameAsync(string eventName, CancellationToken cancellationToken)
        {
            return await _emailTemplates.Include(x => x.Event).FirstOrDefaultAsync(x => x.Event.EventName == eventName, cancellationToken);
        }
    }
}
