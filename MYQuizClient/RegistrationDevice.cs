using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYQuizClient
{
    public class RegistrationDevice
    {
        public string token;
        public string deviceID;
        public string password;

        public RegistrationDevice(string token, string deviceID, string password)
        {
            this.token = token;
            this.deviceID = deviceID;
            this.password = password;
        }
    }
}
