using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MYQuizClient
{
    public partial class Fragenseite : CarouselPage
    {

        private App App { get { return (MYQuizClient.App)Application.Current; } }

        public Fragenseite()
        {
            InitializeComponent();
        }

        private void btn_next_Clicked(object sender, EventArgs e)
        {

            App.navigateTo(App.preSendView);

        }
    }
}
