using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MarcTron.Plugin;

namespace tutorialalodav.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Interstitial : ContentPage
    {
        public Interstitial()
        {
            InitializeComponent();

            CrossMTAdmob.Current.OnInterstitialLoaded += Current_OnInterstitialLoaded;
        }

        private void Current_OnInterstitialLoaded(object sender, EventArgs e)
        {
            CrossMTAdmob.Current.ShowInterstitial();
        }

        private void interstitial_Clicked(object sender, EventArgs e)
        {
            CrossMTAdmob.Current.LoadInterstitial("ca-app-pub-1184057908298837/2554720736");
        }
    }
}