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
            if (App.Current != null)
            {
                var data = message.Data;
                if (data["Tipo"] == "NuevaNoticia")
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
        }
    }
}