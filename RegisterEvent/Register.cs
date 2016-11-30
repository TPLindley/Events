using EventBase;

namespace RegisterEvent
{
    public class Register : BaseEvent
    {
        public Register()
            :base()
        {
            Multiples.Add(3, EventLibConstants.Register);
            Multiples.Add(5, EventLibConstants.Patient);
        }
        public override string Name()
        {
            return EventLibConstants.Name;
        }
        public override string Process(int value)
        {
            return base.Process(value).Trim();
        }
    }
}
