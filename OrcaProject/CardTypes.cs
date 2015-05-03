using System;

namespace OrcaProject
{
    public enum Cardtypes 
    { 
        /// <summary>
        /// this is an enumeration of the card types
        /// pass means no money is deducted--the payment
        /// is up front when you purchase the card
        /// purse means money is added to the card directly
        /// and reduced with each fare
        /// discount is like purse but with a reduced fare
        /// </summary>
        pass, purse, discount 
    };
}
