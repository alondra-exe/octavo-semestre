using NoticiaMovil.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoticiaMovil
{
    public partial class App : Application
    {
        public static event Action ActualizacionRealizada;
        public static HttpNoticiasService NoticiasServices { get; set; } = new HttpNoticiasService();
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new Views.IncioView());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public static async Task Descargar()
        {
            DateTime fechaultimaactualizado = Preferences.Get("fechaAct", DateTime.MinValue);
            var fecha = DateTime.Now;
            var resultado = await NoticiasServices.DescargarNoticias(fechaultimaactualizado);
            if (resultado)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    ActualizacionRealizada?.Invoke();
                });
            }
            Preferences.Set("fechaAct", fecha);
        }
    }
}