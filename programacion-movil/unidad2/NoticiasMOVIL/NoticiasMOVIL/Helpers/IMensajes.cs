using System;
using System.Collections.Generic;
using System.Text;

namespace NoticiasMOVIL.Helpers
{
    public interface IMensajes
    {
        void MostrarToast(string mensaje);
        void Notificaciones(string mensaje);
    }
}