using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PushNotification.Plugin;

namespace MYQuizClient
{
    public class NotificationManager
    {
        //Received Token after Registration.
        public static string token = string.Empty;

        public static TaskCompletionSource<bool> WhenReady { get; set; } = new TaskCompletionSource<bool>();

        public void Register()
        {

            CrossPushNotification.Current.Register();

        }

        public void Unregister()
        {
            CrossPushNotification.Current.Unregister();
        }
    }
}
