using System;
using System.Collections.Generic;
using System.Text;

namespace DictionaryApp.Core.Models
{
    public class LanguagePair
    {
        public LanguagePair(string from, string to)
        {
            this.From = from;
            this.To = to;
        }
        public string From { get; set; }
        public string To { get; set; }
    }
}
