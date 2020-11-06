using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace cezar
{
    class cezar
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("-----------------Witaj w programie----------------");
            Console.WriteLine("----Wybierz szyfr z którego chcesz skorzystać.----");
            Console.WriteLine("-----------------------WPISZ:---------------------");
            Console.WriteLine("-c - jeżeli chcesz skorzystać z szyfru cezara");
            Console.WriteLine("-a - jeżeli chcesz skorzystać z szyfru afinicznego");

            string alfabetDUZE = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string alfabetMALE = "abcdefghijklmnopqrstuvwxyz";

            string keyTXT = @"key.txt";
            string key_newTXT = @"key-new.txt";
 
            
            
            
            

            // DEKLARACJA KLUCZY //
            int cesar_KEY = 0;
            int[] afinic_KEY = { 0, 0 };
            int[] possibleAfinic_KEYS = { 1, 3, 5, 7, 9, 11, 15, 17, 19, 21, 23, 25};

            readKEY(ref cesar_KEY, ref afinic_KEY, keyTXT);

            string cryptType = Console.ReadLine();

            switch (cryptType) 
             {
                 case "-c":
                     {
                        Console.WriteLine("-e - jeżeli chcesz zaszyfrować wiadomość");
                        Console.WriteLine("-d - jeżeli chcesz rozszyfrować wiadomość");
                        Console.WriteLine("-k - kryptoanaliza oparta wyłącznie o kryptogram");
                        Console.WriteLine("-j - kryptoanaliza oparta o tekst zaszyfrowany i tekst pomocniczy");
                        string codeAction1 = Console.ReadLine();
                         switch (codeAction1)
                         {
                            case "-e":
                                {
                                    crypt(alfabetMALE, alfabetDUZE, cesar_KEY);
                                    break;
                                }
                            case "-d":
                                {
                                    decrypt(alfabetMALE, alfabetDUZE, cesar_KEY);
                                    break;
                                }
                            case "-j": 
                                {
                                    AnalizeWithPlainTXT(alfabetMALE, alfabetDUZE, cesar_KEY);
                                    readKEY(ref cesar_KEY, ref afinic_KEY, key_newTXT);
                                    decrypt(alfabetMALE, alfabetDUZE, cesar_KEY);
                                    break;
                                }
                            case "-k":
                                {
                                    cryptoAnalize_WITHOUTplain(alfabetMALE, alfabetDUZE);
                                    break;
                                }
                            
                         }
                         break;
                     }
                 case "-a":
                     {
                        Console.WriteLine("-e - jeżeli chcesz zaszyfrować wiadomość");
                        Console.WriteLine("-d - jeżeli chcesz rozszyfrować wiadomość");
                       
                        string codeAction2 = Console.ReadLine();
                        switch (codeAction2)
                        {
                            case "-e":
                                {
                                    crypt_AFINIC(afinic_KEY, alfabetDUZE, alfabetMALE, possibleAfinic_KEYS);
                                    break;
                                }
                            case "-d":
                                {
                                    decrypt_AFINIC(afinic_KEY, alfabetDUZE, alfabetMALE, possibleAfinic_KEYS);
                                    break;
                                }
                        }
                        break;
                     }
             }
        }

        public static void readKEY(ref int cesar_KEY, ref int[] afinic_KEY, string keyTXT)
        {
            if (File.Exists(keyTXT) == true)
            {
                using (TextReader reader = File.OpenText(keyTXT)) // czytanie kluczy
                {
                    string text = reader.ReadLine();
                    string[] bits = text.Split(' ');

                    if (bits.Length > 1)
                    {
                        afinic_KEY[0] = int.Parse(bits[0]);
                        afinic_KEY[1] = int.Parse(bits[1]);
                        cesar_KEY = int.Parse(bits[0]);
                    }
                    else
                    {
                        cesar_KEY = int.Parse(bits[0]);
                    }
                }
            }
        }

        public static void crypt(string alfabetMALE, string alfabetDUZE, int cesar_KEY) 
        {
            File.WriteAllText("crypto.txt", null);
            string read_plain = File.ReadAllText("plain.txt"); // czyta tekst jawny z pliku plain.txt (wiadmość)
            byte[] plain_ASCII = Encoding.ASCII.GetBytes(read_plain); // załdanie kodów ASCII znakow z pliku plain.txt (wiadomość)
            char[] crypted_plain = new char[read_plain.Length];

            if ((cesar_KEY < 25) && (cesar_KEY > 0))
            {
                for (int i = 0; i < read_plain.Length; i++)
                {
                    for (int j = 0; j < 26; j++)
                    {
                        if (read_plain[i].Equals(alfabetDUZE[j]))
                        {
                            crypted_plain[i] = alfabetDUZE[(j + cesar_KEY) % 26];
                        }
                        else if (read_plain[i].Equals(alfabetMALE[j]))
                        {
                            crypted_plain[i] = alfabetMALE[(j + cesar_KEY) % 26];
                        }
                        else if ((plain_ASCII[i] < 65) || ((plain_ASCII[i] < 97) && (plain_ASCII[i] > 90)) || (plain_ASCII[i] > 122))
                        {
                            crypted_plain[i] = (char)plain_ASCII[i];
                        }
                    }
                    File.AppendAllText("crypto.txt", crypted_plain[i].ToString());
                }
            }
            else 
            {
                Console.WriteLine("Error. Incorrect key.");
            }
        }
        public static void decrypt(string alfabetMALE, string alfabetDUZE, int cesar_KEY) 
        {
            File.WriteAllText("decrypt.txt", null);
            string read_decrypt = File.ReadAllText("crypto.txt"); // czyta tekst zaszyfrowany z pliku crypto.txt (zaszyfrowana)
            char[] decrypted = new char[read_decrypt.Length];
            byte[] decrypt_ASCII = Encoding.ASCII.GetBytes(read_decrypt); // czytanie kodów ASCII znakow z pliku crypto.txt (wiadomość)
            
            if ((cesar_KEY < 25) && (cesar_KEY > 0))
            {
                for (int i = 0; i < read_decrypt.Length; i++) 
                {
                    for (int j = 0; j < 26; j++) 
                    {
                        if (read_decrypt[i].Equals(alfabetDUZE[j]))
                        {
                            if((j - cesar_KEY) < 0)
                                decrypted[i] = alfabetDUZE[(j - cesar_KEY) + 26];
                            else 
                                decrypted[i] = alfabetDUZE[(j - cesar_KEY)];
                        }
                        else if (read_decrypt[i].Equals(alfabetMALE[j]))
                        {
                            if ((j - cesar_KEY) < 0)
                                decrypted[i] = alfabetMALE[(j - cesar_KEY) + 26];
                            else
                                decrypted[i] = alfabetMALE[(j - cesar_KEY)];
                        }
                        else if ((decrypt_ASCII[i] < 65) || ((decrypt_ASCII[i] < 97) && (decrypt_ASCII[i] > 90)) || (decrypt_ASCII[i] > 122))
                        {
                            decrypted[i] = (char)decrypt_ASCII[i];
                        }
                    }
                    File.AppendAllText("decrypt.txt", decrypted[i].ToString());
                }
            }
            else
            {
                Console.WriteLine("Error. Incorrect key.");
            }
        }

        
        public static void AnalizeWithPlainTXT(string alfabetMALE, string alfabetDUZE, int cesar_KEY) 
        {
            string read_decrypt = File.ReadAllText("crypto.txt"); // czyta tekst zaszyfrowany z pliku crypto.txt (zaszyfrowana)
            string read_extra = File.ReadAllText("extra.txt"); // czyta część tekstu jawnego
            
            for (int i = 0; i < read_decrypt.Length; i++) {
                for (int j = 0; j < 26; j++)
                {
                    if ((read_extra[0].Equals(alfabetMALE[j])) || (read_extra[0].Equals(alfabetDUZE[j])))
                    {
                        for (int n = 0; n < 26; n++)
                        {
                            if ((read_decrypt[i].Equals(alfabetMALE[n])) || (read_decrypt[i].Equals(alfabetDUZE[n])))
                            {
                                if ((n - j) < 0)
                                    cesar_KEY = (n - j) + 26;
                                else
                                    cesar_KEY = n - j;
                            }
                        }
                    }
                }
                File.WriteAllText("key-new.txt", cesar_KEY.ToString());
                break;
            }
        }

        public static void cryptoAnalize_WITHOUTplain(string alfabetMALE, string alfabetDUZE)
        {
            File.WriteAllText("plain.txt", null);
            string read_decrypt = File.ReadAllText("crypto.txt"); // czyta tekst zaszyfrowany z pliku crypto.txt (zaszyfrowana)
            byte[] decrypt_ASCII = Encoding.ASCII.GetBytes(read_decrypt); // załdanie kodów ASCII znakow z pliku decrypt.txt (wiadomość zaszyfrowana)
            char[] decrypted_plain = new char[decrypt_ASCII.Length];

            int i = 1;
            while (i <= 25)
            {
                for (int character = 0; character < read_decrypt.Length; character++){
                    for (int j = 0; j < 25; j++) {
                        if (read_decrypt[character].Equals(alfabetMALE[j]))
                        {
                            if ((j - i) >= 0)
                                decrypted_plain[character] = alfabetMALE[j - i];
                            else
                                decrypted_plain[character] = alfabetMALE[(j - i) + 26];
                        }
                        else if (read_decrypt[character].Equals(alfabetDUZE[j]))
                        {
                            if ((j - i) >= 0)
                                decrypted_plain[character] = alfabetDUZE[j - i];
                            else
                                decrypted_plain[character] = alfabetDUZE[(j - i) + 26];

                        }
                        else if ((decrypt_ASCII[character] < 65 ) || ((decrypt_ASCII[character] < 97) && (decrypt_ASCII[character] > 90)) || (decrypt_ASCII[character] > 122))
                        {
                            decrypted_plain[character] = (char)(decrypt_ASCII[character]);
                        }
                    }
                    File.AppendAllText("plain.txt", Char.ToString(decrypted_plain[character]));
                }
                File.AppendAllText("plain.txt", "\n");
                i++;
            }
        }

        // SZYFR AFINICZNY //
        public static void crypt_AFINIC(int[] afinic_KEY, string alfabetDUZE, string alfabetMALE, int[] possibleAfinic_KEYS) 
        {
            File.WriteAllText("crypto.txt", null);
            string read_plain = File.ReadAllText("plain.txt"); // czyta tekst jawny z pliku plain.txt (wiadmość)
            byte[] plain_ASCII = Encoding.ASCII.GetBytes(read_plain); // załdanie kodów ASCII znakow z pliku plain.txt (wiadomość)
            bool KEY = false;
            int p = 0;

            while (p < 12)
            {
                if ((possibleAfinic_KEYS[p] == afinic_KEY[0]) && ((afinic_KEY[1] >= 0) && (afinic_KEY[1] <= 25)))
                {
                    KEY = true;
                    break;
                }
                else if (p == 11)
                {
                    KEY = false;
                }
                p++;
            }

            if (KEY == true)
            {
                for (int i = 0; i < read_plain.Length; i++)
                {
                    for (int j = 0; j < 26; j++)
                    {
                        if (read_plain[i].Equals(alfabetDUZE[j]))
                        {
                            int m = ((afinic_KEY[0] * j) + afinic_KEY[1]) % 26;

                            File.AppendAllText("crypto.txt", alfabetDUZE[m].ToString());
                            break;
                        }
                        else if (read_plain[i].Equals(alfabetMALE[j]))
                        {
                            int m = ((afinic_KEY[0] * j) + afinic_KEY[1]) % 26;

                            File.AppendAllText("crypto.txt", alfabetMALE[m].ToString());
                            break;
                        }
                        else if ((plain_ASCII[i] < 65) || ((plain_ASCII[i] < 97) && (plain_ASCII[i] > 90)) || (plain_ASCII[i] > 122)) 
                        {
                            File.AppendAllText("crypto.txt", ((char)plain_ASCII[i]).ToString());
                            break;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Error. Incorrect Key");
            }
        }

        public static void decrypt_AFINIC(int[] afinic_KEY, string alfabetDUZE, string alfabetMALE, int[] possibleAfinic_KEYS)
        {
            File.WriteAllText("decrypt.txt", null);
            string read_decrypt = File.ReadAllText("crypto.txt"); // czyta tekst zaszyfrowany z pliku crypto.txt (zaszyfrowana)
            byte[] decrypt_ASCII = Encoding.ASCII.GetBytes(read_decrypt); // załdanie kodów ASCII znakow z pliku plain.txt (wiadomość)
            char[] Decrypted_TEXT = new char[read_decrypt.Length];

            int Odwrotnosc, keyA = 0, Decrypt;
            int j = 0;
            int p = 0;
            bool KEY = false;

            while (p < 12)
            {
                if ((possibleAfinic_KEYS[p] == afinic_KEY[0]) && ((afinic_KEY[1] >= 0) && (afinic_KEY[1] <= 25)))
                {
                    KEY = true;
                    break;
                }
                else if (p == 11)
                {
                    KEY = false;

                }
                p++;
            }

            if (KEY == true)
            {
                while (j >= 0)
                {
                    Odwrotnosc = (afinic_KEY[0] * j) % 26;
                    if (Odwrotnosc == 1)
                    {
                        keyA = j;
                        break;
                    }
                    j++;
                }
                // ROZSZYFROWANIE
                for (int i = 0; i < read_decrypt.Length; i++)
                {
                    for (int q = 0; q < 26; q++)
                    {
                        if (read_decrypt[i].Equals(alfabetDUZE[q]))
                        {
                            Decrypt = (keyA * (q - afinic_KEY[1])) % 26;

                            if (Decrypt < 0) Decrypt = 26 + Decrypt;

                            File.AppendAllText("decrypt.txt", alfabetDUZE[Decrypt].ToString());
                            break;
                        }
                        else if (read_decrypt[i].Equals(alfabetMALE[q]))
                        {
                            Decrypt = (keyA * (q - afinic_KEY[1])) % 26;

                            if (Decrypt < 0) Decrypt = 26 + Decrypt;

                            File.AppendAllText("decrypt.txt", alfabetMALE[Decrypt].ToString());
                            break;
                        }
                        else if ((decrypt_ASCII[i] < 65) || ((decrypt_ASCII[i] < 97) && (decrypt_ASCII[i] > 90)) || (decrypt_ASCII[i] > 122))
                        {
                            File.AppendAllText("crypto.txt", ((char)decrypt_ASCII[i]).ToString());
                            break;
                        }
                    }
                }
            } 
            else 
            { 
                Console.WriteLine("Error. Incorrect Key"); 
            }
        }
    }
}
