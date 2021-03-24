using juego.Models;
using juego.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace juego.ViewModels
{
    public class AlumnoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ObservableCollection<Progreso> Reporte { get; set; }

        public Command LoginCommand { get; set; }
        public Command ReportesCommand { get; set; }
        public Command MultiplicacionesCommand { get; set; }
        public Command JugarCommand { get; set; }
        public Command IniciarJuegoCommand { get; set; }
        public Command ResultadoCommand { get; set; }

        private Alumno alumnologin;
        public Alumno AlumnoLogin
        {
            get { return alumnologin; }
            set { alumnologin = value; OnPropertyChanged("AlumnoLogin"); }
        }

        public AlumnoViewModel()
        {
            alumnologin = new Alumno();
            LoginCommand = new Command<Alumno>(Iniciar);
            ReportesCommand = new Command(VerReportes);
            if (AlumnoLogin == null)
            {
                alumnologin = new Alumno();
            }
            IniciarJuegoCommand = new Command<string>(IniciarJuego);
            JugarCommand = new Command(Jugar);
            MultiplicacionesCommand = new Command(Multiplicaciones);
            ResultadoCommand = new Command(ResultadoJuego);
        }

        private int num1;
        public int Num1
        {
            get { return num1; }
            set
            {
                num1 = value;
                OnPropertyChanged("Num1");
            }
        }
        private int num2;
        public int Num2
        {
            get { return num2; }
            set
            {
                num2 = value;
                OnPropertyChanged("Num2");
            }
        }
        private int resultado;
        public int Resultado
        {
            get { return resultado; }
            set
            {
                resultado = value;
                OnPropertyChanged("Resultado");
            }
        }

        private int puntos = 0;

        Views.PrincipalView pv;
        Views.ProgresoView prov;
        Views.JuegoView jv;
        Views.SeleccionView sv;
        Views.ResultadosView rv;

        public async void Iniciar(Alumno a)
        {
            HttpClient client = new HttpClient();
            var json = JsonConvert.SerializeObject(AlumnoLogin);
            var result = await client.PostAsync("https://juegoalondra-jesmeralda.sistemas171.com/api/alumnos/login", new StringContent(json, Encoding.UTF8, "application/json"));
            if (result.IsSuccessStatusCode)
            {
                AlumnoLogin = JsonConvert.DeserializeObject<Alumno>(await result.Content.ReadAsStringAsync());
                if (pv == null)
                {
                    pv = new Views.PrincipalView();
                    pv.BindingContext = this;
                }
                await App.Current.MainPage.Navigation.PushAsync(pv);
            }
            else
            {
                //error = await result.Content.ReadAsStringAsync();
            }
        }
        public async void VerReportes()
        {
            ReporteRepository repos = new Repositories.ReporteRepository(AlumnoLogin.Id);
            Reporte = repos.Reportes;
            if (prov == null)
            {
                prov = new Views.ProgresoView();
                prov.BindingContext = this;
            }
            await App.Current.MainPage.Navigation.PushAsync(prov);
        }
        public async void Jugar()
        {
            var result = Num1 * Num2;
            Num2++;
            if (Resultado == result)
            {
                puntos++;
            }

            if (Num2 == 11)
            {
                Progreso p = new Progreso
                {
                    Puntuacion = puntos,
                    Intentos = Num1,
                    Fecha = DateTime.Now,
                    IdAlumno = AlumnoLogin.Id
                };
                HttpClient client = new HttpClient();
                var json = JsonConvert.SerializeObject(p);
                var resulta = await client.PostAsync("https://juegoalondra-jesmeralda.sistemas171.com/api/progreso",
                    new StringContent(json, Encoding.UTF8, "application/json"));
                if (resulta.IsSuccessStatusCode)
                {
                    await App.Current.MainPage.Navigation.PopAsync();
                    jv = null;
                    puntos = 0;
                }
            }
        }

        public async void Multiplicaciones()
        {
            if (sv == null)
            {
                sv = new Views.SeleccionView();
                sv.BindingContext = this;
            }
            await App.Current.MainPage.Navigation.PushAsync(sv);
        }

        public async void IniciarJuego(string multiplicaciones)
        {
            num1 = int.Parse(multiplicaciones);
            num2 = 1;
            if (jv == null)
            {
                jv = new Views.JuegoView();
                jv.BindingContext = this;
            }
            await App.Current.MainPage.Navigation.PushAsync(jv);
        }

        public async void ResultadoJuego()
        {
            for (var i = 1; i < App.Current.MainPage.Navigation.NavigationStack.Count; i++)
            {
                App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack
                    [App.Current.MainPage.Navigation.NavigationStack.Count-1]);
            }
            await App.Current.MainPage.Navigation.PopAsync();
            jv = null;
            rv = null;
            puntos = 0;
        }
    }
}