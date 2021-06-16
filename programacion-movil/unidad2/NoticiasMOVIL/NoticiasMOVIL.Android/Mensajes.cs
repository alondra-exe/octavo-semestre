using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;

using Android.Views;
using Android.Widget;
using AndroidX.Core.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NoticiasMOVIL.Helpers;

[assembly: Xamarin.Forms.Dependency(typeof(NoticiasMOVIL.Droid.Mensajes))]
namespace NoticiasMOVIL.Droid
{
    public class Mensajes : IMensajes
    {
        public void MostrarToast(string mensaje)
        {
            Toast
                .MakeText(Application.Context, mensaje, ToastLength.Long)
                .Show();
        }

        public void Notificaciones(string mensaje)
        {
            NotificationChannel channel = new NotificationChannel("notificacion", "noticiaNotificacion",
              NotificationImportance.High);
            channel.LockscreenVisibility = NotificationVisibility.Public;
            NotificationManager manager = (NotificationManager)Application.Context.GetSystemService(Application.NotificationService);

            manager.CreateNotificationChannel(channel);


            NotificationCompat.Builder builder = new NotificationCompat.Builder(Application.Context, "notificacion");
            builder.SetContentText(mensaje);
            builder.SetContentTitle("Noticias App");
            builder.SetPriority((int)NotificationPriority.Max);
            builder.SetDefaults((int)NotificationDefaults.All);

            manager.Notify(1, builder.Build());
        }
    }
}