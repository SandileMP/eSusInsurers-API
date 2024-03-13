using eSusInsurers.Models.Common;
using eSusInsurers.Services.Interfaces;

namespace eSusInsurers.Services.Implementations
{
    public class UpdateNotificationTemplate : IUpdateNotificationTemplate
    {
        public string UpdateNotificationContentParametrs(NotificationContentParameters parameters, string notificationContent, string eventName)
        {
            try
            {
                notificationContent = notificationContent.Replace("[index0]", parameters.Index0);
                notificationContent = notificationContent.Replace("[index1]", parameters.Index1);
                notificationContent = notificationContent.Replace("[index2]", parameters.Index2);
                notificationContent = notificationContent.Replace("[index3]", parameters.Index3);
                return notificationContent;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
