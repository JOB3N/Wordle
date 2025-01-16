using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Text.Json.Serialization;

namespace Wordle.Class
{
    public abstract class Word
    {
        public string category { get; set; }
        public string original { get; set; }
        public string translation { get; set; }
        

        public Word(string orig, string trans, string categor)
        {
            original = orig;
            translation = trans;
            this.category = categor;
        }


    }
}
