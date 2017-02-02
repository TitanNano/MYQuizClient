using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MYQuizClient
{
    public partial class WaitingTimeAndFeedbackView : ContentPage
    {
        private App App { get { return (MYQuizClient.App)Application.Current; } }

        public WaitingTimeAndFeedbackView()
        {
            InitializeComponent();

        }

        private void b_complete_Clicked(object sender, EventArgs e)
        {
            App.navigateTo(App.loginView);
        }

    }
}
