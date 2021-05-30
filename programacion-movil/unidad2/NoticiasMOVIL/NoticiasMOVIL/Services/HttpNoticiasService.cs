using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NoticiasMOVIL.Repositories;
using Xamarin.Essentials;

namespace NoticiasMOVIL.Services
{
    public class HttpNoticiasService
    {
        HttpClient client;

        public HttpNoticiasService()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://noticiasalondra.sistemas171.com/");
        }

        NoticiasRepository repos;

        public async Task<bool> DescargarNoticias(DateTime fechaUltimaActualizacion)
        {
            bool hubocambios = false;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {

                var result = await client.GetAsync("api/noticias/false/" + fechaUltimaActualizacion.ToString("yyyy-MM-dd"));


                if (result.IsSuccessStatusCode)
                {
                    if (result.Content.Headers.ContentLength > 0)
                    {

                        var json = await result.Content.ReadAsStringAsync();
                        if (repos == null) repos = new NoticiasRepository();

                        hubocambios = true;
                    }
                }
            }
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                var result = await client.GetAsync("api/noticias/true/" + fechaUltimaActualizacion.ToString("yyyy-MM-dd"));

                if (result.IsSuccessStatusCode)
                {
                    if (result.Content.Headers.ContentLength > 0)
                    {
                        var json = await result.Content.ReadAsStringAsync();
                        if (repos == null) repos = new NoticiasRepository();
                        hubocambios = true;
                    }
                }
            }
            return hubocambios;
        }
    }
}