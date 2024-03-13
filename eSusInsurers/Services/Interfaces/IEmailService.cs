using eSusInsurers.Models.Common;

namespace eSusInsurers.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(NotificationContentParameters parameters
                                                    , string eventName
                                                    , string[] ToAddress
                                                    , IFormFileCollection? attachments = null
                                                    , CancellationToken cancellationToken = default);
    }
}
