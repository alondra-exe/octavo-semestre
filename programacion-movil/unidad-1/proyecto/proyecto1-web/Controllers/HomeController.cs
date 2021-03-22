using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using proyecto1_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace proyecto1_web.Controllers
{
    public class HomeController : Controller
    {
        public IHttpClientFactory Factory { get; }
        public HomeController(IHttpClientFactory factory)
        {
            Factory = factory;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult InicioSesion()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> InicioSesion(Docente d)
        {
            HttpClient client = Factory.CreateClient("proyecto1-api");
            d.Correo = d.Correo.ToLower();
            var contra = Encoding.UTF8.GetBytes(d.Contrasena);
            d.Contrasena = Convert.ToBase64String(contra);

            var json = JsonConvert.SerializeObject(d);
            var result = await client.PostAsync("docentes/login", new StringContent(json, Encoding.UTF8, "application/json"));

            try
            {

                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {

                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Role, "Docente"));
                    claims.Add(new Claim("Id", d.Id.ToString()));
                    claims.Add(new Claim("Correo", d.Correo));

                    var claimidentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimprincipal = new ClaimsPrincipal(claimidentity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimprincipal,
                    new AuthenticationProperties { IsPersistent = true });

                    return RedirectToAction("Index");
                }

                else
                {
                    ModelState.AddModelError("", "La clave o la contraseña del docente son incorrectas.");
                    return View(d);
                }
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(d);
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("InicioSesion");
        }

        [Authorize(Roles = "Docente")]
        public async Task<IActionResult> Index()
        {
            HttpClient client = Factory.CreateClient("proyecto1-api");
            var result = await client.GetAsync("alumnos");
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json = await result.Content.ReadAsStringAsync();
                var datos = JsonConvert.DeserializeObject<List<Alumno>>(json);
                return View(datos);
            }
            else
            {
                ModelState.AddModelError("", "No se puede conectar con el servidor.");
                return View(new List<Alumno>());
            }

        }

        [Authorize(Roles = "Docente")]
        public IActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Docente")]
        public async Task<IActionResult> Agregar(Alumno a)
        {
            HttpClient client = Factory.CreateClient("proyecto1-api");
            var json = JsonConvert.SerializeObject(a);
            var result = await client.PostAsync("alumnos", new StringContent(json, Encoding.UTF8, "application/json"));
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var jsonerrores = await result.Content.ReadAsStringAsync();
                var lista = JsonConvert.DeserializeObject<List<string>>(jsonerrores);
                lista.ForEach(x => ModelState.AddModelError("", x));
                return View(a);
            }
            else
            {
                ModelState.AddModelError("", result.StatusCode.ToString());
                return View(a);
            }
        }

        [Authorize(Roles = "Docente")]
        public async Task<IActionResult> Editar(int id)
        {
            HttpClient client = Factory.CreateClient("proyecto1-api");
            var result = await client.GetAsync("alumnos/" + id);
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
        [Authorize(Roles = "Docente")]
        public async Task<IActionResult> Editar(Alumno a)
        {
            HttpClient client = Factory.CreateClient("proyecto1-api");
            var json = JsonConvert.SerializeObject(a);
            var result = await client.PutAsync("alumnos", new StringContent(json, System.Text.Encoding.UTF8, "application/json"));
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var jsonerrores = await result.Content.ReadAsStringAsync();
                var lista = JsonConvert.DeserializeObject<List<string>>(jsonerrores);
                lista.ForEach(x => ModelState.AddModelError("", x));
                return View(a);
            }
            else
            {
                ModelState.AddModelError("", result.StatusCode.ToString());
                return View(a);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Docente")]
        public async Task<IActionResult> Eliminar(Alumno a)
        {
            HttpClient client = Factory.CreateClient("proyecto1-api");
            var json = JsonConvert.SerializeObject(a);
            var result = await client.DeleteAsync("alumnos/" + a.Id);
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var jsonerrores = await result.Content.ReadAsStringAsync();
                var lista = JsonConvert.DeserializeObject<List<string>>(jsonerrores);
                lista.ForEach(x => ModelState.AddModelError("", x));
                return View(a);
            }
            else
            {
                ModelState.AddModelError("", result.StatusCode.ToString());
                return View(a);
            }
        }

        [Authorize(Roles = "Docente")]
        public async Task<IActionResult> VerProgreso(Progreso p)
        {
            HttpClient client = Factory.CreateClient("proyecto1-api");
            var result = await client.GetAsync("progreso/" + p.IdAlumno);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json = await result.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject<List<Progreso>>(json);
                return View(obj);
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
    }
}
