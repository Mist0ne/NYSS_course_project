using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace CourseProject
{
    public static class FileSaver
    {
        public static bool SaveFile(string text)
        {
            try
            {
                if (text.Trim().Length == 0)
                {
                    MessageBox.Show("You have chosen to keep an empty file. Well, this is the choice of a real samurai.");
                }
                SaveFileDialog saveFileDialog = new SaveFileDialog();

                // Add filters for using only .txt files
                saveFileDialog.Filter = "txt files (*.txt)|*.txt";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == true)
                {
                    File.WriteAllText(saveFileDialog.FileName, text);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                // Like writing logs xD
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}