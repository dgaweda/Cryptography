using System;
using System.IO;

namespace vigenere
{
    class vigenere
    {
        table Table = new table();
        string abc = ("abcdefghijklmnopqrstuvwxyz");

        public void prepare()
        {
            Console.WriteLine("Przygotowywanie tekstu...");

            File.WriteAllText("plain.txt", null);

            string orig = File.ReadAllText("orig.txt");
            char[] prepared_orig = new char[orig.Length];
            int i = 0;

            foreach (var character in orig)
                if (((int)character >= 65 && (int)character <= 90) || ((int)character >= 97 && (int)character <= 122))
                {
                    prepared_orig[i] = character;
                    File.AppendAllText("plain.txt", (prepared_orig[i].ToString()).ToLower());
                }
            i++;

            Console.WriteLine("Przygotowywanie tekstu zakonczone.");
        }

        public void encrypt()
        {
            Console.WriteLine("Szyfrowanie...");

            File.WriteAllText("crypto.txt", null);

            string plain = File.ReadAllText("plain.txt");
            string key = File.ReadAllText("key.txt");
            char[] plainTransformByKey = new char[plain.Length];
            int k = 0;
            int i = 0;
            int x = 0;
            int y = 0;

            while (k < plain.Length)
            {
                plainTransformByKey[k] = key[k % key.Length];
                k++;
            }

            while (i < plain.Length)
            {
                for (var character = 0; character < abc.Length; character++)
                {
                    if (abc[character].Equals(plain[i]))
                        x = character;
                    if (abc[character].Equals(plainTransformByKey[i]))
                        y = character;

                }
                File.AppendAllText("crypto.txt", Table.vigenere_Table[x, y]);
                i++;
            }

            Console.WriteLine("Szyfrowanie ukonczone.");
        }

        public void decrypt()
        {
            Console.WriteLine("Odszyfrowuje kryptogram ... Prosze czekac.");

            File.WriteAllText("decrypt.txt", null);

            string crypt = File.ReadAllText("crypto.txt");
            string key = File.ReadAllText("key.txt");
            char[] cryptTransformByKey = new char[crypt.Length];
            char[] TransformedKey = new char[key.Length];
            int k = 0;
            int i = 0;
            int x = 0;
            int y = 0;
            // odwrocenie klucza
            while (i < key.Length)
            {
                for (var j = 0; j < 26; j++)
                {
                    if (key[i].Equals(abc[j]))
                    {
                        TransformedKey[i] = abc[(26 - j) % 26];
                    }
                }
                i++;
            }

            while (k < crypt.Length)
            {
                cryptTransformByKey[k] = TransformedKey[k % key.Length];
                k++;
            }

            i = 0;
            while (i < crypt.Length)
            {
                for (var character = 0; character < abc.Length; character++)
                {
                    if (abc[character].Equals(crypt[i]))
                        x = character;
                    if (abc[character].Equals(cryptTransformByKey[i]))
                        y = character;

                }
                File.AppendAllText("decrypt.txt", Table.vigenere_Table[x, y]);
                i++;
            }
            Console.WriteLine("Odszyfrowanie zakonczone.");
        }

        public void analize()
        {
            Console.WriteLine("Kryptoanaliza nie dziala.");
        }
    }

}