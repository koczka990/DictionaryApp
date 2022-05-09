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
        HttpDataService service;
        public List<string> LanguageList { get; set; } = new List<string>();
        public List<LanguagePair> LanguagePairs { get; set; } = new List<LanguagePair>();
        public ObservableCollection<string> fromLanguages { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> toLanguages { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<Def> Entries { get; set; } = new ObservableCollection<Def>();
        

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

        /// <summary>
        /// Loading list of available languages into the app
        /// </summary>
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

        /// <summary>
        /// translation function which looks up the translation for the specified word from API. The input is in the fromLang language, the output is in the toLang language.
        /// </summary>
        /// <param name="fromLang">language of the input word</param>
        /// <param name="toLang">target language, which we want to get the translation in</param>
        /// <param name="input">the word for translation</param>
        internal async void Translate(string fromLang, string toLang, string input)
        {
            if (string.IsNullOrEmpty(input)) return;
            var lookup = await service.LookupTranslation($"{fromLang}-{toLang}", input);
            if (lookup.def.Count == 0) return;
            Entries.Clear();
            foreach(var d in lookup.def)
            {
                Entries.Add(d);
            }
        }

        /// <summary>
        /// Get sysnonims for a specified word from API.
        /// </summary>
        /// <param name="fromLang">Language which the input word is written in.</param>
        /// <param name="input">Input word for getting sysnonims for</param>
        internal async void GetSynonims(string fromLang, string input)
        {
            if (string.IsNullOrEmpty(input)) return;
            if (!LanguageList.Contains($"{fromLang}-{fromLang}")) return;
            var lookup = await service.LookupTranslation($"{fromLang}-{fromLang}", input);
            if (lookup.def.Count == 0) return;
            Entries.Clear();
            foreach (var d in lookup.def)
            {
                Entries.Add(d);
            }
        }

        /// <summary>
        /// Changes the content of tolanguages (combobox), according to an input language.
        /// this is required so the user can not pick language pairs which are not included in the API.
        /// </summary>
        /// <param name="fromLang">The new input language</param>
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
