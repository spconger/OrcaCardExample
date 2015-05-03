using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrcaProject
{
    class CardReaderDisplay
    {
        private int rType;
        
        public CardReaderDisplay(int readerType) 
        {
            rType = readerType;
            ReadCard();
        }

        private void ReadCard()
        {
            if (rType==1)
            {
                GetBusInformation();
            }

            if (rType==2)
            {
                GetTrainInformation();
            }
        }

        private void GetBusInformation()
        {
            Console.WriteLine("Enter Card Number");
            string cNumber = Console.ReadLine();
            Console.WriteLine("Enter Coach Number");
            string coach = Console.ReadLine();
            Console.WriteLine("Enter Bus info");
            string bus = Console.ReadLine();
            try
            {
                BusCardReader br = new BusCardReader(cNumber, coach, bus);
                Console.WriteLine(br.ToString());
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void GetTrainInformation()
        {
            Console.WriteLine("Enter Card Number");
            string cNumber = Console.ReadLine();
            Console.WriteLine("Enter Coach plus '#' plus 1-10");
            string coach = Console.ReadLine();

            try
            {
                TrainReader tr = new TrainReader(cNumber, coach);
                Console.WriteLine(tr.ToString());
            }
             catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
