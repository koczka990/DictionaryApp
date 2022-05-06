using System;
using DictionaryApp.Core.Services;
using DictionaryApp.Core.Models;
using Prism.Windows.Mvvm;

namespace DictionaryApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            HttpDataService service = new HttpDataService("https://dictionary.yandex.net/api/v1/dicservice.json");
            var response = await service.GetAsync<LanguageList>();
        }

        public override async Task ()
        {

        }
    }
}
