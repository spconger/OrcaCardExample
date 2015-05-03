using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrcaProject
{
    public class Trip
    {
        /// <summary>
        /// this class encapuslates the Trip 
        /// it includes the Mode which refers
        /// to whether it is Train or bus
        /// VehicleLoc is the vehicle id or the start location
        /// then the date and time of the trip
        /// </summary>
        public string Mode { get; set; }
        public string vehicleLoc { get; set; }
        public string TripDate { get; set; }
        public string TripTime { get; set; }

       
    }
}
