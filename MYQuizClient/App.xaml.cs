using MYQuizClient.Helpers;
using System.Collections.Generic;
using Xamarin.Forms;
using System;

namespace MYQuizClient
{
	public partial class App : Application
	{

        public NotificationManager NotificationManager;
        Networking networking;


        //Pages
        public Page loginView;
        public Page questionView;
        public Page preSendView;
        public Page waitingTimeAndFeedbackView;

		public App()
		{
			InitializeComponent();
                        
            //Networking über Singleton erstellen
            networking = Networking.Current;

            loginView = new LoginView();
            questionView = new QuestionView();
            preSendView = new PreSendView();
            waitingTimeAndFeedbackView = new WaitingTimeAndFeedbackView();

            navigateTo(loginView);
   		}


        public void navigateTo(Page page)
        {
            MainPage = new NavigationPage(page);
        }


        protected override async void OnStart()
        {

            // Handle when your app starts

            //TODO Manager load
            NotificationManager = new NotificationManager();

            try
            {

                //Register Pushnotification
                NotificationManager.Register();
                //MainPage.DisplayAlert("PushNotification Register successful!", "PushNotification should work now ^_^", "Ok");


                //register the device             
                registerDevice();


            }
            catch (System.Exception e)
            {

                await MainPage.DisplayAlert("Error", e.Message, "Cancel");
            }

        }


        RegistrationDevice regDeviceResponse; 

        private async void registerDevice()
        {
            //Erst wenn Device noch nicht registriert, registrieren ausführen
            if(regDeviceResponse == null)
            {
                regDeviceResponse = await networking.registerClientDevice();

                //save device id in application settings:
                Settings.ClientId = regDeviceResponse.id;
                //await MainPage.DisplayAlert("Device registered successfully!", "ClientId: " + Settings.ClientId, "Ok");
            }
        }

        protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
