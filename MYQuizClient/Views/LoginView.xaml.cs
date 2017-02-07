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

        private bool p_isNotBusy = true;

        public bool isNotBusy { get { return p_isNotBusy; } set { p_isNotBusy = value; OnPropertyChanged("isNotBusy"); } }

        private bool isInGroup;

        public LoginView()
        {
            InitializeComponent();

            checkRegistered();
        }

        private async void checkRegistered()
        {
            if (isRegistered())
            {
               en_pincode.IsEnabled = true;
            }
            else
            {
               en_pincode.IsEnabled = false;
               RegistrationDevice registeredDevice = await Networking.Current.registerClientDevice();
               Settings.ClientId = registeredDevice.id.ToString();
               checkRegistered();
                

                
            }
        }

        //Wenn registriert wird das Eingabefeld enabled, sonst nicht  
        private bool isRegistered()
        {
           if(Settings.ClientId != String.Empty)
            {                
                return true;
            }
           else
            {                
                return false;
            }
            

        }

        public async void entry_Completed(object sender, EventArgs e)
        {

			isInGroup = await enterGroup();
            //App.navigateTo(App.questionView);
        }

        public void btn_loadQuestionnaire_Click(object sender, EventArgs e)
        {
            isNotBusy = false;
            App.questionView.loadQuestionnaire();


            if (isInGroup)
            {
                //ToDo: if isInGroup == true do something
			}

        }




        private async Task<bool> enterGroup()
        {
            bool getEntrance = false;

            //Nur wenn Device registriert ist, hat es eine ClientId,
            //dann darf man erst die Gruppe betreten.

            try
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
            catch (WebException webEx)
            {
                                            
                    using (var errorResponse = (HttpWebResponse)webEx.Response)
                    {

                        if (errorResponse.StatusCode == HttpStatusCode.BadRequest)                   
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
