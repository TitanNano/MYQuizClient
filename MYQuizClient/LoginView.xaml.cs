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
        public LoginView()
        {
            InitializeComponent();
        }

        //Event called by TextChanged from en_pincode.
        public async void TextChangedNavigation(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new Fragenseite());
        }

    }
}
