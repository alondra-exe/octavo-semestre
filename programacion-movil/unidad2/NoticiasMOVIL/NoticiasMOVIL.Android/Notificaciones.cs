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

[assembly:Xamarin.Forms.Dependency(typeof(NoticiasMOVIL.Droid.Notificaciones))]

namespace NoticiasMOVIL.Droid
{
    public class Notificaciones:INotificaciones
    {
        public void Notificar(string mensaje)
        {
            NotificationChannel channel = new NotificationChannel("Noti1", "PRUBEANOTIFICACION", NotificationImportance.High);
            channel.LockscreenVisibility = NotificationVisibility.Public;

            NotificationManager manager = (NotificationManager)Application.Context.GetSystemService(Application.NotificationService);
            manager.CreateNotificationChannel(channel);

            NotificationCompat.Builder builder = new NotificationCompat.Builder(Application.Context, "Noti1");
            builder.SetContentText(mensaje);
            builder.SetContentTitle("Prueba de notificaciones");
            builder.SetSmallIcon(Resource.Drawable.notification_tile_bg);
            builder.SetPriority((int)NotificationPriority.Max);
            builder.SetDefaults((int)NotificationDefaults.All);

            manager.Notify(1, builder.Build());
        }
    }
}