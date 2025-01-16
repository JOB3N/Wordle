using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Wordle.Class
{
    public class FruitWord : Word
    {
        public string original { get; set; }
        public string translation { get; set; }
        public string categor { get; set; }
        public FruitWord(string original, string translation, string categor) : base(original, translation, categor)
        {
            this.original = original;
            this.translation = translation;
            this.categor = categor;
        }
    }
}
