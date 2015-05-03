using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace OrcaProject
{
    public class CardSerialization
    {
        /// <summary>
        /// This is a utility class that has two methods.
        /// The DeserializeCard() method takes a card number
        ///finds the file associated with that number
        ///loads it, and deserializes it--e.g. turns the XML
        ///file into a C# Class--in this case an instance of the
        ///Card Class. It returns the Card object to the calling
        ///method. 
        ///The second method SerializeCard() takes a Card object
        ///as a parameter and serializes the object into an XML file
        ///using the serial number as the file name
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>

         public Card DeserializeCard(string cardNumber)
        {
            XmlSerializer mySerializer = new XmlSerializer(typeof(Card));
            // To read the file, create a FileStream.
            Card c = null; ;
            try 
            { 
            FileStream myFileStream = new FileStream(cardNumber+".xml", FileMode.Open);
            // Call the Deserialize method and cast to the object type.
            c = (Card) mySerializer.Deserialize(myFileStream);
            myFileStream.Close();
            }
            catch (FileNotFoundException fnf)
            {
                throw fnf;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return c;
        }

        public void SerializeCard(Card c)
        {
            XmlSerializer mySerializer = new XmlSerializer(typeof(Card));
            // To write to a file, create a StreamWriter object.
            try
            {
                using ( StreamWriter myWriter = new StreamWriter(c.SerialNumber + ".xml",false))
                { 
               
                     mySerializer.Serialize(myWriter, c);
                     mySerializer = null;
                     myWriter.Flush();
                     myWriter.Close();
                }
                
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
