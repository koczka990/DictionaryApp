using System;
using System.Collections.Generic;
using System.Text;

namespace DictionaryApp.Core.Models
{
    /// <summary>
    /// answer for translation
    /// contains all the corresponding dictionary entries
    /// </summary>
    public class LookUp
    {
        public Head head { get; set; }
        public List<Def> def { get; set; } //array of dictionary entries
    }

    //Not used
    public class Head
    {
    }

    /// <summary>
    /// Dictionary entry
    /// </summary>
    public class Def
    {
        public string text { get; set; } //text to output (same in each class)
        public string pos { get; set; } //part of speech
        public string ts { get; set; } //transcription (not needed)
        public List<Tr> tr { get; set; } //array of translations
    }

    /// <summary>
    /// Translation
    /// </summary>
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

    /// <summary>
    /// Sysnonim
    /// </summary>
    public class Syn
    {
        public string text { get; set; }
        public string pos { get; set; }
        public string gen { get; set; }
        public int fr { get; set; }
        public string asp { get; set; }
    }

    /// <summary>
    /// Meaning
    /// </summary>
    public class Mean
    {
        public string text { get; set; }
    }

    /// <summary>
    /// Example
    /// </summary>
    public class Ex
    {
        public string text { get; set; }
        public List<Tr1> tr { get; set; }
    }

    /// <summary>
    /// Translation of example
    /// </summary>
    public class Tr1
    {
        public string text { get; set; }
    }

}
