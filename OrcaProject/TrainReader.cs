using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrcaProject
{
    class TrainReader : CardReader, IFares
    {
        /// <summary>
        /// this class inherits from CardReader and implements IFares
        /// it handles the Sounder train trips
        /// The tricky part with this is figuring out if you tapped
        /// to get on or getting off
        /// </summary>
        const string MODE = "ST ExpressBus";
        string location;
        const double BASEFARE = 3.00;
        const double PERSTATION = .5;
        CardSerialization serialize;

    
        public TrainReader(string cardNumber, string sLocation)
            : base(cardNumber)
        {
           
            Message = null;
            location = sLocation;
            GetFare();
            DeductFare();
            serialize = new CardSerialization();
            serialize.SerializeCard(C);
        }

        public void GetFare()
        {
            Trip[] trips = C.TripList;
            if (C.TripFlag)
            {
                int stop = GetStation(trips[0].vehicleLoc);
                int start = GetStation(location);
                Fare = Math.Abs(start - stop) * PERSTATION + BASEFARE;
            }
            SwitchTripFlag();

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
                //if the card type is pass just add the trip
                AddTrip();
            }
        }

        public void AddTrip()
        {
            //create the trip object
            Trip t = new Trip();
            t.Mode = MODE;
            t.TripDate = DateTime.Now.ToShortDateString();
            t.TripTime = DateTime.Now.TimeOfDay.ToString();
            t.vehicleLoc = location;

            //add it to the log and the card
            AddTripToLog(t);
            C.AddTrip(t);
            //create the message that the reader will return
            CreateTripMessage(t);
        }



        private int GetStation(string location)
        {
            //for the sounders train the coach numbers
            //represent stations 1 being Lakewood
            //8 being Kingstreet
            //I assume there are further numbers
            //for stations heading north to Everett
            int locationNumber = 0;
            switch (location)
            {
                case "Coach#1":
                    locationNumber = 1;
                    break;
                case "Coach#2":
                    locationNumber = 2;
                    break;
                case "Coach#3":
                    locationNumber = 3;
                    break;
                case "Coach#4":
                    locationNumber = 4;
                    break;
                case "Coach#5":
                    locationNumber = 5;
                    break;
                case "Coach#6":
                    locationNumber = 5;
                    break;
                case "Coach#7":
                    locationNumber = 5;
                    break;
                case "Coach#8":
                    locationNumber = 5;
                    break;
                default:
                    locationNumber = 0;
                    break;

            }

            return locationNumber;
        }

        private void SwitchTripFlag()
        {
            if (C.TripFlag)
            {
                C.TripFlag = false;
            }
            else
            {
                C.TripFlag = true;
            }
        }

        public override string ToString()
        {
            return Message;
        }
    }
}
