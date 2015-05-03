using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OrcaProject
{
    class InitializeCard
    {
        Card card;
        CardSerialization cSerial;

        public InitializeCard()
        {
            card = new Card();
            cSerial = new CardSerialization();
            SetCardInfo();
            cSerial.SerializeCard(card);
        }

        private void SetCardInfo()
        {
            card.SerialNumber = GenerateSerial();
            CardTypePrompt();

        }

        private string GenerateSerial(){
            int mSec=DateTime.Now.Millisecond;
            int sec=DateTime.Now.Second;
            int year = DateTime.Now.Year;
            int month =DateTime.Now.Month;
            int day = DateTime.Now.Day;
            
            Random rand = new Random();
            int num = rand.Next(1000, 9999);

            return num.ToString()+year.ToString()+month.ToString()+day.ToString()+sec.ToString()+mSec.ToString();
        }


        private void CardTypePrompt()
        {
            Console.WriteLine("Please Select Card Type. Key in the appropriate number.");
            Console.WriteLine("1.\tPass");
            Console.WriteLine("2.\tPurse");
            Console.WriteLine("3.\tDiscount");
            int choice;
            bool goodChoice = int.TryParse(Console.ReadLine(), out choice);
            if(goodChoice)
            {
                //SetCardType(choice);
                if (choice ==1)
                {
                    card.CardType = (int)Cardtypes.pass;
                   int reg= SelectRegion(choice);
                   SetRegionChoice(reg);
                }
                
                if (choice==2)
                {
                    card.CardType=(int)Cardtypes.purse;
                    
                    SetInitialPurse();
                }
                else
                {
                    card.Balance = 0;
                }

                if (choice==3)
                {
                   card.CardType=(int)Cardtypes.discount;
                    SetInitialPurse();
                }
            }
            CardCompleted();

        }

        private int SelectRegion(int r)
        {
            Console.WriteLine("Which region(s) will this pass cover?");
            Console.WriteLine("Key in the number that matches the region.");
            Console.WriteLine("1.\tAll");
            Console.WriteLine("2.\tKing County");
            Console.WriteLine("3.\tPierce County");
            Console.WriteLine("4.\tSnohomish County");
            int choice;
            bool goodChoice = int.TryParse(Console.ReadLine(), out choice);
           return choice;

        }

        private void SetRegionChoice(int rChoice)
        {
            switch(rChoice)
            {
                case 1:
                    card.CardRegion = (int)CardRegions.all;
                    break;
                case 2:
                    card.CardRegion=(int)CardRegions.KingCounty;
                    break;
                case 3:
                    card.CardRegion = (int)CardRegions.Pierce;
                    break;
                case 4:
                    card.CardRegion = (int)CardRegions.Snohomish;
                    break;
                default:
                    card.CardRegion = -1;
                    break;

            }
        }

        private void SetInitialPurse()
        {
            Console.WriteLine("Enter the amount of money to Initialize Card. $5 Dollar minimum");
            double amount;
            bool goodAmount = double.TryParse(Console.ReadLine(), out amount);
            while (!goodAmount)
            {
                Console.WriteLine("Please enter a valid numerical amount (no dollar signs).");
                goodAmount = double.TryParse(Console.ReadLine(), out amount);
            }
            while (amount < 5)
            {
                Console.WriteLine("A minimum of $5 is required.");
                goodAmount = double.TryParse(Console.ReadLine(), out amount);
            }
            card.Balance = amount;
        }

        private void CardCompleted()
        {
            Console.WriteLine("Your card number is {0}", card.SerialNumber);
            Console.WriteLine("Thank you for buying ORCA.");
        }
    }
}
