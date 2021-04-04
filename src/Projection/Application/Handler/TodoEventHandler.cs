using System;

namespace Todo.Projection
{
    public class TodoEventHandler
    {
        public void Handle(Event occurredEvent)
        {
            Console.WriteLine("Ignoring event, method not yet implemented");
        }
    }
}