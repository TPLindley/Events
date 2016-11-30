using EventBase;

namespace RegisterEvent
{
    public class Register : BaseEvent
    {
        /// <summary>
        /// Constructor to implement rules for 'Regster' event
        /// 
        /// BaseEvent handles processing, this constructor sets the multiples and values to return
        /// </summary>
        public Register()
        {
            Multiples.Add(3, EventLibConstants.Register);
            Multiples.Add(5, EventLibConstants.Patient);
        }
        /// <summary>
        /// Method to return the name of this event handler
        /// </summary>
        /// <returns></returns>
        public override string Name()
        {
            return EventLibConstants.Name;
        }
        /// <summary>
        /// Method to implement the business rules for the 'Register' event
        /// 
        /// Event type = “Register”
        ///
        /// Print the numbers from one to one hundred except in the following cases:
        /// •	For multiples of three print “Register” instead of the number
        /// •	For the multiples of five print “Patient” instead of the number
        /// •	For numbers which are multiples of both three and five print “Register Patient” instead of the number
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override string Process(int value)
        {
            return base.Process(value).Trim();
        }
    }
}
