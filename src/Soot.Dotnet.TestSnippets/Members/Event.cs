namespace Soot.Dotnet.TestSnippets.Members
{
    public class Event
    {
        public event MyEvent MyEvent;

        protected virtual void OnMyEvent(MyEventArgs args)
        {
            MyEvent?.Invoke(this, args);
        }
    }

    public delegate void MyEvent(object sender, MyEventArgs args);

    public class MyEventArgs
    {
    }
}