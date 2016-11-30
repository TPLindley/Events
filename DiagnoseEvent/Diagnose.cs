using EventBase;

namespace DiagnoseEvent
{
    public class Diagnose : BaseEvent
    {
        /// <summary>
        /// Constructor to implement rules for 'Diagnose' event
        /// 
        /// BaseEvent handles processing, this constructor sets the multiples and values to return
        /// </summary>
        public Diagnose()
        {
            Multiples.Add(2, DiagnoseLibConstants.Diagnose);
            Multiples.Add(7, DiagnoseLibConstants.Patient);
        }
        /// <summary>
        /// Method to return the name of this event handler
        /// </summary>
        /// <returns></returns>
        public override string Name()
        {
            return DiagnoseLibConstants.Name;
        }
        /// <summary>
        /// Method to implement the business rules for the 'Diagnose' event
        /// 
        /// Event type = “Diagnose”
        ///
        /// Print the numbers from one to one hundred except in the following cases:
        /// •	For multiples of two print “Diagnose” instead of the number
        /// •	For the multiples of seven print “Patient” instead of the number
        /// •	For numbers which are multiples of both three and five print “Diagnose Patient” instead of the number
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override string Process(int value)
        {
            return base.Process(value).Trim();
        }
    }
}
