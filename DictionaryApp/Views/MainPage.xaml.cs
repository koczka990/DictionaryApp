using System;

using DictionaryApp.ViewModels;

using Windows.UI.Xaml.Controls;

namespace DictionaryApp.Views
{
    public sealed partial class MainPage : Page
    {
        private MainViewModel ViewModel => DataContext as MainViewModel;
        public string selectedItem = "";
        public MainPage()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Listener for the combobox containing input languages.
        /// when the selection is changed it calls the changeList function in the viewmodel.
        /// </summary>
        private void FromLangCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            selectedItem = comboBox.SelectedItem.ToString();
            ViewModel.changeLists(selectedItem);
            ToLangComboBox.SelectedIndex = 0;
            //SynTextBlock.Text = selectedItem;
        }

        /// <summary>
        /// Listener for the translate button.
        /// when its clicked the app translates the input word from the input language to the specified output language.
        /// </summary>
        private void Translate_Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var input = InputTextBox.Text;
            var fromLang = FromLangComboBox.SelectedItem.ToString();
            var toLang = ToLangComboBox.SelectedItem.ToString();
            if (String.IsNullOrEmpty(fromLang) || String.IsNullOrEmpty(toLang)) return;
            ViewModel.Translate(fromLang, toLang, input);
        }

        /// <summary>
        /// Listener for sysnonims button
        /// when it is clicked it gets all the synonims for the input word written in the input language
        /// </summary>
        private void Synonims_Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var input = InputTextBox.Text;
            var fromLang = FromLangComboBox.SelectedItem.ToString();
            if (String.IsNullOrEmpty(fromLang)) return;
            ViewModel.GetSynonims(fromLang, input);
        }
    }
}
