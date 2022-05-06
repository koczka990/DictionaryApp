using System;

using DictionaryApp.ViewModels;

using Windows.UI.Xaml.Controls;

namespace DictionaryApp.Views
{
    public sealed partial class MainPage : Page
    {
        private MainViewModel ViewModel => DataContext as MainViewModel;

        public MainPage()
        {
            InitializeComponent();
        }
    }
}
