using System;
using System.IO;

namespace vigenere
{
    class Program
    {
        static void Main(string[] args)
        {
            vigenere VigenereCrypt = new vigenere();
            
            Console.Write(" -p - przygotowanie tekstu oryginalnego \n -e - szyfrowanie tekstu \n -d - odszyfrowanie tekstu \n");
            string option = Console.ReadLine();
           
            switch (option)
            {
                case "-p": 
                    VigenereCrypt.prepare();
                    break;
                
                case "-e":
                    VigenereCrypt.encrypt();
                    break;

                case "-d":
                    VigenereCrypt.decrypt();
                    break;

                case "-c":
                    VigenereCrypt.analize();
                    break;
                
                default:
                    {
                        Console.WriteLine("-p Required");
                        break;
                    }
            }
        }
    }
}
