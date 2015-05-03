using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrcaProject
{
    public interface IFares
    {
        /// <summary>
        /// this is an interface that must be implemented by each
        /// Card Reader
        /// I tried to refractor these into the abstract
        /// base class CardReader, but there is just
        /// enough specific information in them
        /// to prevent it.
        /// </summary>
        void GetFare();
        void DeductFare();
        void AddTrip();
       
    }
}
