using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NoticiasMOVIL.Repositories;
using NoticiasMOVIL.Models;
using Xamarin.Essentials;
using NoticiasMOVIL.Helpers;
using System.IO;
using Xamarin.Forms;

namespace NoticiasMOVIL.Services
{
    public class HttpNoticiasService
    {
        HttpClient client;
        NoticiasRepository repos;
        public HttpNoticiasService()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://apinoticiasalondra.sistemas171.com/");
        }

        public async Task<bool> DescargarNoticias()
        {
            bool Existencambios = false;
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                var result = await client.GetAsync("api/noticias");

                if (result.IsSuccessStatusCode)
                {
                    var json = await result.Content.ReadAsStringAsync();
                    var noticias = JsonConvert.DeserializeObject<Noticia[]>(json);

                    if (repos == null) repos = new NoticiasRepository();

                    foreach (var noticia in noticias)
                    {
                        var n = repos.Get(noticia.Id);
                        if (n == null)
                        {
                            repos.Insert(noticia);
                        }
                        else
                        {
                            n.Encabezado = noticia.Encabezado;
                            n.Contenido = noticia.Contenido;
                            n.Autor = noticia.Autor;
                            n.Lugar = noticia.Lugar;
                            n.Fecha = noticia.Fecha;
                            repos.Update(n);
                        }
                    }
                    Existencambios = true;
                }
            }
            return Existencambios;
        }
    }
}