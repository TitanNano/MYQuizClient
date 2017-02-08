using MYQuizClient.Helpers;
using System.Collections.Generic;
using Xamarin.Forms;
using System;
using System.Diagnostics;

namespace MYQuizClient
{
	public partial class App : Application
	{

        public NotificationManager NotificationManager;
        public Networking networking;

        //Pages
        public LoginView loginView;
        public QuestionView questionView;
        public PreSendView preSendView;
        public WaitingTimeAndFeedbackView waitingTimeAndFeedbackView;

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
            //Erst wenn Device noch nicht registriert, 
            //d.h. es existiert noch keine ClientId,
            //dann Registrieren ausführen für
            //PushNotification und Device
            if( Settings.ClientId == String.Empty)
            {
                //Register Pushnotification
                NotificationManager.Register();

                //Device registrieren
                regDeviceResponse = await networking.registerClientDevice();

                //save device id in application settings:
                Settings.ClientId = regDeviceResponse.id.ToString();
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
