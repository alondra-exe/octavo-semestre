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

namespace NoticiasMOVIL.Services
{
    public class HttpNoticiasService
    {
        HttpClient client;

        public HttpNoticiasService()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://apinoticiasalondra.sistemas171.com/api/");
        }

        NoticiasRepository repos;

        public async Task<bool> DescargarNoticias()
        {
            bool hubocambios = false;
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                var result = await client.GetAsync("noticias");
                if (result.IsSuccessStatusCode)
                {
                    if (result.Content.Headers.ContentLength > 0)
                    {
                        var json = await result.Content.ReadAsStringAsync();
                        var noticias = JsonConvert.DeserializeObject<Noticia[]>(json);

                        if (repos == null) repos = new NoticiasRepository();

                        foreach (var n in noticias)
                        {
                            var existente = repos.Get(n.Id);
                            if (existente == null)
                            {
                                repos.Insert(n);
                            }
                            else
                            {
                                existente.Encabezado = n.Encabezado;
                                existente.Contenido = n.Contenido;
                                existente.Autor = n.Autor;
                                existente.Lugar = n.Lugar;
                                existente.Fecha = n.Fecha;
                                repos.Update(existente);
                            }
                        }
                        hubocambios = true;
                    }
                }
            }
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                var result = await client.GetAsync("noticias");
                if (result.IsSuccessStatusCode)
                {
                    if (result.Content.Headers.ContentLength > 0)
                    {
                        var json = await result.Content.ReadAsStringAsync();
                        var noticias = JsonConvert.DeserializeObject<Noticia[]>(json);
                        if (repos == null) repos = new NoticiasRepository();

                        foreach (var n in noticias)
                        {
                            var existente = repos.Get(n.Id);
                            if (existente == null)
                            {
                                repos.Insert(n);
                            }
                            else
                            {
                                existente.Encabezado = n.Encabezado;
                                existente.Contenido = n.Contenido;
                                existente.Autor = n.Autor;
                                existente.Lugar = n.Lugar;
                                existente.Fecha = n.Fecha;
                                repos.Update(existente);
                            }
                        }
                        hubocambios = true;
                    }
                }

            }
            return hubocambios;
        }
    }
}