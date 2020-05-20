using System;

namespace HTMLTemplate
{
    public class EventClass
    {
        public event EventHandler<MyEventArgs> Notify;

        public EventClass()
        {
            Console.WriteLine("Start Event");
        }
        public void Select(string value)
        {
            Notify?.Invoke(this, new MyEventArgs($"{value}: Ok"));
        }
        public void Error(string value, Exception exception)
        {
            Notify?.Invoke(this, new MyEventArgs($"{value}: {exception}"));
        }

    }   
    public class MyEventArgs : EventArgs
    {
        private readonly string _eventInfo;

        public MyEventArgs(string text)
        {
            _eventInfo = text;
        }

        public string GetInfo()
        {
            return _eventInfo;
        }
    }
}
