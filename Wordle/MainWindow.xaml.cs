using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wordle.Class;
using System.IO;
using Microsoft.Win32;
using System.Collections;
using System.Diagnostics;

namespace Wordle
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CWordList words = new CWordList();
        Statistics statistic;
        string correctAnswer;
        public MainWindow()
        {
            InitializeComponent();

            statistic = LoadStatistics();

            if (File.Exists("words.json"))
            {
                string path = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + @"\words.json"; ;
                words.loadFromJson(path);
                LoadRadioButtons(words);
            }
        }
        public Statistics LoadStatistics()
        {
            if (File.Exists("statistics.json"))
            {
                string json = File.ReadAllText("statistics.json");
                statistic = JsonSerializer.Deserialize<Statistics>(json);
            }
            else
            {
                statistic = new Statistics();
                SaveStatistics();
            }
            return statistic;
        }
        public void SaveStatistics()
        {
            string json = JsonSerializer.Serialize(statistic);
            File.WriteAllText("statistics.json", json);
        }

        public void LoadRadioButtons(CWordList words)
        {
            Statistic.Text = "Правильные ответы: " + statistic.correctAnswers.ToString() + "   Неправильные ответы: " + statistic.incorrectAnswers.ToString();
            List<Word> animal = new List<Word>();
            Random random = new Random();
            string cat = Category.Text;

            if (cat == "Животные")
                for (int i = 0; i < words.CountWordsByCategory(cat); i++) animal.Add(words.GetWordByIndexAndCategory(i, cat));
            if (cat == "Фрукты")
                for (int i = 0; i < words.CountWordsByCategory(cat); i++) animal.Add(words.GetWordByIndexAndCategory(i, cat));
            if (cat == "Глаголы")
                for (int i = 0; i < words.CountWordsByCategory(cat); i++) animal.Add(words.GetWordByIndexAndCategory(i, cat));
            if (cat == "Цвета")
                for (int i = 0; i < words.CountWordsByCategory(cat); i++) animal.Add(words.GetWordByIndexAndCategory(i, cat));


            correctAnswer = animal[random.Next(animal.Count())].translation;
            OrigWord.Text = words.getWordByTrans(correctAnswer).original;
            // Создаем список для вариантов ответов
            List<string> options = new List<string> { correctAnswer };
            animal.Remove(words.getWordByTrans(correctAnswer));

            // Добавляем три случайных неправильных варианта

            while (options.Count < 4)
            {
                Random rnd = new Random();
                string randomWord = animal[rnd.Next(animal.Count())].translation;

                if (!options.Contains(randomWord))
                {
                    options.Add(randomWord);
                    animal.Remove(words.getWordByTrans(randomWord));
                }
            }

            // Перемешиваем варианты ответов
            options = options.OrderBy(x => random.Next()).ToList();

            // Заполняем радиокнопки
            radioButton1.Content = options[0];
            radioButton2.Content = options[1];
            radioButton3.Content = options[2];
            radioButton4.Content = options[3];

        }

        private void buttonCheck_Click(object sender, EventArgs e)
        {
            // Проверяем, какой вариант выбран
            string selectedAnswer = "";

            if (radioButton1.IsChecked == true)
            {
                selectedAnswer = radioButton1.Content.ToString();
                radioButton1.IsChecked = false;
            }
            else if (radioButton2.IsChecked == true)
            {
                selectedAnswer = radioButton2.Content.ToString();
                radioButton2.IsChecked = false;
            }
            else if (radioButton3.IsChecked == true)
            {
                selectedAnswer = radioButton3.Content.ToString();
                radioButton3.IsChecked = false;
            }
            else if (radioButton4.IsChecked == true)
            {
                selectedAnswer = radioButton4.Content.ToString();
                radioButton4.IsChecked = false;
            }

            // Проверяем правильность ответа
            if (selectedAnswer == correctAnswer)
            {
                statistic.correctAnswers += 1;
            }
            else
            {
                statistic.incorrectAnswers += 1;
            }
            SaveStatistics();
            // Обновляем радиокнопки с новым правильным ответом
            LoadRadioButtons(words);
            
        }
        private void AddWordButton(object sender, RoutedEventArgs e)
        {
            AddWord add_word = new AddWord(this);
            add_word.ShowDialog();
                 
        }
    }
}
