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
        HttpDataService service;
        public List<string> LanguageList { get; set; } = new List<string>();
        public List<LanguagePair> LanguagePairs { get; set; } = new List<LanguagePair>();
        public ObservableCollection<string> fromLanguages { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> toLanguages { get; set; } = new ObservableCollection<string>();
        public string _outputWord;
        public ObservableCollection<Tr> Translations { get; set; } = new ObservableCollection<Tr>();
        public string OutputWord
        {
            get { return _outputWord; }
            set { SetProperty(ref _outputWord, value); }
        }

        public string _selectedFromLang;
        public string SelectedFromLang
        {
            get { return _selectedFromLang; }
            set { SetProperty(ref _selectedFromLang, value); }
        }
        public MainViewModel()
        {
            service = new HttpDataService("https://dictionary.yandex.net/api/v1/dicservice.json");
        }

        public override async void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            
            LanguageList = await service.GetListOfLanguages();
            foreach (var s in LanguageList)
            {
                var splitted = s.Split('-');
                LanguagePairs.Add(new LanguagePair(splitted[0], splitted[1]));
            }

            foreach(var pair in LanguagePairs)
            {
                if(!fromLanguages.Contains(pair.From)) fromLanguages.Add(pair.From);
                if(!toLanguages.Contains(pair.To)) toLanguages.Add(pair.To);
            }
            SelectedFromLang = "en";
            base.OnNavigatedTo(e, viewModelState);
        }

        internal async void Translate(string fromLang, string toLang, string input)
        {
            if (string.IsNullOrEmpty(input)) return;
            var lookup = await service.LookupTranslation($"{fromLang}-{toLang}", input);
            if (lookup.def.Count == 0) return;
            Translations.Clear();
            foreach(var tr in lookup.def[0].tr)
            {
                Translations.Add(tr);
            }
            //OutputWord = lookup.def[0].tr[0].text;
        }

        public void changeLists(string fromLang)
        {
            toLanguages.Clear();
            foreach (var pair in LanguagePairs)
            {   
                if(pair.From == fromLang && pair.To != fromLang)
                {
                    if (!toLanguages.Contains(pair.To)) toLanguages.Add(pair.To);
                }
            }
        }


    }
}
