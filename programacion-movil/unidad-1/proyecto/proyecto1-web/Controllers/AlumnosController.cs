using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using proyecto1_web.Models;

namespace proyecto1_web.Controllers
{
    public class AlumnosController : Controller
    {
        public IHttpClientFactory Factory { get; }
        public AlumnosController(IHttpClientFactory factory)
        {
            Factory = factory;
        }
        public async Task<IActionResult> Index()
        {
            HttpClient cliente = Factory.CreateClient("proyecto1-api");
            var result = await cliente.GetAsync("alumnos");

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json = await result.Content.ReadAsStringAsync();
                var datos = JsonConvert.DeserializeObject<List<Models.Alumno>>(json);
                return View(datos);
            }
            else
            {
                ModelState.AddModelError("", "No se puede conectar con el servidor de datos");
                return View(new List<Models.Alumno>());
            }
        }

        public IActionResult AgregarAlumno()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AgregarAlumno(Models.Alumno alumno)
        {
            HttpClient cliente = Factory.CreateClient("proyecto1-api");

            var json = JsonConvert.SerializeObject(alumno);
            var result = await cliente.PostAsync("alumnos", new StringContent(json, System.Text.Encoding.UTF8, "application/json"));

            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var jsonerrores = await result.Content.ReadAsStringAsync();
                var lista = JsonConvert.DeserializeObject<List<string>>(jsonerrores);

                lista.ForEach(x => ModelState.AddModelError("", x));

                return View(alumno);
            }
            else
            {
                ModelState.AddModelError("", result.StatusCode.ToString());
                return View(alumno);
            }
        }

        public async Task<IActionResult> EditarAlumno(int id)
        {
            HttpClient cliente = Factory.CreateClient("proyecto1-api");
            var result = await cliente.GetAsync("alumnos/" + id);

            if (result.IsSuccessStatusCode)
            {
                var json = await result.Content.ReadAsStringAsync();
                var a = JsonConvert.DeserializeObject<Alumno>(json);
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
        public async Task<IActionResult> EditarAlumno(Models.Alumno alumno)
        {
            HttpClient cliente = Factory.CreateClient("proyecto1-api");

            var json = JsonConvert.SerializeObject(alumno);
            var result = await cliente.PutAsync("alumnos", new StringContent(json, System.Text.Encoding.UTF8, "application/json"));

            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var jsonerrores = await result.Content.ReadAsStringAsync();
                var lista = JsonConvert.DeserializeObject<List<string>>(jsonerrores);

                lista.ForEach(x => ModelState.AddModelError("", x));

                return View(alumno);
            }
            else
            {
                ModelState.AddModelError("", result.StatusCode.ToString());
                return View(alumno);
            }
        }
         
        public async Task<IActionResult> VerProgreso()
        {
            HttpClient cliente = Factory.CreateClient("proyecto1-api");
            var result = await cliente.GetAsync("progreso");

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json = await result.Content.ReadAsStringAsync();
                var datos = JsonConvert.DeserializeObject<List<Models.Progreso>>(json);
                return View(datos);
            }
            else
            {
                ModelState.AddModelError("", "No se puede conectar con el servidor de datos");
                return View(new List<Models.Progreso>());
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> EliminarAlumno(Alumno alumno)
        //{
        //    HttpClient cliente = Factory.CreateClient("proyecto1-api");
           
        //    var json = JsonConvert.SerializeObject(alumno);
        //    var result = await cliente.DeleteAsync("alumno");

        //    if (result.IsSuccessStatusCode)
        //    {
        //        return RedirectToAction("Index");
        //    }

        //    else if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
        //    {
        //        var jsonerrores = await result.Content.ReadAsStringAsync();
        //        var lista = JsonConvert.DeserializeObject<List<string>>(jsonerrores);

        //        lista.ForEach(x => ModelState.AddModelError("", x));

        //        return View(alumno);
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", result.StatusCode.ToString());
        //        return View(alumno);
        //    }
        //}

    }
}
