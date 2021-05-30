using Newtonsoft.Json;
using NoticiasMOVIL.Models;
using NoticiasMOVIL.Helpers;
using NoticiasMOVIL.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Threading.Tasks;
using System.Linq;

namespace NoticiasMOVIL.ViewModels
{
    public class NoticiasViewModel : INotifyPropertyChanged
    {
        public Command VerCommand { get; set; }

        public SmartCollection<Noticia> Noticias { get; set; } = new SmartCollection<Noticia>();

        public NoticiasViewModel()
        {
            repository = new Repositories.NoticiasRepository();
            Noticias = repository.NoticiasAll;
            VerCommand = new Command<Noticia>(Ver);
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private bool cargando;
        public bool Cargando
        {
            get { return cargando; }
            set
            {
                cargando = value;
                OnPropertyChanged("IsRunning");
            }
        }
        private bool visible;
        public bool SinConexion
        {
            get { return visible; }
            set { visible = value; OnPropertyChanged("SinConexion"); }
        }
        private Noticia noticia;
        public Noticia Noticia
        {
            get { return noticia; }
            set { noticia = value; OnPropertyChanged("Noticia"); }
        }

        public bool EstaCargando
        {
            get { return cargando; }
            set { cargando = value; OnPropertyChanged(); }
        }

        NoticiasMOVIL.Repositories.NoticiasRepository repository;

        private async void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (Noticia != null && Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                SinConexion = false;
                Cargando = true;
                var resultado = await repository.Actualizar(Noticia);
                if (resultado != null)
                {
                    Noticia = resultado;
                }
                Cargando = false;
            }
        }

        Views.VerNoticia ver;
        private async void Ver(Noticia n)
        {
            if (ver == null)
            {
                ver = new Views.VerNoticia();
                ver.BindingContext = this;
            }
            Noticia = n;
            await App.Current.MainPage.Navigation.PushAsync(ver);
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                SinConexion = false;
                Cargando = true;
                var resultado = await repository.Actualizar(n);

                if (resultado != null)
                {
                    Noticia = resultado;
                }
                Cargando = false;
            }
            else
            {
                SinConexion = n.Contenido == null;
            }
        }
    }
}