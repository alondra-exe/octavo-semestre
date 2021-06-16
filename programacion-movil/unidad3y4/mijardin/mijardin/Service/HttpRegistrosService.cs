using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using mijardin.Repositories;
using mijardin.Models;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace mijardin.Service
{
    public class HttpRegistrosService
    {
        HttpClient client;
        RegistroRepository repos;
        public HttpRegistrosService()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://jardinapialondra.sistemas171.com/");
        }

        public async Task<bool> DescargarRegistro()
        {
            bool Existencambios = false;
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                var result = await client.GetAsync("api/registro");

                if (result.IsSuccessStatusCode)
                {
                    var json = await result.Content.ReadAsStringAsync();
                    var registros = JsonConvert.DeserializeObject<Registro[]>(json);

                    if (repos == null) repos = new RegistroRepository();

                    foreach (var registro in registros)
                    {
                        var r = repos.Get(registro.Id);
                        if (r == null)
                        {
                            repos.Insert(registro);
                        }
                        else
                        {
                            r.Nombre = registro.Nombre;
                            r.Cientifico = registro.Cientifico;
                            r.Descripcion = registro.Descripcion;
                            r.Imagen = registro.Imagen;
                            repos.Update(r);
                        }
                    }
                    Existencambios = true;
                }
            }
            return Existencambios;
        }
    }
}