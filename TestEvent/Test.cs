using EventBase;

namespace TestEvent
{
    public class Test : BaseEvent
    {
        /// <summary>
        /// Constructor to implement rules for 'Test' event
        /// 
        /// BaseEvent handles processing, this constructor sets the multiples and values to return
        /// </summary>
        public Test()
        {
            Multiples.Add(4, TestLibConstants.Test);
            Multiples.Add(11, TestLibConstants.Patient);
        }
        public override string Name()
        {
            return TestLibConstants.Name;
        }

        public override string Process(int value)
        {
            return base.Process(value).Trim();
        }
    }
}
