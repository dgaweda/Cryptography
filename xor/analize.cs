<<<<<<< HEAD
﻿using System;
using System.IO;
using System.Text;

namespace xor
{
    class analize
    {
        static string crypted = File.ReadAllText("crypto.txt");
        byte[] cryptedAscii = Encoding.ASCII.GetBytes(crypted);
        int keyLength = 32;

        public int[,] CharToTable()
        {
            int x = (cryptedAscii.Length / keyLength) + 1;
            int y = keyLength;
            int b;
            int[,] cryptedTab = new int[x, y];
            int j = 0;

            for (var i = 0; i < cryptedAscii.Length; i++)
            {
                if (i % keyLength != 0 || i == 0)
                {
                    b = i % keyLength;

                    cryptedTab[j, b] = cryptedAscii[i];
                }
                else if (i != 0)
                {
                    j++;
                    b = i % keyLength;

                    cryptedTab[j, b] = cryptedAscii[i];
                }
            }
            return cryptedTab;
        }

        public void cryptoanalize()
        {
            File.WriteAllText("key-new.txt", null);
            int y = keyLength;
            int[,] cryptedTab = CharToTable();
            char[] key = new char[y];
            
            int x = (cryptedAscii.Length / keyLength)+1;
            int xor, xor2, xor3;
            int j = 0;
            int k = 0;
            int plusOne, minusOne;

            for (var i = 0; i < x; i++)
            {
                plusOne = i + 1;
                minusOne = i - 1;
                xor = cryptedTab[i, j] ^ cryptedTab[plusOne, j];

                if (i == 0 && ((xor >= 65 && xor <= 90) || (xor >= 97 && xor <= 122)))
                {
                    xor3 = cryptedTab[i, j] ^ xor;
                    if ((xor3 < 65 || xor3 > 90))
                        xor3 = cryptedTab[plusOne, j] ^ xor;
                    
                    key[k] = Char.ToLower((char)xor3);
                    File.AppendAllText("key-new.txt", key[k].ToString());
                    
                    j++; k++; i = -1;
                    if (j == keyLength) break;
                }
                else if ((xor >= 65 && xor <= 90) || (xor >= 97 && xor <= 122))
                {
                    xor2 = cryptedTab[i, j] ^ cryptedTab[minusOne, j];

                    if (xor2 < 65 || (xor2 > 90 && xor2 < 97) && xor2 > 122)
                    {
                        xor3 = xor ^ cryptedTab[i, j];

                        key[k] = Char.ToLower((char)xor3);
                        File.AppendAllText("key-new.txt", key[k].ToString());
                        
                        j++; k++; i = -1;
                        if (j == keyLength) break;
                    }
                }

            }
        }

        public void decrypt()
        {
            File.WriteAllText("decrypt.txt", null);
            string crypted = File.ReadAllText("crypto.txt");
            string key_new = File.ReadAllText("key-new.txt");
            
            byte[] newKeyAscii = Encoding.ASCII.GetBytes(key_new);
            byte[] cryptedAscii = Encoding.ASCII.GetBytes(crypted);
            
            int xor;
            int a;

            for (var i = 0; i < crypted.Length; i++)
            {
                a = i % (key_new.Length);
                xor = cryptedAscii[i] ^ newKeyAscii[a];
                
                File.AppendAllText("decrypt.txt", ((char)xor).ToString());
            }
        }
    }
}
=======
﻿using System;
using System.IO;
using System.Text;

namespace xor
{
    class analize
    {
        static string crypted = File.ReadAllText("crypto.txt");
        byte[] cryptedAscii = Encoding.ASCII.GetBytes(crypted);
        int keyLength = 32;

        public int[,] CharToTable()
        {
            int x = (cryptedAscii.Length / keyLength) + 1;
            int y = keyLength;
            int b;
            int[,] cryptedTab = new int[x, y];
            int j = 0;

            for (var i = 0; i < cryptedAscii.Length; i++)
            {
                if (i % keyLength != 0 || i == 0)
                {
                    b = i % keyLength;

                    cryptedTab[j, b] = cryptedAscii[i];
                }
                else if (i != 0)
                {
                    j++;
                    b = i % keyLength;

                    cryptedTab[j, b] = cryptedAscii[i];
                }
            }
            return cryptedTab;
        }

        public void cryptoanalize()
        {
            File.WriteAllText("key-new.txt", null);
            int y = keyLength;
            int[,] cryptedTab = CharToTable();
            char[] key = new char[y];
            
            int x = (cryptedAscii.Length / keyLength)+1;
            int xor, xor2, xor3;
            int j = 0;
            int k = 0;
            int plusOne, minusOne;

            for (var i = 0; i < x; i++)
            {
                plusOne = i + 1;
                minusOne = i - 1;
                xor = cryptedTab[i, j] ^ cryptedTab[plusOne, j];

                if (i == 0 && ((xor >= 65 && xor <= 90) || (xor >= 97 && xor <= 122)))
                {
                    xor3 = cryptedTab[i, j] ^ xor;
                    if ((xor3 < 65 || xor3 > 90))
                        xor3 = cryptedTab[plusOne, j] ^ xor;
                    
                    key[k] = Char.ToLower((char)xor3);
                    File.AppendAllText("key-new.txt", key[k].ToString());
                    
                    j++; k++; i = -1;
                    if (j == keyLength) break;
                }
                else if ((xor >= 65 && xor <= 90) || (xor >= 97 && xor <= 122))
                {
                    xor2 = cryptedTab[i, j] ^ cryptedTab[minusOne, j];

                    if (xor2 < 65 || (xor2 > 90 && xor2 < 97) && xor2 > 122)
                    {
                        xor3 = xor ^ cryptedTab[i, j];

                        key[k] = Char.ToLower((char)xor3);
                        File.AppendAllText("key-new.txt", key[k].ToString());
                        
                        j++; k++; i = -1;
                        if (j == keyLength) break;
                    }
                }

            }
        }

        public void decrypt()
        {
            File.WriteAllText("decrypt.txt", null);
            string crypted = File.ReadAllText("crypto.txt");
            string key_new = File.ReadAllText("key-new.txt");
            
            byte[] newKeyAscii = Encoding.ASCII.GetBytes(key_new);
            byte[] cryptedAscii = Encoding.ASCII.GetBytes(crypted);
            
            int xor;
            int a;

            for (var i = 0; i < crypted.Length; i++)
            {
                a = i % (key_new.Length);
                xor = cryptedAscii[i] ^ newKeyAscii[a];
                
                File.AppendAllText("decrypt.txt", ((char)xor).ToString());
            }
        }
    }
}
>>>>>>> ea6092fdba1d2425318dc2530209163952a883d6
