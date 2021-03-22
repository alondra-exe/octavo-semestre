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
        }

        Views.PrincipalView pv;
        Views.ProgresoView prov;

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
    }
}
