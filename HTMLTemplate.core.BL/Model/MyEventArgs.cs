using System;
using System.Data;

namespace HTMLTemplate.core.BL.Model
{
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