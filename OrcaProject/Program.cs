using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace OrcaProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            int quit = 0;
            CardReaderDisplay cd;
            while (quit != 4)
            {
                Console.WriteLine("Choose your option");
                Console.WriteLine("1.\tInitializeCard");
                Console.WriteLine("2.\tRide Bus");
                Console.WriteLine("3.\tRideTrain");
                Console.WriteLine("4.\tQuit");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        InitializeCard ic = new InitializeCard();
                        break;
                    case 2:
                        cd = new CardReaderDisplay(1);
                        break;
                    case 3:
                        cd = new CardReaderDisplay(2);
                        break;
                    case 4:
                        quit = 4;
                        break;
                    default:
                        quit = 0;
                        break;
                }
            }

            
       
        }

       
    }
}
