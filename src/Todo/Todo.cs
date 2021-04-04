using System;

namespace Todo
{
    public class Todo
    {
        public string Title {
            get;
            private set;
        }

        public Todo(string title) 
        {
            this.Title = title;
        }
    }
}
