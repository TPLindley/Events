namespace EventBase.Interfaces
{
    /// <summary>
    /// Interface definition for event handlers
    /// </summary>
    public interface IEvent
    {
        string Name();
        string Process(int value);
    }
}
