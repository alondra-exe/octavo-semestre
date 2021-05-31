using NoticiaMovil.Models;
using NoticiaMovil.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NoticiaMovil.ViewModels
{
    public class NoticiasViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public Command VerCommand { get; set; }
        public Command ActualizarCommand { get; set; }

        public ObservableCollection<Noticia> Noticias { get; set; } = new ObservableCollection<Noticia>();

        public NoticiasViewModel()
        {
            ActualizarCommand = new Command(() => { _ = Descargar(); });
            //VerCommand = new Command<Noticia>(Ver);
            App.ActualizacionRealizada += App_ActualizacionRealizada; Actualizar();
            _ = Descargar();
        }

        private void App_ActualizacionRealizada()
        {
            Actualizar();
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

        private bool cargando;
        public bool EstaCargando
        {
            get { return cargando; }
            set { cargando = value; OnPropertyChanged(); }
        }

        //Views.VerNoticia ver;

        public async Task Descargar()
        {
            EstaCargando = true;
            await App.Descargar();
            EstaCargando = false;
        }

        public void Actualizar()
        {
            NoticiasRepository repos = new NoticiasRepository();
            Noticias.Clear();
            var todas = repos.GetAll();
            foreach (var item in todas)
            {
                Noticias.Add(item);
            }
        }

        //private async void Ver(Noticia n)
        //{
        //    if (ver == null)
        //    {
        //        ver = new Views.VerNoticia();
        //        ver.BindingContext = this;
        //    }
        //    Noticia = n;
        //    await App.Current.MainPage.Navigation.PushAsync(ver);
        //    if (Connectivity.NetworkAccess == NetworkAccess.Internet)
        //    {
        //        SinConexion = false;
        //        EstaCargando = true;
        //        var resultado = await repository.Actualizar(n);

        //        if (resultado != null)
        //        {
        //            Noticia = resultado;
        //        }
        //        EstaCargando = false;
        //    }
        //    else
        //    {
        //        SinConexion = n.Contenido == null;
        //    }
        //}
    }
}
