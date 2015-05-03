using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace OrcaProject
{
    public abstract class CardReader
    {
        /// <summary>
        /// This abstract class is the parent of all the Card reader classes
        /// it takes a Card as a parameter in its constructor
        /// and deserializes it from its Xml file format
        /// //so that it is ready for use. It also has a method
        /// for writing the trips to a log
        /// </summary>
        /// 
        // declare the card object
        public Card C {get; set;}
        public string Message { get; set; }
        public double Fare { get; set; }

        public CardSerialization CardSerialization
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        //declare the serialization class
        public CardSerialization cs;

        //constructor initilizes serialize Class
        //Deserializes the xml into a Card ojbect
        //and assigns it to C
        public CardReader(string cardNumber)
        {
            Fare = 0;
            cs = new CardSerialization();
            C=cs.DeserializeCard(cardNumber);
        }

      

       
        //writes the current trip to the log file
        public void AddTripToLog(Trip t)
        {
            StreamWriter writer = new StreamWriter("tripLog.txt", true);

            StringBuilder trip = new StringBuilder();
            trip.Append(C.SerialNumber);
            trip.Append("'");
            trip.Append(t.TripDate);
            trip.Append(",");
            trip.Append(t.TripTime);
            trip.Append(",");
            trip.Append(t.Mode);
            trip.Append(",");
            trip.Append(t.vehicleLoc);
        

            writer.WriteLine(trip.ToString());

            writer.Close();
        }

        public void CreateTripMessage(Trip t)
        {
            //use the String Builder object to
            //assemble the message about the 
            //trip returned by ToString()
            StringBuilder builder = new StringBuilder();
            builder.Append(t.Mode);
            builder.Append(", ");
            builder.Append(t.vehicleLoc);
            builder.Append(", ");
            builder.Append(t.TripDate);
            builder.Append(", ");
            builder.Append(t.TripTime);
            builder.Append(", ");
            builder.Append(C.Balance.ToString());

            Message = builder.ToString();


        }
    }
}
