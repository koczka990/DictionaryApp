using System;
using DictionaryApp.Core.Services;
using DictionaryApp.Core.Models;
using Prism.Windows.Mvvm;

namespace DictionaryApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        const string API_KEY = "dict.1.1.20220505T150713Z.6f6ae4bb7c0ea354.0731fdcbff1249c030cd7ea0e953956bd23ea0c3";
        public MainViewModel()
        {
            HttpDataService service = new HttpDataService("https://dictionary.yandex.net/api/v1/dicservice.json");
            var response = service.GetAsync<LanguageList>("getLangs", API_KEY);

            
            
        }

        
    }
}
