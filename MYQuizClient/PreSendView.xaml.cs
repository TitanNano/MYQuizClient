using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MYQuizClient
{
    public partial class PreSendView : ContentPage
    {

        private App App { get { return (MYQuizClient.App)Application.Current; } }

        public PreSendView()
        {
            InitializeComponent();
        }

        private void b_yes_Clicked(object sender, EventArgs e)
        {

            App.navigateTo(App.watingTimeAndFeedbackView);

        }

        private void b_no_Clicked(object sender, EventArgs e)
        {

            App.navigateTo(App.frageSeite);

        }
    }
}
