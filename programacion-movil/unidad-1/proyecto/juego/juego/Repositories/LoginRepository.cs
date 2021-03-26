using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace juego.Repositories
{
    public class LoginRepository
    {
        public LoginRepository()
        {
            var ruta = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/reportes.json";
            if (File.Exists(ruta))
            {
                File.Delete(ruta);
            }
        }
    }
}