using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using mijardin.Models;

namespace mijardin.Repositories
{
    public class RegistroRepository
    {
        public SQLiteConnection connection { get; set; }

        public RegistroRepository()
        {
            var ruta = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/plantas.db3";
            connection = new SQLiteConnection(ruta);
            connection.CreateTable<Registro>();
        }

        public static bool Exists
        {
            get
            {
                var rutabd = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/plantas.db3";
                return System.IO.File.Exists(rutabd);
            }
        }

        public void Insert(Registro r)
        {
            connection.Insert(r);
        }

        public void Update(Registro r)
        {
            connection.Update(r);
        }

        public void Delete(Registro r)
        {
            connection.Delete(r);
        }

        public Registro Get(int id)
        {
            return connection.Table<Registro>().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Registro> GetAll()
        {
            return connection.Table<Registro>().OrderBy(x => x.Nombre);
        }
    }
}
