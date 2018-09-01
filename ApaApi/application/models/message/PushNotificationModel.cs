namespace application.models.message
{
    public class PushNotificationModel
    {
        public NotificationContentModel Notification { get; set; }
        public string[] RegistrationIds { get; set; }
    }

    public class NotificationContentModel
    {
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
