using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace MYQuizClient
{
    class Networking
    {
        string hostAddress = string.Empty;

        const string contentType = "application/json";
        const bool useFakeData = false;

        public Networking(string hostAddress)
        {

            this.hostAddress = hostAddress;
            
        }

        private async Task<T> sendRequest<T>(string route, string methode, object postData)
        {

            if (useFakeData)
            {
                //Fake data for offline service
                return default(T);
            }

            byte[] byteArray = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(postData));

            WebRequest request = WebRequest.Create(hostAddress + route);

            request.ContentType = contentType;
            request.Method = methode;

            Stream dataStream = await request.GetRequestStreamAsync();

            dataStream.Write(byteArray, 0, byteArray.Length);

            WebResponse response = await request.GetResponseAsync();

            StreamReader reader = new StreamReader(response.GetResponseStream());

            T value = JsonConvert.DeserializeObject<T>(reader.ReadToEnd());

            dataStream.Dispose();
            response.Dispose();
            reader.Dispose();

            return value;

        }

        public async void dummyRequest()
        {

            List<string> test = new List<string>() { "Hallo", "Welt" };

            var result = await sendRequest<List<string>>("/posts", "POST", test);

            return;
        }

    }
}
