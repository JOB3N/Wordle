using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Wordle.Class;
using System.IO;
using System.Xml.Linq;

namespace Wordle
{
    public class CWordList
    {
        private readonly ISaveList<List<Word>> _serializer = new JsonWordSaver();
        List<Word> words;

        public CWordList()
        {
            words = new List<Word>();
        }
        public void addWord(string original, string translation, string category, int typeIndex)
        {
            switch (typeIndex)
            {
                case 3:
                    words.Add(new ColorWord(original, translation, category));
                    break;
                case 2:
                    words.Add(new VerbWord(original, translation, category));
                    break;
                case 1:
                    words.Add(new FruitWord(original, translation, category));
                    break;
                case 0:
                    words.Add(new AnimalWord(original, translation, category));
                    break;
            }
        }
        public void saveToJson(string path)
        {
            _serializer.Save(words, path);
        }
        public void loadFromJson(string path)
        {
            words = _serializer.Load(path);
        }
        public Word getWordByTrans(string trans)
        {
            foreach (Word word in words)
            {
                if (trans == word.translation)
                {
                    return word;
                }
            }
            return null;
        }
        public Word getWordByOriginal(string orig)
        {
            foreach (Word word in words)
            {
                if (orig == word.original)
                {
                    return word;
                }
            }
            return null;
        }
        public int Count()
        {
            return words.Count;
        }
        public Word GetWordByCategory(string cat) 
        {
            foreach (Word word in words)
            {
                if (cat == word.category)
                {
                    return word;
                }
            }
            return null;
        }
        public List<Word> GetWords() { return words; }

        public void deleteWordByTranslation(string trans)
        {
            string n = "";
            foreach (Word word in words)
            {
                if (trans == word.translation)
                {
                    n = word.translation;
                }
            }
            words.Remove(getWordByTrans(n));
        }
        public List<Word> GetWordsByCategory(string category)
        {
            List<Word> filteredWords = words.FindAll(word => word.category.Equals(category, StringComparison.OrdinalIgnoreCase));
            return filteredWords;
        }
        public Word GetWordByIndexAndCategory(int index, string category)
        {
            List<Word> filteredWords = GetWordsByCategory(category);
            if (index < 0 || index >= filteredWords.Count())
            {
                throw new IndexOutOfRangeException("Индекс вне диапазона списка слов данной категории.");
            }
            return filteredWords[index];
        }
        public int CountWordsByCategory(string category)
        {
            return GetWordsByCategory(category).Count;
        }
    }
}
