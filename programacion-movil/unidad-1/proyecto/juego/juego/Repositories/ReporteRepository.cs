using juego.Models;
using juego.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace juego.Repositories
{
    public class ReporteRepository
    {
        public ObservableCollection<Progreso> Reportes { get; set; }
        public ReporteRepository()
        {
            var ruta = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/reportes.json";
            if (File.Exists(ruta))
            {
                string jsondata = File.ReadAllText(ruta);
                Reportes = JsonConvert.DeserializeObject<ObservableCollection<Progreso>>(jsondata);
            }
            else
            {
                Reportes = new ObservableCollection<Progreso>();
            }
        }

        async void Descargar(Progreso p)
        {
            SmartCollection<Progreso> temportal = new SmartCollection<Progreso>();
            HttpClient client = new HttpClient();
            var result = await client.GetAsync("https://juegoalondra-jesmeralda.sistemas171.com/api/progreso/" + p.IdAlumno);
            if (result.IsSuccessStatusCode)
            {
                var json = await result.Content.ReadAsStringAsync();
                var reporte = JsonConvert.DeserializeObject<Progreso[]>(json);
            }
            var ruta = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/reportes.json";
            var datos = JsonConvert.SerializeObject(Reportes);
            File.WriteAllText(ruta, datos);
        }

        private void Guardar(SmartCollection<Progreso> lista)
        {
            var ruta = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/reportes.json";
            var datos = JsonConvert.SerializeObject(lista);
            File.WriteAllText(ruta, datos);
        }

        public async Task<Progreso> ActualizarReporte(Progreso p)
        {
            if (p.Fecha == null)
            {
                HttpClient client = new HttpClient();
                var result = await client.GetAsync("https://juegoalondra-jesmeralda.sistemas171.com/api/progreso/" + p.IdAlumno);
                if (result.IsSuccessStatusCode)
                {
                    var json = await result.Content.ReadAsStringAsync();
                    var clon = JsonConvert.DeserializeObject<Progreso>(json);
                    var index = Reportes.IndexOf(p);
                    Guardar(Reportes);
                    return clon;
                }
                return null;
            }
            return null;
        }
    }
}