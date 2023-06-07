using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Rich_Text_Editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string allText = "";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void FileDialogButton_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog = new();
            openFileDialog.FileName = FileDialogTextBox.Text;
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                var str = File.ReadAllText(openFileDialog.FileName);
                MainTextBox.Text = str;
                FileDialogTextBox.Text = openFileDialog.FileName;
            }
        }

        private void CutButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(MainTextBox.SelectedText))
            {
                Clipboard.SetText(MainTextBox.SelectedText);
                MainTextBox.SelectedText = string.Empty;
            }
        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(MainTextBox.SelectedText))
            {
                Clipboard.SetText(MainTextBox.SelectedText);
            }
        }
        private void PasteButton_Click(object sender, RoutedEventArgs e)
        {
            string clipboardText = Clipboard.GetText();
            int caretIndex = MainTextBox.CaretIndex;

            MainTextBox.Text = MainTextBox.Text.Insert(caretIndex, clipboardText);
            MainTextBox.CaretIndex = caretIndex + clipboardText.Length;
        }

        private void SelectAllButton_Click(object sender, RoutedEventArgs e)
        {
            MainTextBox.SelectAll();
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog= new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog()==true)
            {
                File.WriteAllText(saveFileDialog.FileName,allText);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            allText = MainTextBox.Text;
        }

        private void AutoSaveCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            SaveButton.IsEnabled = false;
            allText = MainTextBox.Text;
        }

        private void AutoSaveCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            SaveButton.IsEnabled = true;
        }

        private void MainTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(AutoSaveCheckBox.IsChecked==true)
            {
                allText=MainTextBox.Text;
            }
        }
    }
}
