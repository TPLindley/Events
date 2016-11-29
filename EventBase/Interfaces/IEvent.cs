namespace EventBase
{
    public interface IEvent
    {
        string Name();
        string Process(int value);
    }
}
