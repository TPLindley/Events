using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace EventBase
{
    public abstract class BaseEvent : IEvent
    {
        private Dictionary<int, string> multiples;
        protected Dictionary<int,string> Multiples { get { return multiples; } }
        public BaseEvent()
        {
            multiples = new Dictionary<int, string>();
        }
        public virtual string Name()
        {
            throw new NotImplementedException();
        }
        public virtual string Process(int value)
        {
            string result = string.Empty;
            if (value<1||value>100)
                throw new ArgumentException();
            foreach(var multiple in multiples)
            {
                if (value % multiple.Key == 0)
                    result += multiple.Value;
            }
            if (result.Length == 0)
                result = value.ToString();
            return result;
        }
    }
}
