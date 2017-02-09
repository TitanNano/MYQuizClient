using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

            new GivenAnswer() { };

            App.navigateTo(App.waitingTimeAndFeedbackView);

        }

        private void b_no_Clicked(object sender, EventArgs e)
        {

            if (App.questionView.endTime.Subtract(DateTime.Now).TotalMilliseconds > 0)
            {

                List<Question> sortedList = (App.questionView.ItemsSource as ObservableCollection<Question>).OrderBy(x => App.questionView.answers.ContainsKey(x)).ToList();
                (App.questionView.ItemsSource as ObservableCollection<Question>).Clear();
                foreach (Question q in sortedList)
                {
                    (App.questionView.ItemsSource as ObservableCollection<Question>).Add(q);
                }

                foreach (ContentPage page in App.questionView.Children)
                {
                    ListView lv = page.FindByName<ListView>("lv_question");
                    lv.SelectedItem = (from r in App.questionView.answers where r.Key == lv.BindingContext select r.Value).FirstOrDefault();
                }

                App.questionView.OnCurrentPageChanged(null, null);
                App.navigateTo(App.questionView);
            }
            else
            {
                App.navigateTo(App.loginView);
            }
        }
    }
}
