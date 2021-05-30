using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoticiasWEB.Services
{
    public class MessagingService
    {
        public MessagingService()
        {
            var ruta = AppDomain.CurrentDomain.BaseDirectory + "/clave.json";

            if (FirebaseMessaging.DefaultInstance == null)
            {
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(ruta)
                });
            }
        }

        public void EnviarMensaje(string tipo = "Actualizar", int? id = null)
        {
            var mensaje = new Message
            {
                Topic = "Actualizacion",
                Data = new Dictionary<string, string>()
                 {
                    {"Tipo", tipo },
                    {"Id", id==null?"":id.ToString() }
                 },
                Android = new AndroidConfig { Priority = Priority.High }
            };
            FirebaseMessaging.DefaultInstance.SendAsync(mensaje);
        }
    }
}