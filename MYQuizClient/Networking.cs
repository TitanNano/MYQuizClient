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

            WebRequest request = WebRequest.Create(hostAddress + route);
            request.ContentType = contentType;
            request.Method = methode;

            if (methode != "GET")
            {

                byte[] byteArray = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(postData));
                Stream dataStream = await request.GetRequestStreamAsync();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Dispose();

            }

            WebResponse response = await request.GetResponseAsync();

            StreamReader reader = new StreamReader(response.GetResponseStream());

            T value = JsonConvert.DeserializeObject<T>(reader.ReadToEnd());

            response.Dispose();
            reader.Dispose();

            return value;

        }

        //Device registrieren
        public async void registerDevice(string deviceId, string password)
        {
            var postData = JsonConvert.SerializeObject(new RegistrationDevice(NotificationManager.token, deviceId, password));

            var result = await sendRequest<object>("/api/devices", "POST", postData);
        }

        //Client sendet beantwortete Frage
        public async void sendAnsweredQuestion(string groupId, string questionId, GivenAnswer answer)
        {
            string route = "/api/groups/" + groupId + "/questions/" + questionId + "/answers";

            var postData = JsonConvert.SerializeObject(answer);

            var result = await sendRequest<object>(route, "POST", postData);
        }

        //Vorbereitete Fragen abrufen
        public async void getPreparedQuestions(string questionListID)
        {

            string route = "/api/questionLists/" + questionListID;

            var result = await sendRequest<object>(route, "GET", null);

        }

        //Gruppen abrufen
        public async void getGroups()
        {
            var result = await sendRequest<object>("/api/groups", "GET", null);
            
        }
    }
}
