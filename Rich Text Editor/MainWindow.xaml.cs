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
using Xceed.Wpf.Toolkit;
using System.Windows.Forms;
using System.Drawing;

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
            SizeComboBox.ItemsSource = new string[] { "8", "9", "10", "11", "12", "14", "16", "18", "20", "22", "24", "26", "28", "36", "48", "72" };
            FontComboBox.ItemsSource = Fonts.SystemFontFamilies.Select(f => f.ToString());
            AlignmentComboBox.ItemsSource = new string[] { "Left", "Center"  ,"Right" };
            AlignmentComboBox.SelectedItem = "Left";
            SizeComboBox.SelectedItem = "11";
            FontComboBox.SelectedItem = "Segoe UI";
        }

        private void FileDialogButton_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.OpenFileDialog openFileDialog = new();
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
                System.Windows.Clipboard.SetText(MainTextBox.SelectedText);
                MainTextBox.SelectedText = string.Empty;
            }
        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(MainTextBox.SelectedText))
            {
                System.Windows.Clipboard.SetText(MainTextBox.SelectedText);
            }
        }
        private void PasteButton_Click(object sender, RoutedEventArgs e)
        {
            string clipboardText = System.Windows.Clipboard.GetText();
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
            Microsoft.Win32.SaveFileDialog saveFileDialog= new Microsoft.Win32.SaveFileDialog();
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

        private void BoldButton_Click(object sender, RoutedEventArgs e)
        {
            if(MainTextBox.FontWeight!=FontWeights.Bold) MainTextBox.FontWeight = FontWeights.Bold;
            else MainTextBox.FontWeight = FontWeights.Normal;
        }
     
        private void ItalicButton_Click(object sender, RoutedEventArgs e)
        {
            if(MainTextBox.FontStyle != FontStyles.Italic) MainTextBox.FontStyle = FontStyles.Italic;
            else MainTextBox.FontStyle = FontStyles.Normal;
        }

        private void UnderlineButton_Click(object sender, RoutedEventArgs e)
        {
            if(MainTextBox.TextDecorations != TextDecorations.Underline) MainTextBox.TextDecorations = TextDecorations.Underline;
            else MainTextBox.TextDecorations = null;
        }

        private void FontComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainTextBox.FontFamily = new(FontComboBox.SelectedItem.ToString());
        }

        private void SizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainTextBox.FontSize = Convert.ToDouble(SizeComboBox.SelectedItem);
        }

        private void BackColorPicker_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog  colorDialog = new ColorDialog();
            if(colorDialog.ShowDialog()==System.Windows.Forms.DialogResult.OK)
            {
                MainTextBox.Background = new SolidColorBrush(System.Windows.Media.
                    Color.FromRgb(colorDialog.Color.R, colorDialog.Color.G,colorDialog.Color.B));
            }
            
        }

        private void ForeColorPicker_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                MainTextBox.Foreground = new SolidColorBrush(System.Windows.Media.
                    Color.FromRgb(colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B));
            }
        }

        private void AlignmentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(AlignmentComboBox.SelectedItem=="Left")
            {
                MainTextBox.TextAlignment = TextAlignment.Left;
            }
            else if(AlignmentComboBox.SelectedItem=="Center")
            {
                MainTextBox.TextAlignment = TextAlignment.Center;
            }
            else if(AlignmentComboBox.SelectedItem=="Right")
            {
                MainTextBox.TextAlignment = TextAlignment.Right;
            }

        }
    }
}
