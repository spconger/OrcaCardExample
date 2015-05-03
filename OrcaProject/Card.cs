using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;


namespace OrcaProject
{
    [Serializable]
     public class Card
    {
        
       /// <summary>
       /// This class contains the card information
       /// and a list of up the 8 last trips
       /// CardType and CardRegion use values from
       /// public enumerations
       /// Also are methods for adding a trip to the card
       /// which will only store eight trips
       /// when the card has 8 trips and the que is full
       /// the oldest one drops off the list
       /// </summary>
        public string SerialNumber { set; get; }
        public double Balance { set; get; }
        public int CardType { get; set; }
        public int CardRegion { get; set; }

        public bool TripFlag { set; get; }

        //array to store Trips
        public Trip[] TripList { get; set; }

     
        //constructor
        public Card()
        {
            //initialize the array and set to 8
            TripList = new Trip[8];
            TripFlag = false;
        }

    
        //add a trip
        public void AddTrip(Trip t)
        {
            //call the method that resets the array
            TripReset(t);
        }

       

        private void TripReset(Trip t)
        {

            //this is a crude way to do this
            //but it works and is easy to understand
            TripList[7] = TripList[6];
            TripList[6] = TripList[5];
            TripList[5] = TripList[4];
            TripList[4] = TripList[3];
            TripList[3] = TripList[2];
            
            TripList[2] = TripList[1];
            TripList[1] = TripList[0];
            TripList[0] = t;
        }

    }
}
