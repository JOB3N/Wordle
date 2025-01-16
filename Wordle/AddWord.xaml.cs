using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wordle.Class;

namespace Wordle
{
    /// <summary>
    /// Логика взаимодействия для AddWord.xaml
    /// </summary>
    public partial class AddWord : Window
    {
        CWordList words = new CWordList();
        public event EventHandler<CloseEventArgs> ClosingEvent;

        public AddWord()
        {
            InitializeComponent();
            if (File.Exists("words.json"))
            {
                string path = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + @"\words.json"; ;
                words.loadFromJson(path);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string original = OrigWord.Text;
            string translation = TranslationWord.Text;
            string category = Category.Text;
            int typeIndex = Category.SelectedIndex;

            if (!string.IsNullOrWhiteSpace(original) && !string.IsNullOrWhiteSpace(translation))
            {
                words.addWord(original, translation, category, typeIndex);
                InfoText.Text = "Слово добавлено в список";
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = "words";
            dlg.DefaultExt = ".json";
            dlg.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                string filePath = dlg.FileName;
                words.saveToJson(filePath);
            }
            InfoText.Text = "Cписок cохранен";
        }
    }
}
