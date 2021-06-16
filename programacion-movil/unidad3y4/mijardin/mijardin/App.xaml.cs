using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using mijardin.Service;
using System.Threading.Tasks;

namespace mijardin
{
    public partial class App : Application
    {
        public static event Action ActualizacionRealizada;
        public static HttpRegistrosService PlantasServices { get; set; } = new HttpRegistrosService();
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new Views.PrincipalView());
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
            var resultado = await PlantasServices.DescargarRegistro();
            if (resultado)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    ActualizacionRealizada?.Invoke();
                });
            }
        }
    }
}
