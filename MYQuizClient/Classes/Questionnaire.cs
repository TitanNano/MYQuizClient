using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYQuizClient
{
    public class Questionnaire
    {

        public double timeStamp { get; set; }
        public SingleTopic singleTopic { get; set; } = new SingleTopic() { Name = "Fragebogen" };
        public QuestionBlock questionBlock { get; set; }
        public long Id { get; set; }

        public Questionnaire() { }

        public static Questionnaire generateTestData()
        {
            return new Questionnaire()
            {
                timeStamp = DateTime.Now.AddMinutes(2).Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds,
                singleTopic = new SingleTopic() { Name = "TestDataSingleTopic" },
                questionBlock = new QuestionBlock()
                {
                    Id = 100,
                    Title = "TestDataQuestionBlock",
                    Questions = new ObservableCollection<Question>()
                    {
                        new Question()
                        {
                            Id=1,
                            Category="FirstQuestion",
                            MultipleChoice="True",
                            Text="Is this the first Question?",
                            AnswerOptions = new List<AnswerOption>()
                            {
                                new AnswerOption()
                                {
                                    Id=11,
                                    Text="First Answer for first Question",
                                    IsCorrect="True"
                                },
                                new AnswerOption()
                                {
                                    Id=12,
                                    Text="Second Answer for first Question",
                                    IsCorrect="IsCorrectQ1A2"
                                },
                                new AnswerOption()
                                {
                                    Id=13,
                                    Text="Third Answer for first Question",
                                    IsCorrect="IsCorrectQ1A3"
                                },
                                new AnswerOption()
                                {
                                    Id=14,
                                    Text="Fourth Answer for first Question",
                                    IsCorrect="IsCorrectQ1A4"
                                }
                            }
                        },
                        new Question()
                        {
                            Id=2,
                            Category="SecondQuestion",
                            MultipleChoice="MultipleChoiceQ2",
                            Text="Is this the second Question?",
                            AnswerOptions = new List<AnswerOption>()
                            {
                                new AnswerOption()
                                {
                                    Id=21,
                                    Text="First Answer for second Question",
                                    IsCorrect="IsCorrectQ2A1"
                                },
                                new AnswerOption()
                                {
                                    Id=22,
                                    Text="Second Answer for second Question",
                                    IsCorrect="IsCorrectQ2A2"
                                },
                                new AnswerOption()
                                {
                                    Id=23,
                                    Text="Third Answer for second Question",
                                    IsCorrect="IsCorrectQ2A3"
                                }
                            }
                        },
                        new Question()
                        {
                            Id=3,
                            Category="ThirdQuestion",
                            MultipleChoice="MultipleChoiceQ3",
                            Text="Is this the third Question?",
                            AnswerOptions = new List<AnswerOption>()
                            {
                                new AnswerOption()
                                {
                                    Id=31,
                                    Text="First Answer for third Question",
                                    IsCorrect="IsCorrectQ2A1"
                                },
                                new AnswerOption()
                                {
                                    Id=32,
                                    Text="Second Answer for third Question",
                                    IsCorrect="IsCorrectQ2A2"
                                },
                                new AnswerOption()
                                {
                                    Id=33,
                                    Text="Third Answer for third Question",
                                    IsCorrect="IsCorrectQ2A3"
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}
