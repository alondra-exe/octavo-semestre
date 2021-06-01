using Newtonsoft.Json;
using NoticiasMOVIL.Helpers;
using NoticiasMOVIL.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace NoticiasMOVIL.Repositories
{
    public class NoticiasRepository
    {
        public SQLiteConnection connection { get; set; }

        public NoticiasRepository()
        {
            var ruta = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/noticias.db3";
            connection = new SQLiteConnection(ruta);
            connection.CreateTable<Noticia>();
        }

        public static bool Exists
        {
            get
            {
                var rutabd = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/noticias.db3";
                return System.IO.File.Exists(rutabd);
            }
        }

        public void Insert(Noticia n)
        {
            connection.Insert(n);
        }

        public void Update(Noticia n)
        {
            connection.Update(n);
        }

        public void Delete(Noticia n)
        {
            connection.Delete(n);
        }

        public Noticia Get(int id)
        {
            return connection.Get<Noticia>(id);
        }

        public IEnumerable<Noticia> GetAll()
        {
            return connection.Table<Noticia>().OrderBy(x => x.Encabezado);
        }
    }
}