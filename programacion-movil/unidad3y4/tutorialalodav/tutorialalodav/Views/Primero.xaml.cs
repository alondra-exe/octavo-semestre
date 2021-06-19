using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace tutorialalodav.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Primero : ContentPage
    {
        public Primero()
        {
            InitializeComponent();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.Gold;
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.Black;
        }
    }
}