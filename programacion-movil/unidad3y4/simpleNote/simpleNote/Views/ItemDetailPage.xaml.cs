using System.ComponentModel;
using Xamarin.Forms;
using simpleNote.ViewModels;

namespace simpleNote.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}