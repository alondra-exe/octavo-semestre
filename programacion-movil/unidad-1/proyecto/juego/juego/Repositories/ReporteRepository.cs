using juego.Models;
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
        public ReporteRepository(int id)
        {
            Reportes = new ObservableCollection<Progreso>();
            Descargar(id);
        }

        async void Descargar(int id)
        {
            HttpClient client = new HttpClient();
            var result = await client.GetAsync("https://juegoalondra-jesmeralda.sistemas171.com/api/progreso/" + id);
            if (result.IsSuccessStatusCode)
            {
                var json = await result.Content.ReadAsStringAsync();
                var reporte = JsonConvert.DeserializeObject<Progreso[]>(json);
                foreach (var r in reporte)
                {
                    Reportes.Add(r);
                }
            }
            Guardar(Reportes);
        }

        private void Guardar(ObservableCollection<Progreso> lista)
        {
            var ruta = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/reportes.json";
            var datos = JsonConvert.SerializeObject(lista);
            File.WriteAllText(ruta, datos);
        }
    }
}