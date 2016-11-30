using System;
using System.Reflection;

namespace Events.Model
{
    /// <summary>
    /// EventHandler class to hold the event handlers loaded dynamically
    /// </summary>
    public class MyEventHandler
    {
        public Assembly EventAssembly { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// Method to invoke the event handling method
        /// 
        /// 1: Find the base class assembly type
        /// 2: Invoke the object constructor
        /// 3: Invoke the 'Process' method (see IEvent interface)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string ProcessEvent(int value)
        {
            string eventName = EventAssembly.FullName.Substring(0, EventAssembly.FullName.IndexOf("Event", StringComparison.Ordinal));
            var method = EventAssembly.GetType($"{eventName}Event.{eventName}");
            var obj = method.InvokeMember(null, BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance | BindingFlags.CreateInstance, null, null, new object[] { });
            var result = method.InvokeMember("Process", BindingFlags.InvokeMethod, Type.DefaultBinder, obj, new object[] { value });
            return result as string;
        }
    }
}
