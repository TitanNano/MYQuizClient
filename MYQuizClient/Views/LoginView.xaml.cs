using MYQuizClient.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MYQuizClient
{
    public partial class LoginView : ContentPage
    {

        private App App { get { return (MYQuizClient.App)Application.Current; } }

        public LoginView()
        {
            InitializeComponent();
        }

        public async void entry_Completed(object sender, EventArgs e)
        {

            bool isInGroup = await enterGroup();

            if (isInGroup == true)
            {
                //ToDo: if isInGroup == true do something

                //await DisplayAlert("Login", "Login in Gruppe erfolgreich", "ok");
            }
        }




        private async Task<bool> enterGroup()
        {
            bool getEntrance = false;

            //Nur wenn Device registriert ist, hat es eine ClientId,
            //dann darf man erst die Gruppe betreten.

            try
            {

                if (Settings.ClientId != String.Empty)
                {
                    string groupPin = en_pincode.Text;
                    var group = await Networking.Current.enterGroup(groupPin);

                    if (group != null)
                    {

                        //korrekte Eingabe
                        displayCorrect();
                        getEntrance = true;

                    }
                }
                else
                {
                    //Wenn Registrierung bei App.OnStart fehlgeschlagen
                    //hier nochmal registrieren:
                    RegistrationDevice registeredDevice = await Networking.Current.registerClientDevice();
                    Settings.ClientId = registeredDevice.id;
                    await enterGroup();
                }
            }
            catch (WebException webEx)
            {
                var errorMessage = webEx.Message;

                if (errorMessage == "The remote server returned an error: (400) Bad Request.")
                {
                    using (var errorResponse = (HttpWebResponse)webEx.Response)
                    {
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            string error = reader.ReadToEnd();

                            //Fehler abfangen

                            //Bereits eingeloggt
                            if (error == "Device already exists in Group")
                            {
                                displayLoggedIn();
                                getEntrance = false;
                            }


                            //Falscher PIn für Gruppe
                            if (error == "Group does not exist!")
                            {
                                displayWrongPin();
                                getEntrance = false;
                            }
                        }
                    }
                }
            }
            return getEntrance;
        }

        private void displayLoggedIn()
        {
            lb_loggedin.Text = "in Gruppe bereits beigetreten";
            lb_loggedin.IsVisible = true;
            lb_loggedin.TextColor = Color.Orange;
        }

        private void displayWrongPin()
        {
            lb_loggedin.Text = "falscher Pincode";
            lb_loggedin.IsVisible = true;
            lb_loggedin.TextColor = Color.Red;
        }

        private void displayCorrect()
        {
      
            lb_loggedin.Text = "Login erfolgreich";
            lb_loggedin.IsVisible = true;
            lb_loggedin.TextColor = Color.Green;
        }
    


    }
}
