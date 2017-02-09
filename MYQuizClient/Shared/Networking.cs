using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using MYQuizClient.Helpers;
 
using Xamarin.Forms;


namespace MYQuizClient
{
    //Networking als Singleton
    public sealed class Networking
    {

        private static Networking instance = null;
        //für Thread Safety - padlock
        private static readonly object padlock = new object();

        string hostAddress = string.Empty;

        const string contentType = "application/json";
        const bool useFakeData = false;

        public Networking(string hostAddress)
        {

            this.hostAddress = hostAddress;

        }

        //Eine Instanz erstellen bzw. wenn erstellt diese zurückgeben mit Thread-Safety
        public static Networking Current
        {
            get
            {
                lock (padlock)
                {
                    if(instance == null)
                    {
                        instance = new Networking("http://h2653223.stratoserver.net");
                    }
                    return instance;
                }
            }
        }



        private async Task<T> sendRequest<T>(string route, string methode, object postData, WebHeaderCollection headers)
        {
            WebRequest request = WebRequest.Create(hostAddress + route);
            request.ContentType = contentType;
            request.Method = methode;

            if (headers != null)
            {
                request.Headers = headers;
            }

            //Gruppe beitreten -> im Header muss zusätzlich "DeviceID" hinzu,
            //weil Header für Autorisierung benötigt wird.
            //REST-API-Pfad, um Ressource anzusprechen
            if(postData is Group)
            {                
               request.Headers["DeviceID"] = Settings.ClientId;
            }


            if (methode != "GET")
            {
                //Json soll Parameter mit NULL beim String-erstellen ignorieren
                var jsonString = JsonConvert.SerializeObject(postData, 
                                                        Newtonsoft.Json.Formatting.None,
                                                        new JsonSerializerSettings
                                                        {
                                                            NullValueHandling = NullValueHandling.Ignore
                                                        });

                byte[] byteArray = Encoding.UTF8.GetBytes(jsonString);
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

        

        //ClientDevice registrieren        
        public async Task<RegistrationDevice> registerClientDevice()
        {
            var mytoken = NotificationManager.token;
            var postData = new RegistrationDevice() { token = mytoken, id = null, pushUpToken = null, isAdmin = null};

            RegistrationDevice device = await sendRequest<RegistrationDevice>("/api/devices", "POST", postData, null);          

            return device;
        }
        
        
        //Client in Gruppe beitreten
        public async Task<Group> enterGroup(string groupPin)
        {
            var deviceId = Settings.ClientId;

            var postData = new Group() { enterGroupPin = groupPin, id = null, title = null };
            Group group = await sendRequest<Group>("/api/devices/" + deviceId + "/groups", "POST", postData, null);


            Debug.WriteLine("Networking - enterGroup: deviceId = " + deviceId);
            Debug.WriteLine("Networking - enterGroup: Title = " + group.title);

            return group;
        }
                 
  

        //Client sendet beantwortete Frage
        public async void sendAnsweredQuestion(GivenAnswer answer)
        {
            string route = "/api/givenAnswer";
            answer.DeviceId = Convert.ToInt64(Settings.ClientId);
            var postData = answer;
            var result = await sendRequest<object>(route, "POST", postData,null);
        }

        //Vorbereitete Fragen abrufen
        public async void getPreparedQuestions(string questionListID)
        {

            string route = "/api/questionLists/" + questionListID;

            var result = await sendRequest<object>(route, "GET", null,null);

        }

        //Gruppen abrufen
        public async void getGroups()
        {
            var result = await sendRequest<object>("/api/groups", "GET", null,null);
            Debug.WriteLine("Networking - Group Message: " + result);

        }

        //Get Questionnaire
        public async Task<Questionnaire> getQuestionnaire(string id)
        {
            return await sendRequest<Questionnaire>("/api/givenAnswer/" + id, "GET", null,null);

        }

        //Get latest Questionnaire
        public async Task<List<Questionnaire>> getLatestQuestionnaire()
        {
            WebHeaderCollection headers = new WebHeaderCollection();
            string cid = Settings.ClientId;
            headers["DeviceID"] = cid;

            return await sendRequest<List<Questionnaire>>("/api/givenAnswer/latest", "GET", null,headers);
        }
    }
}
