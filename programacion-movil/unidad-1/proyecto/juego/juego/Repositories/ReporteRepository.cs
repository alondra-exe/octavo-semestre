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
    public class ReporteRepository
    {
        public ObservableCollection<Progreso> Datos { get; set; }
        public ReporteRepository()
        {
            var ruta = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/reportes.json";
            if (File.Exists(ruta))
            {
                string jsondata = File.ReadAllText(ruta);
                Datos = JsonConvert.DeserializeObject<ObservableCollection<Progreso>>(jsondata);
            }
            else
            {
                Datos = new ObservableCollection<Progreso>();
                Descargar();
            }
        }

        async void Descargar()
        {
            {
                HttpClient client = new HttpClient();
                var resulta = await client.GetAsync("https://juegoalondra-jesmeralda.sistemas171.com/api/progreso/");
                if (resulta.IsSuccessStatusCode)
                {
                    var json = await resulta.Content.ReadAsStringAsync();
                    var reporte = JsonConvert.DeserializeObject<Progreso[]>(json);
                    foreach (var r in reporte)
                    {
                        Datos.Add(r);
                    }
                }
            }
            var ruta = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/reportes.json";
            var datos = JsonConvert.SerializeObject(Datos);
            File.WriteAllText(ruta, datos);
        }
    }
}