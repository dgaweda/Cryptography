using System;

namespace xor
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("-p - przygotowanie tekstu");
            Console.WriteLine("-e - szyfrowanie");
            Console.WriteLine("-k - kryptoanaliza");
            Console.WriteLine("--------------------");

            string option = Console.ReadLine();
           
            switch (option)
            {
                case "-p":
                    prepare prepare = new prepare();
                    prepare.prepareOrig();
                    Console.WriteLine("Przygotowywanie tekstu zakonczone.");
                    break;

                case "-e":
                    encrypt encrypt = new encrypt();
                    encrypt.encryptplain();
                    Console.WriteLine("Szyfrowanie zakonczone.");
                    break;

                case "-k":
                    analize cryptoanalize = new analize();
                    cryptoanalize.cryptoanalize();
                    cryptoanalize.decrypt();
                    Console.WriteLine("Kryptoanaliza zakonczona.");
                    break;
            }
        }
    }
}
