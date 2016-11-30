using System;
using System.Collections.Generic;
using EventBase.Interfaces;

namespace EventBase
{
    /// <summary>
    /// Base class for event handling. Rule set is:
    /// 
    /// Allow input integers from 1 to 100, outside that range, arguments are invalid
    /// Accept pairs of integers and strings, use the integer as multipliers of the index
    /// If the index is a multiplier of the given key, then concatenate the supplied value to return string
    /// If no multiplers are supplied, or found, then convert the index to a string
    /// </summary>
    public abstract class BaseEvent : IEvent
    {
        private readonly Dictionary<int, string> _multiples;
        protected Dictionary<int,string> Multiples => _multiples;
        /// <summary>
        /// BaseEvent constructor, allocate empty dictionary
        /// </summary>
        protected BaseEvent()
        {
            _multiples = new Dictionary<int, string>();
        }
        /// <summary>
        /// Name method, throw exception if called
        /// </summary>
        /// <returns></returns>
        public virtual string Name()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Process according to rules. If index is mod of given value, add string to output
        /// if not divisible, then just return the index as string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual string Process(int value)
        {
            string result = string.Empty;
            if (value<1||value>100)                             // Validate input numbers, throw invalid argument exception if out of bounds
                throw new ArgumentException();
            foreach(var multiple in _multiples)                 // Check supplied multiples, if true concatenate given value
            {
                if (value % multiple.Key == 0)
                    result += multiple.Value;
            }
            if (result.Length == 0)                             // If no values, i.e. no multipliers, then convert index to string
                result = value.ToString();
            return result;
        }
    }
}
