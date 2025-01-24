using System;
using System.Collections.Generic;

namespace Lab3_TranLeHienDuc_CSE422
{
    public class AdvancedNotificationService : NotificationService
    {
        public AdvancedNotificationService(Library library) : base(library, true)
        {
        }

        public override void SendNotification(string message)
        {
            var enhancedMessage = $"PRIORITY: {message}";
            base.SendNotification(enhancedMessage);
        }

        public override void SendNotification(string message, string recipient)
        {
            var enhancedMessage = $"PRIORITY: {message}";
            base.SendNotification(enhancedMessage, recipient);
        }

        public override void SendNotification(string message, List<string> recipients)
        {
            var enhancedMessage = $"PRIORITY: {message}";
            base.SendNotification(enhancedMessage, recipients);
        }
    }
}
