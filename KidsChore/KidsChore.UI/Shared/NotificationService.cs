using Microsoft.AspNetCore.Components;

namespace VibeHome.UI.Shared
{
    public class NotificationService
    {
        public event Action<NotificationMessage>? OnNotification;

        public void ShowSuccess(string message, int duration = 3000)
        {
            OnNotification?.Invoke(new NotificationMessage
            {
                Type = NotificationType.Success,
                Message = message,
                Duration = duration
            });
        }

        public void ShowError(string message, int duration = 5000)
        {
            OnNotification?.Invoke(new NotificationMessage
            {
                Type = NotificationType.Error,
                Message = message,
                Duration = duration
            });
        }

        public void ShowInfo(string message, int duration = 3000)
        {
            OnNotification?.Invoke(new NotificationMessage
            {
                Type = NotificationType.Info,
                Message = message,
                Duration = duration
            });
        }

        public void ShowWarning(string message, int duration = 4000)
        {
            OnNotification?.Invoke(new NotificationMessage
            {
                Type = NotificationType.Warning,
                Message = message,
                Duration = duration
            });
        }
    }

    public class NotificationMessage
    {
        public NotificationType Type { get; set; }
        public string Message { get; set; } = string.Empty;
        public int Duration { get; set; } = 3000;
        public Guid Id { get; set; } = Guid.NewGuid();
    }

    public enum NotificationType
    {
        Success,
        Error,
        Info,
        Warning
    }
} 