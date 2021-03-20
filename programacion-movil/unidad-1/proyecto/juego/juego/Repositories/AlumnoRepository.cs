using juego.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Text;

namespace juego.Repositories
{
    public class AlumnoRepository
    {
        async void Login()
        {
            HttpClient client = new HttpClient();
            var resulta = await client.GetAsync("https://juegoalondra-jesmeralda.sistemas171.com/api/alumnos/login");
            if (resulta.IsSuccessStatusCode)
            {
                var json = await resulta.Content.ReadAsStringAsync();
            }
        }
    }
}