using System.Reflection;

namespace Events.Model
{
    public class MyEventHandler
    {
        public Assembly EventAssembly { get; set; }
        public string Name { get; set; }
        public string ProcessEvent(int value)
        {
            string eventName = EventAssembly.FullName.Substring(0, EventAssembly.FullName.IndexOf("Event"));
            var types = EventAssembly.GetTypes();
            var method = EventAssembly.GetType($"{eventName}Event.{eventName}");
            var obj = method.InvokeMember(null, BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance | BindingFlags.CreateInstance, null, null, new object[] { });
            var result = method.InvokeMember("Process", System.Reflection.BindingFlags.InvokeMethod, System.Type.DefaultBinder, obj, new object[] { value });
            return result as string;
        }
    }
}
