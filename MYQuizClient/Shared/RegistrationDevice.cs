using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYQuizClient
{
    public class RegistrationDevice
    {
        public string token { get; set; }
        public string id { get; set; }

       
        //leerer Konstruktor für Deserialisierung
        public RegistrationDevice() { }
    }
}
