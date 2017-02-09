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

        public DateTime endTime;

        public Dictionary<Question, AnswerOption> answers = new Dictionary<Question, AnswerOption>();

        public bool IsQuestionnaireCompleted
        {
            get
            {
                foreach (ContentPage page in Children)
                {
                    ListView lv = page.FindByName<ListView>("lv_question");
                    Question question = lv.BindingContext as Question;
                    if (answers.ContainsKey(question))
                    {
                        if (answers[question] != null)
                        {
                            continue;
                        }
                        return false;
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
        }

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
                List<Questionnaire> questionnaires = await App.networking.getLatestQuestionnaire();
                questionnaires.OrderBy(x => x.timeStamp);
                currentQuestionnaire = questionnaires[0];
                //currentQuestionnaire = Questionnaire.generateTestData();

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
            endTime = DateTime.Now.AddMinutes(2);
            p_time = endTime.Subtract(DateTime.Now);

            Xamarin.Forms.Device.StartTimer(new TimeSpan(0, 0, 1), OnTimerTick);
        }

        public void OnCurrentPageChanged(object sender, EventArgs e)
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

        public void OnQuestionSelected(object sender, EventArgs args)
        {
            ListView lv = sender as ListView;

            Question question = lv.BindingContext as Question;
            AnswerOption answer = lv.SelectedItem as AnswerOption;

            
            if (answers.ContainsKey(question))
            {
                answers.Remove(question);
            }
            answers.Add(question, answer);
            OnPropertyChanged("IsQuestionnaireCompleted");
        }

        public void OnNext(object sender, EventArgs args)
        {
            var nextIndex = Children.IndexOf(CurrentPage) + 1;

            if (nextIndex == Children.Count())
            {
                App.navigateTo(App.preSendView);
                CurrentPage = Children[0];
                return;
            }

            CurrentPage = Children[nextIndex];
        }
    }
}
