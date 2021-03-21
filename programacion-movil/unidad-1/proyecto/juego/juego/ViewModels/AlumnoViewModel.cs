using juego.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        public Command LoginCommand { get; set; }
        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
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
        }

        Views.PrincipalView pv;
        public async void Iniciar(Alumno a)
        {
            HttpClient client = new HttpClient();
            var json = JsonConvert.SerializeObject(AlumnoLogin);
            var result = await client.PostAsync("https://juegoalondra-jesmeralda.sistemas171.com/api/alumnos/login", new StringContent(json, Encoding.UTF8, "application/json"));
            if (result.IsSuccessStatusCode)
            {
                AlumnoLogin = JsonConvert.DeserializeObject<Alumno>(await result.Content.ReadAsStringAsync());
                pv = new Views.PrincipalView();
                pv.BindingContext = this;
                await App.Current.MainPage.Navigation.PushAsync(pv);
            }
            else
            {
                //error = await result.Content.ReadAsStringAsync();
            }
        }
    }
}
