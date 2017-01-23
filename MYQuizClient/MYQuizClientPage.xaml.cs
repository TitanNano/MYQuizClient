using System;
using Xamarin.Forms;

namespace MYQuizClient
{
	public partial class MYQuizClientPage : ContentPage
	{
        string pincode;

		public MYQuizClientPage()
		{
			InitializeComponent();
            pincode = "1234";
        }


        public async void OnPincode(object sender, EventArgs args)
        {
            var eingabetext = en_pincode.Text;

            //Pincode muss 4-stellig sein:
            if (eingabetext.Length == 4)
            {
                if (eingabetext == pincode)
                {

                    lb_loggedin.Text = "eingeloggt";
                    en_pincode.Text = "";
                    await Navigation.PushAsync(new Fragenseite());
                }
                else
                {
                    lb_loggedin.Text = "Pincode ist falsch";
                }
            }
        }
    }
}

