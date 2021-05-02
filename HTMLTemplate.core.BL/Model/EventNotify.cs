using System;
using System.Data;

namespace HTMLTemplate.core.BL.Model
{
    public class EventNotify
    {
        public event EventHandler<MyEventArgs> Notify;

        public EventNotify()
        {
            Notify += EventNotify.event_Notify;
            // Console.WriteLine("Start Event");
        }
        public void Select(string value)
        {
            Notify?.Invoke(this, new MyEventArgs($"[OK] {value}"));
        }
        public void Error(string value, Exception exception)
        {
            Notify?.Invoke(this, new MyEventArgs($"[ERROR] {value}: {exception}"));
        }
        
        public static void Mysql_StateChange(object sender, StateChangeEventArgs e)
        {
            Console.ResetColor();
            Console.Write("Mysql Connect: ");
            switch (e.CurrentState)
            {
                case ConnectionState.Open:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case ConnectionState.Connecting:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case ConnectionState.Closed:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                default:
                    Console.ResetColor();
                    break;
            }
            Console.WriteLine(e.CurrentState);
        }

        private static void event_Notify(object sender, MyEventArgs e)
        {
            var text = e.GetInfo().Split(" ");
            Console.ForegroundColor = text[0].ToUpper() == "[OK]" ? ConsoleColor.Green : ConsoleColor.Red;
            foreach (var variable in e.GetInfo().Split(" "))
            {
                Console.Write(variable);
                Console.Write(" ");
                Console.ResetColor();
            }
            Console.WriteLine();
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