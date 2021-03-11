using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;
using pokedex.models;

namespace pokedex.models.viewModels
{
    public class pokedexViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Pokemon> Pokemons { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}