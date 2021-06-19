using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;
using MarcTron.Plugin;


namespace tutorialalodav.ViewModels
{
    public class TutorialVM : INotifyPropertyChanged
    {
        private int puntos;
        public int Puntos
        {
            get { return puntos; }
            set
            {
                puntos = value;
                OnPropertyChanged("Puntos");
            }
        }

        public Command BannerCommand { get; set; }
        public Command IntersticialCommand { get; set; }
        public Command RewardCommand { get; set; }
        public Command InfoCommand { get; set; }
        public Command LoadRewardCommand { get; set; }

        Views.Banner banner;
        Views.Interstitial interstitial;
        Views.Rewards rewards;
        Views.Info info;

        public TutorialVM()
        {
            BannerCommand = new Command(TutoBanner);
            IntersticialCommand = new Command(TutoIntersticial);
            RewardCommand = new Command(TutoRewards);
            InfoCommand = new Command(TutoInfo);
            LoadRewardCommand = new Command(LoadReward);
        }

        public void LoadReward()
        {
            CrossMTAdmob.Current.OnRewardedVideoAdLoaded += Current_OnRewardedVideoAdLoaded;
            CrossMTAdmob.Current.LoadRewardedVideo("ca-app-pub-3940256099942544/5224354917");
            CrossMTAdmob.Current.OnRewardedVideoAdCompleted += Current_OnRewardedVideoAdCompleted;
        }

        private void Current_OnRewardedVideoAdCompleted(object sender, EventArgs e)
        {
            Puntos ++;
        }

        private void Current_OnRewardedVideoAdLoaded(object sender, EventArgs e)
        {
            CrossMTAdmob.Current.ShowRewardedVideo();
        }

        public async void TutoBanner()
        {
            if (banner == null)
            {
                banner = new Views.Banner();
                banner.BindingContext = this;
            }
            await App.Current.MainPage.Navigation.PushAsync(banner);
        }
        public async void TutoIntersticial()
        {
            if (interstitial == null)
            {
                interstitial = new Views.Interstitial();
                interstitial.BindingContext = this;
            }
            await App.Current.MainPage.Navigation.PushAsync(interstitial);
        }
        private async void TutoRewards()
        {
            if (rewards == null)
            {
                rewards = new Views.Rewards();
                rewards.BindingContext = this;
            }
            await App.Current.MainPage.Navigation.PushAsync(rewards);
        }

        private async void TutoInfo()
        {
            if (info == null)
            {
                info = new Views.Info();
                info.BindingContext = this;
            }
            await App.Current.MainPage.Navigation.PushAsync(info);
        }

        

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
