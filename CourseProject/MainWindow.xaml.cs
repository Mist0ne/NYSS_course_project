using System;
using System.IO;
using System.Text;
using System.Windows;
using Microsoft.Win32;

namespace CourseProject
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void ChoseNewEncryptedFile(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Text files (*.txt)|*.txt";
                if (openFileDialog.ShowDialog() == true)
                {
                    // Trying to get data from files two ways (ANSI and UTF)
                    var text = File.ReadAllText(openFileDialog.FileName);
                    if (text.Contains("�"))
                        text = File.ReadAllText(openFileDialog.FileName, Encoding.GetEncoding(1251));
                    encryptedText.Text = text;
                    decryptedText.Text = "";
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                MessageBox.Show("Something went wrong. Don't worry, you can try touch me again :-)");
            }
        }

        private void ChoseNewDecryptedFile(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Text files (*.txt)|*.txt";
                if (openFileDialog.ShowDialog() == true)
                {
                    // Trying to get data from files two ways (ANSI and UTF)
                    var text = File.ReadAllText(openFileDialog.FileName);
                    if (text.Contains("�"))
                        text = File.ReadAllText(openFileDialog.FileName, Encoding.GetEncoding(1251));
                    decryptedText.Text = text;
                    encryptedText.Text = "";
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                MessageBox.Show("Something went wrong. Don't worry, you can try touch me again :-)");
            }
        }

        private void SaveEncryptedText(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(FileSaver.SaveFile(encryptedText.Text)
                ? "Well done! I can Save your encrypted data"
                : "Oops. Something went wrong while I tried to save your encrypted data. Please, try again");
        }

        private void SaveDecryptedText(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(FileSaver.SaveFile(decryptedText.Text)
                ? "Well done! I can Save your decrypted data"
                : "Oops. Something went wrong while I tried to save your decrypted data. Please, try again");
        }

        private void EncryptText(object sender, RoutedEventArgs e)
        {
            try
            {
                if (decryptedText.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Please, fill in the (decrypted text) field. It is empty(");
                }
                else if (codePhrase.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Please, fill in the (code phrase) field. It is empty(");
                }
                else
                {
                    VigenereCipher cipher = new VigenereCipher();
                    var encryptText = cipher.Encrypt(decryptedText.Text, codePhrase.Text);
                    encryptedText.Text = encryptText;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                MessageBox.Show("Something went wrong. Don't worry, you can try touch me again :-)");
            }
        }

        private void DecryptText(object sender, RoutedEventArgs e)
        {
            try
            {
                if (encryptedText.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Please, fill in the (encrypted text) field. It is empty(");
                }
                else if (codePhrase.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Please, fill in the (code phrase) field. It is empty(");
                }
                else
                {
                    VigenereCipher cipher = new VigenereCipher();
                    var decryptText = cipher.Decrypt(encryptedText.Text, codePhrase.Text);
                    decryptedText.Text = decryptText;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                MessageBox.Show("Something went wrong. Don't worry, you can try touch me again :-)");
            }
        }
    }
}
