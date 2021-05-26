using Newtonsoft.Json;
using NoticiasMOVIL.Models;
using NoticiasMOVIL.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NoticiasMOVIL.Repositories
{
    public class NoticiasRepository
    {
        public SmartCollection<Noticia> NoticiasAll { get; set; }
        public NoticiasRepository()
        {
            var ruta = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/noticias.json";
            if (File.Exists(ruta))
            {
                string jsondata = File.ReadAllText(ruta);
                NoticiasAll = JsonConvert.DeserializeObject<SmartCollection<Noticia>>(jsondata);
            }
            else
            {
                NoticiasAll = new SmartCollection<Noticia>();
                Descargar();
            }
        }

        async void Descargar()
        {
            SmartCollection<Noticia> temporal = new SmartCollection<Noticia>();
                HttpClient client = new HttpClient();
                var result = await client.GetAsync("https://apinoticiasalondra.sistemas171.com/api/noticias");
                if (result.IsSuccessStatusCode)
                {
                    var json = await result.Content.ReadAsStringAsync();
                    var noticia = JsonConvert.DeserializeObject<Noticia[]>(json);
                        NoticiasAll.AddRange(noticia);
                        temporal.AddRange(noticia);
                }
            //Guardar(temporal);
            //NoticiasAll.AddRange(temporal);
        }

        private void Guardar(SmartCollection<Noticia> obj)
        {
            var ruta = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/noticias.json";
            var datos = JsonConvert.SerializeObject(obj);
            File.WriteAllText(ruta, datos);
        }

        public async Task<Noticia> Actualizar(Noticia n)
        {
            if (n.Contenido == null)
            {
                HttpClient client = new HttpClient();
                var result = await client.GetAsync("https://apinoticiasalondra.sistemas171.com/api/noticias/" + n.Id);
                if (result.IsSuccessStatusCode)
                {
                    var json = await result.Content.ReadAsStringAsync();
                    var clon = JsonConvert.DeserializeObject<Noticia>(json);
                    var index = NoticiasAll.IndexOf(n);
                    NoticiasAll[index] = clon;
                    Guardar(NoticiasAll);
                    return clon;
                }
                return null;
            }
            return null;
        }
    }
}