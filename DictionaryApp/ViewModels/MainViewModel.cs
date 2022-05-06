using System;
using DictionaryApp.Core.Services;
using DictionaryApp.Core.Models;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DictionaryApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        const string API_KEY = "dict.1.1.20220505T150713Z.6f6ae4bb7c0ea354.0731fdcbff1249c030cd7ea0e953956bd23ea0c3";
        public List<string> LanguageList { get; set; } = new List<string>();
        public ObservableCollection<string> fromLanguages { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> toLanguages { get; set; } = new ObservableCollection<string>();
        public MainViewModel()
        {

        }

        public override async void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            HttpDataService service = new HttpDataService("https://dictionary.yandex.net/api/v1/dicservice.json");
            LanguageList = await service.GetListOfLanguages();
            foreach (var pair in LanguageList)
            {
                var splitted = pair.Split('-');
                fromLanguages.Add(splitted[0]);
                toLanguages.Add(splitted[1]);
            }


            base.OnNavigatedTo(e, viewModelState);
        }


    }
}
