using Microsoft.VisualStudio.TestTools.UnitTesting;
using CourseProject;

namespace CourseProject_UnitTests
{
    [TestClass]
    public class VigenereCipherTest
    {
        [TestMethod]
        public void DecodingTest()
        {
            // Base information
            VigenereCipher classObject = new VigenereCipher("јЅ¬√ƒ≈®∆«»… ЋћЌќѕ–—“”‘’÷„ЎўЏџ№Ёёя");
            string codePhrase = "скорпион";


            // Single word encryption test (without characters)
            string firstEncodedStringWithOneWord = "Ѕщцфаирщри";
            string expectedStringAfterFirstDecoding = "ѕоздравл€ю";
            Assert.AreEqual(DecodingJob(classObject, firstEncodedStringWithOneWord, codePhrase, expectedStringAfterFirstDecoding), true, "Test 1 (one word): Decryption is incorrect");
            Assert.AreEqual(DecodingJob(classObject, firstEncodedStringWithOneWord, codePhrase, expectedStringAfterFirstDecoding), 
                DecodingJob(classObject, firstEncodedStringWithOneWord, codePhrase.ToUpper(), expectedStringAfterFirstDecoding), "Uppercase translation caused an error in test 1 (with one word)");


            // Single string encryption test with characters    ',' '!' ' '
            string secondEncodedStringWithCharacters = "Ѕщцфаирщри, бл €чъбиуъ щбюэс€Єш гфуаа!!! ";
            string expectedStringAfterSecondDecoding = "ѕоздравл€ю, ты получил исходный текст!!! ";
            Assert.AreEqual(DecodingJob(classObject, secondEncodedStringWithCharacters, codePhrase, expectedStringAfterSecondDecoding), true, "Test 2 (string with characters): Decryption is incorrect");
            Assert.AreEqual(DecodingJob(classObject, secondEncodedStringWithCharacters, codePhrase, expectedStringAfterSecondDecoding), 
                DecodingJob(classObject, secondEncodedStringWithCharacters, codePhrase, expectedStringAfterSecondDecoding), "Uppercase translation caused an error in test 2 (string with characters)");
        }

        public bool DecodingJob(VigenereCipher vigenereCipher, string encodeString, string rightPassword, string rightDecodeString)
        {
            return vigenereCipher.Decrypt(encodeString, rightPassword) == rightDecodeString
                ? true
                : false;
        }



        /* 
        * This is my first experience writing tests, so I tried to make them a little different
        * I hope that i correctly understood the essence of ideology
        */
        [TestMethod]
        public void EncodingTest()
        {
            // Base information
            VigenereCipher classObject = new VigenereCipher("јЅ¬√ƒ≈®∆«»… ЋћЌќѕ–—“”‘’÷„ЎўЏџ№Ёёя");
            string codePhrase = "скорпион";


            // Single word decryption test (without characters)
            string firstDecodedStringWithOneWord = "ѕоздравл€ю";
            string expectedStringAfterFirstEncoding = "Ѕщцфаирщри";

            var result = classObject.Encrypt(firstDecodedStringWithOneWord, codePhrase);
            Assert.AreEqual(result, expectedStringAfterFirstEncoding, $"Test 1 (one word): Encryption is incorrect\n\nExpected value: {expectedStringAfterFirstEncoding}\nLenth of text: {expectedStringAfterFirstEncoding.Length}\n\nReturned value: {result}\nLenth of text: {result.Length}");
            var resultWithUpperPhrase = classObject.Encrypt(firstDecodedStringWithOneWord, codePhrase.ToUpper());
            Assert.AreEqual(resultWithUpperPhrase, expectedStringAfterFirstEncoding, $"Uppercase translation caused an error in test 1 (with one word)\n\nExpected value: {expectedStringAfterFirstEncoding}\nLenth of text: {expectedStringAfterFirstEncoding.Length}\n\nReturned value: {resultWithUpperPhrase}\nLenth of text: {resultWithUpperPhrase.Length}");


            // Single string decryption test with characters    ',' '!' ' '
            string secondDecodedStringWithCharacters = "ѕоздравл€ю, ты получил исходный текст!!! ";
            string expectedStringAfterSecondEncoding = "Ѕщцфаирщри, бл €чъбиуъ щбюэс€Єш гфуаа!!! "; 

            result = classObject.Encrypt(secondDecodedStringWithCharacters, codePhrase);
            Assert.AreEqual(result, expectedStringAfterSecondEncoding, $"Test 2 (string with characters): Encryption is incorrect\n\nExpected value: {expectedStringAfterSecondEncoding}\nLenth of text: {expectedStringAfterSecondEncoding.Length}\n\nReturned value: {result}\nLenth of text: {result.Length}");
            resultWithUpperPhrase = classObject.Encrypt(secondDecodedStringWithCharacters, codePhrase.ToUpper());
            Assert.AreEqual(resultWithUpperPhrase, expectedStringAfterSecondEncoding, $"Uppercase translation caused an error in test 2 (string with characters)\n\nExpected value: {expectedStringAfterSecondEncoding}\nLenth of text: {expectedStringAfterSecondEncoding.Length}\n\nReturned value: {resultWithUpperPhrase}\nLenth of text: {resultWithUpperPhrase.Length}");
        }

        // ToDo
        // 1) Make an decoding/encoding test without alphabet characters
        // 2) Make an decoding/encoding test with multiline text
    }
}
