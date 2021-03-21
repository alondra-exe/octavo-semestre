using juego.Models;
using juego.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace juego.ViewModels
{
    public class ProgresoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public ObservableCollection<Progreso> Reportes { get; set; }
        public Command VerReportesCommand { get; set; }

        private bool isrunning;
        public bool IsRunning
        {
            get { return isrunning; }
            set { isrunning = value; OnPropertyChanged("IsRunning"); }
        }

        private bool isvisible;
        public bool SinConexion
        {
            get { return isvisible; }
            set { isvisible = value; OnPropertyChanged("SinConexion"); }
        }

        private Progreso reporte;
        public Progreso Reporte
        {
            get { return reporte; }
            set { reporte = value; OnPropertyChanged("Reporte"); }
        }

        ReporteRepository repos;
        public ProgresoViewModel()
        {
            repos = new ReporteRepository();
            Reportes = repos.Reportes;
            VerReportesCommand = new Command<Progreso>(VerReportes);
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (Reporte != null && Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                SinConexion = false;
                IsRunning = true;
                var result=await repos.
            }
        }

        Views.ProgresoView rv;
        private async void VerReportes(Progreso p)
        {
            if (rv == null)
            {
                rv = new Views.ProgresoView();
                rv.BindingContext = this;
            }
            Reporte = p;
            await App.Current.MainPage.Navigation.PushAsync(rv);
            if (Connectivity.NetworkAccess==NetworkAccess.Internet)
            {
                SinConexion = false;
                IsRunning = true;
                var result=await repos.
            }
        }
    }
}