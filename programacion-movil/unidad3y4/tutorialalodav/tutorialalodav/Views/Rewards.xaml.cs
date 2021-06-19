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
    public partial class Rewards : ContentPage
    {
        public Rewards()
        {
            InitializeComponent();
            CrossMTAdmob.Current.OnRewardedVideoAdLoaded += Current_OnRewardedVideoAdLoaded;
        }

        private void Current_OnRewardedVideoAdLoaded(object sender, EventArgs e)
        {
            CrossMTAdmob.Current.ShowRewardedVideo();
        }

        private void reward_Clicked(object sender, EventArgs e)
        {
            CrossMTAdmob.Current.LoadRewardedVideo("ca-app-pub-3940256099942544/5224354917");
        }
    }
}