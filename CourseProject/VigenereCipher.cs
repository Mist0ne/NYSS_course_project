using System;
using System.Linq;

namespace CourseProject
{
    public class VigenereCipher
    {
        const string defaultAlphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        // Actual alphabet for object
        readonly string letters;

        // Constructor setting the value of the alphabet to encrypt (set new alphabet or using default)
        public VigenereCipher(string alphabet = null)
        {
            letters = string.IsNullOrEmpty(alphabet) ? defaultAlphabet : alphabet;
        }

        // Generating a duplicate password
        private string GetRepeatKey(string s, int n)
        {
            var p = s;
            while (p.Length < n)
            {
                p += p;
            }
            // If the password is larger than the required one, it will be truncated to the required size
            return p.Substring(0, n);
        }


        private string Vigenere(string text, string password, bool encrypting = true)
        {
            /*
             * Saving text in its original form
             * This is required to insert characters outside the alphabet
             * And for keeping the register
             */
            var oldFullText = text;

            // Remove all unnecessary characters from the text
            var temp = text.ToUpper().ToList();
            temp.RemoveAll(x => !letters.Contains(x));
            text = new string(temp.ToArray());

            // Our key for cipher
            var gamma = GetRepeatKey(password.ToUpper(), text.Length);
            // Value for returning
            var retValue = "";
            // Lenth of alphabet (default (for russian alphabet) = 33)
            var q = letters.Length;

            // Tracking a current character against the old (full text)
            int j = 0;

            for (int i = 0; i < text.Length; i++)
            {
                var letterIndex = letters.IndexOf(text[i]);
                var codeIndex = letters.IndexOf(gamma[i]);

                /* 
                * If met a character outside the alphabet
                * Add all the elements in their original form until we reach the end 
                * Or to a new character from the alphabet
                */
                while (!letters.Contains(oldFullText[j].ToString().ToUpper()) && j < oldFullText.Length)
                {
                    retValue += oldFullText[j];
                    j++;
                }

                /*
                * Boolean 'encrypting' taken into a method when called
                * 
                * When decoding, you get the formula like this:
                * Decoded symbol = (q(lenth of alphabet) + letterIndex(Index of encrypted symbol) - codeIndex(Index of symbol from repeated key)) mod q
                * You can imagine how: Pi = (Ci + N - Ki) % N
                * 
                * When encoding, you get the formula like this:
                * Encoded symbol = (q(lenth of alphabet) + letterIndex(Index of encrypted symbol) + codeIndex(Index of symbol from repeated key)) mod q
                * You can imagine how: Ci = (Pi + Ki) % N
                * 
                * Where: 
                * Ci - encoded message symbol
                * Pi - original message character
                * Ki - key symbol
                * N - power of the alphabet (number of characters in the alphabet)
                */
                var addElem = letters[(q + letterIndex + ((encrypting ? 1 : -1) * codeIndex)) % q].ToString();
                
                /*
                 * If the letter was in lower case, then add the changed character in lower case too
                 * and vice versa.
                 */
                retValue += Char.IsLower(oldFullText[j])
                    ? addElem.ToLower()
                    : addElem.ToUpper();

                // Continue to move along the original text
                j++;
            }

            // If there are non-alphabetical characters at the end, add them
            for (int i = j; i < oldFullText.Length; i++)
            {
                retValue += oldFullText[i];
            }

            return retValue;
        }

        // Encrypting the text
        public string Encrypt(string plainMessage, string password)
            => Vigenere(plainMessage, password);

        // Decrypting the text
        public string Decrypt(string encryptedMessage, string password)
            => Vigenere(encryptedMessage, password, false);
    }
}