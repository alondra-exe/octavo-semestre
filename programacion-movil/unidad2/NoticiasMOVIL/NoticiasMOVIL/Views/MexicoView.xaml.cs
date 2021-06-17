using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoticiasMOVIL.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MexicoView : ContentPage
    {
        public MexicoView()
        {
            InitializeComponent();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.OrangeRed;
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
        }
    }
}