using System.Collections.Generic;
using Xamarin.Forms;

namespace MYQuizClient
{
	public partial class App : Application
	{

        public NotificationManager NotificationManager;

        //Pages
        public Page loginView;
        public Page frageSeite;
        public Page preSendView;
        public Page watingTimeAndFeedbackView;

		public App()
		{
			InitializeComponent();

            Networking networking = new Networking("http://h2653223.stratoserver.net");

            loginView = new LoginView();
            frageSeite = new Fragenseite();
            preSendView = new PreSendView();
            watingTimeAndFeedbackView = new WatingTimeAndFeedbackView();

            navigateTo(loginView);
		}

        public void navigateTo(Page page)
        {
            MainPage = new NavigationPage(page);
        }

		protected override void OnStart()
		{
            // Handle when your app starts

            //TODO Manager load
            NotificationManager = new NotificationManager();

            try
            {

                NotificationManager.Register();
                MainPage.DisplayAlert("PushNotification Register successful!", "PushNotification should work now ^_^", "Ok");

            }
            catch (System.Exception e)
            {

                MainPage.DisplayAlert("Error", e.Message, "Cancel");
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
