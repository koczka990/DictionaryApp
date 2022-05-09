using System;
using System.Collections.Generic;
using System.Text;

namespace DictionaryApp.Core.Models
{
    /// <summary>
    /// Containse two strings an input and an output language
    /// </summary>
    public class LanguagePair
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="from">input language</param>
        /// <param name="to">output language</param>
        public LanguagePair(string from, string to)
        {
            this.From = from;
            this.To = to;
        }
        public string From { get; set; }
        public string To { get; set; }
    }
}
