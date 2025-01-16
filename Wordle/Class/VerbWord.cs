using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordle.Class
{
    public class VerbWord : Word
    {
        public string original { get; set; }
        public string translation { get; set; }
        public string categor { get; set; }
        public VerbWord(string original, string translation, string categor) : base(original, translation, categor)
        {
            this.original = original;
            this.translation = translation;
            this.categor = categor;
        }
    }
}
