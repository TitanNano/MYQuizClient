using MYQuizClient.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public LoginView()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public void entry_Completed(object sender, EventArgs e)
        {

			enterGroup();
            //App.navigateTo(App.questionView);
        }

        public void btn_loadQuestionnaire_Click(object sender, EventArgs e)
        {
            isNotBusy = false;
            App.questionView.loadQuestionnaire();

        }


        public async void enterGroup()
        {
            try {
                //Nur wenn Device registriert ist, hat es eine ClientId,
                //dann darf man erst die Gruppe betreten.
                if (Settings.ClientId != String.Empty)
                {
                    string groupPin = en_pincode.Text;
                    var group = await Networking.Current.enterGroup(groupPin);

                    //Wenn als Antwort vom Server die Gruppe zurückgeliefert wird,
                    //darf ich zur QuestionView
                    if (group != null)
                    {
                        App.navigateTo(App.questionView);
                    }
                }else
                {
                    //Falls Registrierung beim App.OnStart() misslingt
                    //hier nochmal neu registrieren:
                    RegistrationDevice registeredDevice = await Networking.Current.registerClientDevice();
                    Settings.ClientId = registeredDevice.id;
                    enterGroup();
                }
            }
            catch(Exception e)
            {
                var errorMessage = e.Message;

                if(errorMessage == "The remote server returned an error: (400) Bad Request.")
                {
                    lb_loggedin.IsVisible = true;
                    lb_loggedin.Text = "falscher Pincode";

                }

                
            }
        }


    }
}
