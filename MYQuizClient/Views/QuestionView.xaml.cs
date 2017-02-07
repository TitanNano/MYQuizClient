using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MYQuizClient
{
    public partial class QuestionView : CarouselPage
    {
        private App App { get { return (MYQuizClient.App)Application.Current; } }

        private double p_progress;
        public double progress { get { return p_progress; } set { p_progress = value; OnPropertyChanged("progress"); } }

        private TimeSpan p_time { get; set; }
        public string time { get { return string.Format("{0:mm}:{0:ss}", p_time); } }

        private Questionnaire p_currentQuestionnaire;
        public Questionnaire currentQuestionnaire { get { return p_currentQuestionnaire; } set { p_currentQuestionnaire = value; OnPropertyChanged("currentQuestionnaire"); } }

        private DateTime endTime;

        public string currentSingleTopic
        {
            get
            {
                if (currentQuestionnaire != null)
                {
                    if (currentQuestionnaire.singleTopic != null)
                    {
                        return currentQuestionnaire.singleTopic.Name;
                    }
                    if (currentQuestionnaire.questionBlock.Title != string.Empty)
                    {
                        return currentQuestionnaire.questionBlock.Title;
                    }
                }
                return "Fragebogen";
            }
        }

        public QuestionView()
        {
            InitializeComponent();

            BindingContext = this;

        }

        public async void loadQuestionnaire()
        {
            try
            {

                currentQuestionnaire = await App.networking.getQuestionnaire("1");

            }
            catch (Exception e)
            {

                await App.loginView.DisplayAlert("Netzwerkfehler", "Lloyd ist schuld!", "Macht nichts'", "Ok");
                currentQuestionnaire = Questionnaire.generateTestData();
            }
            startTimer();
            App.navigateTo(this);
            App.loginView.isNotBusy = true;
        }

        public void startTimer()
        {
            DateTime unixTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            endTime = unixTime.AddSeconds(currentQuestionnaire.timeStamp);

            p_time = endTime.Subtract(DateTime.Now);

            Xamarin.Forms.Device.StartTimer(new TimeSpan(0, 0, 1), OnTimerTick);
        }

        private void OnCurrentPageChanged(object sender, EventArgs e)
        {
            double indexOfCurrentPage = Children.IndexOf(CurrentPage);
            double childrenCount = Children.Count;

            progress = (indexOfCurrentPage + 1) / childrenCount;
        }

        public bool OnTimerTick()
        {
            if(p_time.TotalMilliseconds>0)
            {
                p_time = endTime.Subtract(DateTime.Now);
                OnPropertyChanged("time");
                return true;
            }
            OnPropertyChanged("time");
            App.navigateTo(App.preSendView);
            return false;
        }

        public async void OnQuestionSelected(object sender, EventArgs args)
        {
            AnswerOption answer = (sender as ListView).SelectedItem as AnswerOption;

            if (answer != null)
            {
                await DisplayAlert("Antwort ist ...", answer.IsCorrectBool ? "richtig" : "falsch", "ok");
            }
        }

        public void OnNext(object sender, EventArgs args)
        {
            var nextIndex = Children.IndexOf(CurrentPage) + 1;

            if (nextIndex == Children.Count())
            {
                App.navigateTo(App.preSendView);
                return;
            }

            CurrentPage = Children[nextIndex];
        }
    }
}
