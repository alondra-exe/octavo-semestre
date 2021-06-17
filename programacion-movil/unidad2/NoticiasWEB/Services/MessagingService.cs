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

        public void EnviarMensaje(string tipo = "NuevaNoticia", int? id = null)
        {
            var message = new Message
            {
                Topic = "general",
                Data = new Dictionary<string, string>()
                 {
                    {"Tipo", tipo },
                    {"Id", id==null?"":id.ToString() }
                 },
                Android = new AndroidConfig { Priority = Priority.High }
            };
            FirebaseMessaging.DefaultInstance.SendAsync(message);
        }

        public void EnviarNotificacion(string titulo)
        {
            var topic = "general";
            var message = new FirebaseAdmin.Messaging.Message()
            {
                Topic = topic,
                Notification = new Notification()
                { Title = "¡HAY UNA NOTICIA NUEVA, NO TE LA PIERDAS!", Body = titulo }
            };
            FirebaseMessaging.DefaultInstance.SendAsync(message);
        }
    }
}