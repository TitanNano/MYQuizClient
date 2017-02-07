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
            //App.navigateTo(App.questionView);
        }

        public void btn_loadQuestionnaire_Click(object sender, EventArgs e)
        {
            isNotBusy = false;
            App.questionView.loadQuestionnaire();
        }
    }
}
