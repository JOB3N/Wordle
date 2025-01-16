using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordle
{
    public class CloseEventArgs : EventArgs
    {
        public bool SaveChanges { get; set; }

        public CloseEventArgs(bool saveChanges)
        {
            SaveChanges = saveChanges;
        }
    }
}
