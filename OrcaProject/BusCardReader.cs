using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrcaProject
{
    class BusCardReader : CardReader, IFares
    {
        /// <summary>
        /// this class inherits from CardReader and implements IFares interface
        /// it reads the card based on the card number
        /// (this is passed to the parent class which deserializes the card
        /// and assigns it to "C" which is used below to refer to the card
        /// Three fares are listed as constant
        /// The times were listed as constant but TimeSpan is illegal as
        /// a constant, so they are used like constants but withouth the
        /// constant declaration.
        /// The Read() method called by the constructor calls
        /// the methods to get and deduct the fare
        /// the trip is written to the card and the triplog
        /// and then the card is reserialized into xml
        /// 
        /// </summary>

        private CardSerialization serial;
       
     
        string coachNumber;
        string busType;
        private const double REGFARE = 2.50;
        private const double PEAK = 3.50;
        private const double DISC = 1.50;
        private  string STARTMORNINGPEAK = new TimeSpan(6,30,0).ToString();
        private string ENDMORNINGPEAK = new TimeSpan(8, 30, 0).ToString();
        private string STARTAFTERNOONPEAK = new TimeSpan(16,0,0).ToString();
        private string ENDAFTERNOONPEAK = new TimeSpan(18,30,0).ToString();

        /// <summary>
        /// /the constructor takes three arguments the cardNumber, the coach and bus
        /// it passes the card number up to the parent, the abstract class
        /// BusReader. It calls the Read() Method and when that has completed
        /// Sends the card to Serialize to be reserialized into XML
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <param name="coach"></param>
        /// <param name="bus"></param>
        public BusCardReader(string cardNumber, string coach, string bus): base(cardNumber)
        {
            serial = new CardSerialization();
            coachNumber = coach;
            busType = bus;
            
            Read();
            serial.SerializeCard(C);
        }


        private void Read()
        {
            //set the message(used in ToString() to null
            Message = null;
            //call GetFare
            GetFare();
            //Call Deduct Fare
            DeductFare();
        
            
          
        }
        public void GetFare()
        {
            // Get the time of day
            TimeSpan  tapTime = new TimeSpan();
            tapTime=DateTime.Now.TimeOfDay;
            //if the card isn't discount
            if (C.CardType != (int)Cardtypes.pass)
            {
                //choose the appropriate fare for time of day
                if (tapTime >= TimeSpan.Parse(STARTMORNINGPEAK) && tapTime <= TimeSpan.Parse(ENDMORNINGPEAK) || 
                    tapTime >= TimeSpan.Parse(STARTAFTERNOONPEAK) && tapTime<= TimeSpan.Parse(ENDAFTERNOONPEAK))
                {
                    Fare = PEAK;
                }
                else if(C.CardType==(int)Cardtypes.discount)
                 {
                    Fare = REGFARE;
                }
            }
           
        }


        public void DeductFare()
        {
            //if the card type is purse
            if (C.CardType == (int)Cardtypes.purse)
            {
                //if they have enough money on the card
                if (C.Balance > Fare)
                {
                    C.Balance = C.Balance - Fare;
                    AddTrip();
                }
                else
                {
                    //otherwise let them know they have insufficient funds
                    Message = "Insufficient Funds Please Add money";
                }
            }
            else
            {
                //we are only going to cover king county or all in this reader.
                if (C.CardRegion == (int)CardRegions.all || C.CardRegion == (int)CardRegions.KingCounty)
                {
                    AddTrip();
                }
                else
                {
                    Message = "Your pass does not cover this region";
                }
            }
        }

        public void AddTrip()
        {
            //create the trip object
            Trip t = new Trip();
            t.Mode = busType;
            t.TripDate = DateTime.Now.ToShortDateString();
            t.TripTime = DateTime.Now.TimeOfDay.ToString();
            t.vehicleLoc = coachNumber;

            //add it to the log and the card
            AddTripToLog(t);
            C.AddTrip(t);
            //create the message that the reader will return
            CreateTripMessage(t);

        }
       
        public override string ToString()
        {
            //the message varies depending on what
            //occurs in the card reading

            return Message;
        }


     



       
    }
}
