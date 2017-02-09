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

        private TimeSpan p_time { get; set; }
        public string time { get { return string.Format("{0:mm}:{0:ss}", p_time); } }

        public WaitingTimeAndFeedbackView()
        {
            InitializeComponent();

            Device.StartTimer(new TimeSpan(0, 0, 1), OnTimerTick);

            BindingContext = this;
        }

        private bool OnTimerTick()
        {
            if (p_time.TotalMilliseconds > 0)
            {
                p_time = App.questionView.endTime.Subtract(DateTime.Now);
                OnPropertyChanged("time");
                return true;
            }
            OnPropertyChanged("time");
            App.navigateTo(App.loginView);
            return false;
        }

        private void b_complete_Clicked(object sender, EventArgs e)
        {
            App.navigateTo(App.loginView);
        }

    }
}
