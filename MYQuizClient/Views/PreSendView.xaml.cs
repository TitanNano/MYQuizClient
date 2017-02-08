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

        public bool IsUserEndeVisible { get { OnPropertyChanged("IsUserEndeVisible"); return App.questionView.IsQuestionnaireCompleted; } }
        public bool IsOffeneFragenVisible { get { OnPropertyChanged("IsOffeneFragenVisible"); return !App.questionView.IsQuestionnaireCompleted; } }

        public PreSendView()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private void b_yes_Clicked(object sender, EventArgs e)
        {
            //TODO - Send Answers
            /**
             * Example:
             * How to get answers -> in QuestionView
             * 
             * List<AnswerOption> answerOptions = new List<AnswerOption>();
             * 
             * foreach(ContentPage page in Children)
             * {
             *      answerOptions.Add(page.FindByName<ListView>("lv_question").SelectedItem as AnswerOption);
             * }
             * 
             **/
            App.navigateTo(App.waitingTimeAndFeedbackView);

        }

        private void b_no_Clicked(object sender, EventArgs e)
        {

            if ((new DateTime(1970,1,1,0,0,0,DateTimeKind.Utc).AddSeconds(App.questionView.currentQuestionnaire.timeStamp)).Subtract(DateTime.Now).TotalMilliseconds > 0)
            {
                List<Question> newQuestionList = new List<Question>();

                var notAnsweredPages = (from t in App.questionView.Children where t.FindByName<ListView>("lv_question").SelectedItem == null select t).AsEnumerable();

                foreach (ContentPage notAnsweredPage in notAnsweredPages)
                {
                    newQuestionList.Add(notAnsweredPage.FindByName<ListView>("lv_question").BindingContext as Question);
                }

                Dictionary<Question,AnswerOption> rememberAnswers = new Dictionary<Question, AnswerOption>();

                foreach (Question question in App.questionView.currentQuestionnaire.questionBlock.Questions)
                {
                    if (!newQuestionList.Contains(question))
                    {
                        newQuestionList.Add(question);
                        rememberAnswers.Add(question, App.questionView.Children[App.questionView.currentQuestionnaire.questionBlock.Questions.IndexOf(question)].FindByName<ListView>("lv_question").SelectedItem as AnswerOption);
                    }
                }

                App.questionView.currentQuestionnaire.questionBlock.Questions = newQuestionList;
                OnPropertyChanged("App.questionView.currentQuestionnaire");

                foreach (ContentPage page in App.questionView.Children)
                {
                    ListView lv = page.FindByName<ListView>("lv_question");
                    lv.SelectedItem = from r in rememberAnswers where r.Key == lv.BindingContext select r.Value;
                }

                App.navigateTo(App.questionView);
            }
            else
            {
                App.navigateTo(App.loginView);
            }
        }
    }
}
