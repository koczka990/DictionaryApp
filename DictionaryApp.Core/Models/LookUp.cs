using System;
using System.Collections.Generic;
using System.Text;

namespace DictionaryApp.Core.Models
{

    public class LookUp
    {
        public Head head { get; set; }
        public List<Def> def { get; set; } //array of dictionary entries
    }

    public class Head
    {
    }

    public class Def
    {
        public string text { get; set; } //text to output (same in each class)
        public string pos { get; set; } //part of speech
        public string ts { get; set; } //transcription (not needed)
        public List<Tr> tr { get; set; } //array of translations
    }

    //Translation
    public class Tr
    {
        public string text { get; set; }
        public string pos { get; set; }
        public string gen { get; set; }
        public int fr { get; set; }
        public List<Syn> syn { get; set; } //array of synonyms
        public List<Mean> mean { get; set; } //array of meanings
        public List<Ex> ex { get; set; } //array of examples
        public string asp { get; set; }
    }

    public class Syn
    {
        public string text { get; set; }
        public string pos { get; set; }
        public string gen { get; set; }
        public int fr { get; set; }
        public string asp { get; set; }
    }

    public class Mean
    {
        public string text { get; set; }
    }

    public class Ex
    {
        public string text { get; set; }
        public List<Tr1> tr { get; set; }
    }

    public class Tr1
    {
        public string text { get; set; }
    }

}
