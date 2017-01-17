using System.Collections.Generic;
using Xamarin.Forms;

namespace MYQuizClient
{
	public partial class App : Application
	{

        public NotificationManager nManager;

		public App()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new MYQuizClientPage());
		}

		protected override void OnStart()
		{
            // Handle when your app starts

            //TODO Manager load
            nManager = new NotificationManager();

            try
            {

                nManager.Register();
                MainPage.DisplayAlert("Yay!", "blub", "Cancel");

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
