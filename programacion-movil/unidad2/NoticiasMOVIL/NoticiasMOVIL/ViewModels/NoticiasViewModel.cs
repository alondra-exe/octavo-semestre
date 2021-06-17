using NoticiasMOVIL.Models;
using NoticiasMOVIL.Repositories;
using NoticiasMOVIL.Helpers;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace NoticiasMOVIL.ViewModels
{
    public class NoticiasViewModel : INotifyPropertyChanged
    {
        public SmartCollection<Noticia> ListaNoticias { get; set; } = new SmartCollection<Noticia>();
        public SmartCollection<Noticia> ListaMexico { get; set; } = new SmartCollection<Noticia>();
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public Command VerCommand { get; set; }
        public Command VerTodasCommand { get; set; }
        public Command VerMexicoCommand { get; set; }
        public Command ActualizarCommand { get; set; }

        Views.InicioView inicioView;
        Views.VerNoticia verView;
        Views.MexicoView mexicoView;

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
            set { cargando = value; OnPropertyChanged("EstaCargando"); }
        }

        public NoticiasViewModel()
        {
            ActualizarCommand = new Command(() => { _ = Descargar(); });
            VerTodasCommand = new Command(VerTodas);
            VerMexicoCommand = new Command(VerMexico);
            VerCommand = new Command<Noticia>(Ver);
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChangedAsync;
            App.ActualizacionRealizada += App_ActualizacionRealizada;
            Actualizar();
            _ = Descargar();
        }

        private void App_ActualizacionRealizada()
        {
            Actualizar();
        }
        
        private void Actualizar()
        {
            NoticiasRepository repos = new NoticiasRepository();
            ListaNoticias.Clear();
            ListaMexico.Clear();
            var noticias = repos.GetAll();
            foreach (var noticia in noticias)
            {
                if (noticia.Lugar == "Mexico" || noticia.Lugar == "México" || noticia.Lugar == "MEXICO" || noticia.Lugar == "MÉXICO")
                {
                    ListaMexico.Add(noticia);
                }
                ListaNoticias.Add(noticia);
            }
            EstaCargando = false;
        }
        private async void Connectivity_ConnectivityChangedAsync(object sender, ConnectivityChangedEventArgs e)
        {
            if (ListaNoticias == null)
            {
                await Descargar();
            }
            else
            {
                Actualizar();
            }
        }

        private async Task Descargar()
        {
            EstaCargando = true;
            await App.Descargar();
            EstaCargando = false;
        }

        private async void Ver(Noticia n)
        {
            verView = new Views.VerNoticia();
            verView.BindingContext = this;
            Noticia = n;
            await App.Current.MainPage.Navigation.PushAsync(verView);
        }
        private async void VerTodas()
        {
            inicioView = new Views.InicioView();
            inicioView.BindingContext = this;
            await App.Current.MainPage.Navigation.PushAsync(inicioView);
        }
        private async void VerMexico()
        {
            mexicoView = new Views.MexicoView();
            mexicoView.BindingContext = this;
            await App.Current.MainPage.Navigation.PushAsync(mexicoView);
        }
    }
}