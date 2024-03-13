using eSusInsurers.Models.Common;

namespace eSusInsurers.Services.Interfaces
{
    public interface IUpdateNotificationTemplate
    {
        string UpdateNotificationContentParametrs(NotificationContentParameters parameters, string notificationContent, string eventName);
    }
}
