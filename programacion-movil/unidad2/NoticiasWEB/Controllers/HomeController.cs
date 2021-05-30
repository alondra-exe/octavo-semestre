using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NoticiasWEB.Models;
using NoticiasWEB.Services;

namespace NoticiasWEB.Controllers
{
    public class HomeController : Controller
    {
        public IHttpClientFactory Factory { get; }
        public HomeController(IHttpClientFactory factory, MessagingService mensaje)
        {
            Mensaje = mensaje;
            Factory = factory;
        }

        public MessagingService Mensaje { get; set; }

        public async Task<IActionResult> Index()
        {
            HttpClient client = Factory.CreateClient("NoticiasAPI");
            var result = await client.GetAsync("noticias");
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json = await result.Content.ReadAsStringAsync();
                var datos = JsonConvert.DeserializeObject<List<Noticia>>(json);
                return View(datos);
            }
            else
            {
                ModelState.AddModelError("", "No se puede conectar con el servidor.");
                return View(new List<Noticia>());
            }

        }

        public IActionResult Agregar()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Agregar(Noticia n)
        {
            HttpClient client = Factory.CreateClient("NoticiasAPI");
            var json = JsonConvert.SerializeObject(n);
            var result = await client.PostAsync("noticias", new StringContent(json, Encoding.UTF8, "application/json"));
            if (result.IsSuccessStatusCode)
            {
                Mensaje.EnviarMensaje();
                return RedirectToAction("Index");
            }
            else if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var jsonerrores = await result.Content.ReadAsStringAsync();
                var lista = JsonConvert.DeserializeObject<List<string>>(jsonerrores);
                lista.ForEach(x => ModelState.AddModelError("", x));
                return View(n);
            }
            else
            {
                ModelState.AddModelError("", result.StatusCode.ToString());
                return View(n);
            }
        }

        public async Task<IActionResult> Editar(int id)
        {
            HttpClient client = Factory.CreateClient("NoticiasAPI");
            var result = await client.GetAsync("noticias/" + id);
            if (result.IsSuccessStatusCode)
            {
                var json = await result.Content.ReadAsStringAsync();
                var a = JsonConvert.DeserializeObject<Noticia>(json);
                return View(a);
            }
            else if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", result.StatusCode.ToString() + " " + result.ReasonPhrase);
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Noticia n)
        {
            HttpClient client = Factory.CreateClient("NoticiasAPI");
            var json = JsonConvert.SerializeObject(n);
            var result = await client.PutAsync("noticias", new StringContent(json, System.Text.Encoding.UTF8, "application/json"));
            if (result.IsSuccessStatusCode)
            {
                Mensaje.EnviarMensaje();
                return RedirectToAction("Index");
            }
            else if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var jsonerrores = await result.Content.ReadAsStringAsync();
                var lista = JsonConvert.DeserializeObject<List<string>>(jsonerrores);
                lista.ForEach(x => ModelState.AddModelError("", x));
                return View(n);
            }
            else
            {
                ModelState.AddModelError("", result.StatusCode.ToString());
                return View(n);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(Noticia n)
        {
            HttpClient client = Factory.CreateClient("NoticiasAPI");
            var json = JsonConvert.SerializeObject(n);
            var result = await client.DeleteAsync("noticias/" + n.Id);
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var jsonerrores = await result.Content.ReadAsStringAsync();
                var lista = JsonConvert.DeserializeObject<List<string>>(jsonerrores);
                lista.ForEach(x => ModelState.AddModelError("", x));
                return View(n);
            }
            else
            {
                ModelState.AddModelError("", result.StatusCode.ToString());
                return View(n);
            }
        }
    }
}