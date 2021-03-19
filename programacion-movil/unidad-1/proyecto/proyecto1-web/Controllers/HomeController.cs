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

        [Authorize(Roles = "Docente")]
        public IActionResult Index()
        {
            return View();
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
            var result = await client.PostAsync("docente/iniciosesion", new StringContent(json, Encoding.UTF8, "application/json"));

            try
            {

                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {

                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Role, "Docente"));
                    claims.Add(new Claim("Correo electronico", d.Correo));

                    var claimidentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimprincipal = new ClaimsPrincipal(claimidentity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimprincipal,
                    new AuthenticationProperties { IsPersistent = true });

                    return RedirectToAction("ListaAlumnos");
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
            return RedirectToAction("Index");
        }
    }
}
