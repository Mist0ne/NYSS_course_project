using System;
using System.Linq;

namespace CourseProject
{
    public class VigenereCipher
    {
        const string defaultAlphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        readonly string letters;

        public VigenereCipher(string alphabet = null)
        {
            letters = string.IsNullOrEmpty(alphabet) ? defaultAlphabet : alphabet;
        }

        //генерация повторяющегося пароля
        private string GetRepeatKey(string s, int n)
        {
            var p = s;
            while (p.Length < n)
            {
                p += p;
            }

            return p.Substring(0, n);
        }


        private string Vigenere(string text, string password, bool encrypting = true)
        {
            var oldFullText = text;
            var temp = text.ToUpper().ToList();
            temp.RemoveAll(x => !letters.Contains(x));
            text = new string(temp.ToArray());

            var gamma = GetRepeatKey(password.ToUpper(), text.Length);
            var retValue = "";
            var q = letters.Length;

            int j = 0;

            for (int i = 0; i < text.Length; i++)
            {
                var letterIndex = letters.IndexOf(text[i]);
                var codeIndex = letters.IndexOf(gamma[i]);

                var qwerty = text[i];

                while (!letters.Contains(oldFullText[j].ToString().ToUpper()))
                {
                    retValue += oldFullText[j];
                    j++;
                }

                if (Char.IsLower(oldFullText[j]))
                {
                    retValue += letters[(q + letterIndex + ((encrypting ? 1 : -1) * codeIndex)) % q].ToString().ToLower();
                }
                else
                {
                    retValue += letters[(q + letterIndex + ((encrypting ? 1 : -1) * codeIndex)) % q].ToString().ToUpper();
                }

                j++;
            }

            for (int i = j; i < oldFullText.Length; i++)
            {
                retValue += oldFullText[i];
            }

            return retValue;
        }

        //шифрование текста
        public string Encrypt(string plainMessage, string password)
            => Vigenere(plainMessage, password);

        //дешифрование текста
        public string Decrypt(string encryptedMessage, string password)
            => Vigenere(encryptedMessage, password, false);
    }
}