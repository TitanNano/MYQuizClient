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

        //Additional info


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
