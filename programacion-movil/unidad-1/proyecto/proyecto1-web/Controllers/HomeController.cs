using Microsoft.AspNetCore.Mvc;
using proyecto1_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proyecto1_web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult InicioSesionDocente()
        {
            return View();
        }

        public async Task<IActionResult> InicioSesionDocente(Docente d)
        {
            
            try
            {
                
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(d);
            }

            //GymContext context = new GymContext();
            //Repository<Administrador> repository = new Repository<Administrador>(context);
            //var administrador = context.Administrador.FirstOrDefault(x => x.Clave == admin.Clave);
            //try
            //{
            //    if (administrador != null && administrador.Contrasena == HashHelper.GetHash(admin.Contrasena))
            //    {
            //        List<Claim> info = new List<Claim>();
            //        info.Add(new Claim(ClaimTypes.Name, "Usuario" + administrador.Nombre));
            //        info.Add(new Claim(ClaimTypes.Role, "Administrador"));
            //        info.Add(new Claim("clave", administrador.Nombre.ToString()));
            //        info.Add(new Claim("Nombre", administrador.Nombre));

            //        var claimsidentity = new ClaimsIdentity(info, CookieAuthenticationDefaults.AuthenticationScheme);
            //        var claimsprincipal = new ClaimsPrincipal(claimsidentity);
            //        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsprincipal,
            //            new AuthenticationProperties { IsPersistent = true });
            //        return RedirectToAction("IndexConfiguracion");
            //    }
            //    else
            //    {
            //        ModelState.AddModelError("", "La clave o la contraseña del administrador son incorrectas.");
            //        return View(admin);
            //    }
            
        }
    }
}
