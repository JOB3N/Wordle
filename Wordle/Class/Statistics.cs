using System.Xml;
using System.IO;
using System.Text.Json;

namespace Wordle.Class
{
    public class Statistics
    {
        Statistics statistic;
        public int correctAnswers { get; set; }
        public int incorrectAnswers { get; set; }
        public Statistics()
        {
            correctAnswers = 0;
            incorrectAnswers = 0;
        }
        
        
    }
}
