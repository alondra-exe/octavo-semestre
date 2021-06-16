using mijardin.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using mijardin.Models;
using mijardin.Repositories;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace mijardin.ViewModels
{
    public class RegistrosViewModel : INotifyPropertyChanged
    {
        public SmartCollection<Registro> ListaPlantas { get; set; } = new SmartCollection<Registro>();
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public Command VerCommand { get; set; }
        public Command VerTodasCommand { get; set; }
        public Command ActualizarCommand { get; set; }

        Views.PrincipalView inicioView;
        Views.VerPlanta verView;

        private Registro registro;
        public Registro Registro
        {
            get { return registro; }
            set { registro = value; OnPropertyChanged("Registro"); }
        }

        private bool cargando;
        public bool EstaCargando
        {
            get { return cargando; }
            set { cargando = value; OnPropertyChanged(); }
        }

        public RegistrosViewModel()
        {
            ActualizarCommand = new Command(() => { _ = Descargar(); });
            VerTodasCommand = new Command(VerTodas);
            VerCommand = new Command<Registro>(Ver);
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChangedAsync;
            App.ActualizacionRealizada += App_ActualizacionRealizada1;
            Actualizar();
            _ = Descargar();
        }

        private void App_ActualizacionRealizada1()
        {
            Actualizar();
        }

        private async void Connectivity_ConnectivityChangedAsync(object sender, ConnectivityChangedEventArgs e)
        {
            if (ListaPlantas == null)
            {
                await Descargar();
            }
            else
            {
                Actualizar();
            }
        }

        private void Actualizar()
        {
            RegistroRepository repos = new RegistroRepository();
            ListaPlantas.Clear();
            var plantas = repos.GetAll();
            EstaCargando = false;
            ListaPlantas.AddRange(plantas);
        }

        private async Task Descargar()
        {
            EstaCargando = true;
            await App.Descargar();
            EstaCargando = false;
        }

        private async void Ver(Registro r)
        {
            verView = new Views.VerPlanta();
            verView.BindingContext = this;
            Registro = r;
            await App.Current.MainPage.Navigation.PushAsync(verView);
        }
        private async void VerTodas()
        {
            inicioView = new Views.PrincipalView();
            inicioView.BindingContext = this;
            await App.Current.MainPage.Navigation.PushAsync(inicioView);
        }
    }
}
