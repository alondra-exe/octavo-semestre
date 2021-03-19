using juego.Models;
using juego.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace juego.ViewModels
{
    public class ProgresoViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Progreso> Reportes { get; set; }
        public Command VerRecordsCommand { get; set; }
        ReporteRepository repos = new ReporteRepository();
        public ProgresoViewModel()
        {
            repos = new ReporteRepository();
            Reportes = repos.Datos;
            VerRecordsCommand = new Command(VerRecords);
        }

        private void VerRecords(object obj)
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}