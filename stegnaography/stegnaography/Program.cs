using System;
using System.IO;
using System.Linq;

namespace stegnaography
{
    class Program
    {
        static void Main(string[] args)
        {
            First first = new First();
            Second second = new Second();
            Console.WriteLine("Uzyto opcji {0} {1}", args[0], args[1]);

            switch(args[0])
            {
               case "-e":
                    Console.WriteLine("Zanurzanie...");
                    switch (args[1])
                    {
                        case "-1":
                            Console.WriteLine("Opcja -1 - dodawanie spacji na koncu wiersza - 1 niedodawanie - 0");
                            first.AddSpace();
                            Console.WriteLine("Koniec Zanurzania.");
                            break;

                        case "-2":
                            Console.WriteLine("opcja -2 - jesli jest 1 lub 2 spacje ukryj wiadmosc");
                            second.AddDoubleSpace();
                            Console.WriteLine("Koniec Zanurzania.");
                            break;

                        case "-3":
                            Console.WriteLine("opcja -3 - zamiana znacznika <li> na bledny");
                            break;

                        case "-4":
                            Console.WriteLine("opcja -4 - 1 - <font> </font><font>  0- </font><font></font>");
                            break;
                    }
                    break;

                case "-d":
                    Console.WriteLine("Wyodrebnianie wiadomosci...");
                    switch (args[1])
                    {
                        case "-1":
                            Console.WriteLine("Opcja -1 - dodawanie spacji na koncu wiersza - 1 niedodawanie - 0");
                            first.GetMsg();
                            first.FromHexToLetter();
                            Console.WriteLine("Koniec Wyodrebniania.");
                            break;

                        case "-2":
                            Console.WriteLine("opcja -2 - jesli jest 1 lub 2 spacje ukryj wiadmosc");
                            second.GetMsg();
                            Console.WriteLine("Koniec Wyodrebniania");
                            break;

                        case "-3":
                            Console.WriteLine("opcja -3 - zamiana znacznika <li> na bledny");
                            break;

                        case "-4":
                            Console.WriteLine("opcja -4 - 1 - <font> </font><font>  0- </font><font></font>");
                            break;
                    }
                    break;
            }
            Console.ReadKey();
        }
    }
}
