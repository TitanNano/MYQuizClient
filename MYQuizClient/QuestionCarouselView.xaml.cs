using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MYQuizClient
{
    public partial class QuestionCarouselView : CarouselPage
    {
        public QuestionCarouselView()
        {
            InitializeComponent();

            List<Antwort> list_antworten = new List<Antwort>()
            {
                new Antwort() { text="in Berlin", typ=Typus.falsch },
                new Antwort() { text="in Bayern", typ=Typus.falsch },
                new Antwort() { text="in Hessen", typ=Typus.falsch },
                new Antwort() { text="in Baden-Württemberg", typ=Typus.richtig }
            };

            List<myContentPage> list_myCpages = new List<myContentPage>() {
                new myContentPage() { gruppenname="VL_Gruppe1", fragebogenname="Fragebogen 1" , fragentext="Frage 1",
                    antwortliste = list_antworten },
                new myContentPage() { gruppenname="VL_Gruppe1", fragebogenname="Fragebogen 1" , fragentext="Frage 2",
                    antwortliste = list_antworten },
                new myContentPage() { gruppenname="VL_Gruppe1", fragebogenname="Fragebogen 1" , fragentext="Frage 3",
                    antwortliste = list_antworten },
                new myContentPage() { gruppenname="VL_Gruppe1", fragebogenname="Fragebogen 1" , fragentext="Frage 4",
                    antwortliste = list_antworten },


            };

            this.ItemsSource = list_myCpages;
        }


        public async void OnFragenSelected(object sender, EventArgs args)
        {
            Antwort antwort = (sender as ListView).SelectedItem as Antwort;

            if (antwort != null)
            {
                await DisplayAlert("Antwort ist ...", antwort.typ.ToString(), "ok");
            }
        }

        public void OnNext(object sender, EventArgs args)
        {
            var nextIndex = Children.IndexOf(CurrentPage) + 1;

            if (nextIndex == Children.Count())
            {
                nextIndex = 0;
            }

            CurrentPage = Children[nextIndex];
        }


    }


    public class Antwort
    {
        public string text { get; set; }
        public Typus typ { get; set; }
    }

    public enum Typus
    {
        falsch,
        richtig
    }

    public class myContentPage
    {
        public string gruppenname { get; set; }
        public string fragebogenname { get; set; }
        public string fragentext { get; set; }
        public List<Antwort> antwortliste { get; set; }

        public myContentPage()
        {
            antwortliste = new List<Antwort>();
        }
    }
}
