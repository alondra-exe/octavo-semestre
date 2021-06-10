using System;

using Android.App;
using Android.Content;
using Android.Util;
using Firebase.Messaging;
using Xamarin.Essentials;
using NoticiasMOVIL.Services;

namespace NoticiasMOVIL.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        const string TAG = "MyFirebaseMsgService";
        public override void OnMessageReceived(RemoteMessage message)
        {
            Log.Debug("PRUEBAMENSAJES", "RECIBI EL MENSAJE");

            if (App.Current != null)
            {
                var data = message.Data;
                if (data["Tipo"] == "Actualizar")
                {
                    _ = App.Descargar();
                }
                else
                {
                    Repositories.NoticiasRepository repos = new Repositories.NoticiasRepository();
                    var noticia = repos.Get(int.Parse(data["Id"]));
                    if (noticia != null)
                    {
                        repos.Delete(noticia);
                        _ = App.Descargar();
                    }
                }
            }
            else
            {
                var data = message.Data;
                if (data["Tipo"] == "Actualizar")
                {
                    HttpNoticiasService noticias = new HttpNoticiasService();
                    var resultado = noticias.DescargarNoticias();
                    resultado.Wait();

                    if (resultado.Result)
                    {

                    }
                }
                else
                {
                    Repositories.NoticiasRepository repos = new Repositories.NoticiasRepository();
                    var noticia = repos.Get(int.Parse(data["Id"]));
                    if (noticia != null)
                    {
                        repos.Delete(noticia);
                    }
                }
            }
        }
    }
}