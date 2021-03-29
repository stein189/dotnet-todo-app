namespace Projection {
    public class TodoCreated : Event {
        public string Title {
            get;
            set;
        }

        public TodoCreated() : base() {}
    }
}
