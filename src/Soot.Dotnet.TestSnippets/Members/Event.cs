using System;

namespace Soot.Dotnet.TestSnippets.Members
{
    public class Event
    {
        // Declare the event.
        public event MyEventHandler MyEvent;
        
        public delegate void MyEventHandler(object sender, EventArgs e);
        
        protected virtual void OnMyEvent(EventArgs args)
        {
            MyEvent?.Invoke(this, args);
        }
    }

    class Test
    {
        public event EventHandler MyEvent;/*
        {
            add => Console.WriteLine ("add operation");

            remove => Console.WriteLine ("remove operation");
        }       */
    
        static void Lol()
        {
            Test t = new Test();
        
            t.MyEvent += new EventHandler (t.DoNothing);
            t.MyEvent -= null;
        }
    
        void DoNothing (object sender, EventArgs e)
        {
        }

        protected virtual void OnMyEvent()
        {
            MyEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}