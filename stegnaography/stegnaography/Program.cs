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
            Third third = new Third();
            Fourth fourth = new Fourth();
            Console.WriteLine("Uzyto opcji {0} {1}", args[0], args[1]);

            switch(args[0])
            {
               case "-e":
                    Console.WriteLine("Zanurzanie...");
                    switch (args[1])
                    {
                        case "-1":
                            first.AddSpace();
                            break;

                        case "-2":
                            second.AddDoubleSpace();
                            break;

                        case "-3":
                            third.AddingFalseAttributeToLi();
                            break;

                        case "-4":
                            fourth.ChangingClosingOfLi();
                            break;
                    }
                    break;

                case "-d":
                    Console.WriteLine("Wyodrebnianie wiadomosci...");
                    switch (args[1])
                    {
                        case "-1":
                           
                            first.GetMsg();
                            break;

                        case "-2":
                           
                            second.GetMsg();
                            break;

                        case "-3":
                            
                            third.GetMsg();
                            break;

                        case "-4":
                            
                            fourth.GetMsg();
                            break;
                    }
                    break;
            }
        }
    }
}
