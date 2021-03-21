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
        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public string error;
        
        public Command LoginCommand { get; set; }
        private bool log;
        public bool Log
        {
            get { return log; }
            set
            {
                log = value;
                OnPropertyChanged("Log");
            }
        }

        public Alumno alumno;
        public Alumno Alumno
        {
            get { return alumno; }
            set { alumno = value; OnPropertyChanged("Alumno"); }
        }

        

        public AlumnoViewModel()
        {
            LoginCommand = new Command<Alumno>(Login);
        }

       // Views.JuegoView jv;
        async void Login(Alumno a)
        {
            HttpClient client = new HttpClient();
            var json = JsonConvert.SerializeObject(a);
            var result = await client.PostAsync("https://juegoalondra-jesmeralda.sistemas171.com/api/alumnos/login", new StringContent(json, Encoding.UTF8, "application/json"));
            if (result.IsSuccessStatusCode)
            {
                log = true;
                alumno = JsonConvert.DeserializeObject<Alumno>(await result.Content.ReadAsStringAsync());
            }
            else
            {
                error = await result.Content.ReadAsStringAsync();
                log = false;
            }

            //if (jv == null)
            //{
            //    jv = new Views.JuegoView();
            //    jv.BindingContext = this;
            //}
        }
    }
}
