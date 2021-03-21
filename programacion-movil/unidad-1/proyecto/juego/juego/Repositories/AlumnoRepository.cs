using juego.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace juego.Repositories
{
    public class AlumnoRepository
    {
        public string error;
        public bool log;
        public Alumno alumno;
        public Command LoginCommand{ get; set; }

        public AlumnoRepository()
        {
            LoginCommand = new Command<Alumno>(Login);
        }

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
        }
    }
}