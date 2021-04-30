using System;
using System.Reflection;

namespace HTMLTemplate
{
    public class SystemVariables
    {
        private string _userName;

        public string UserName
        {
            get => _userName;
            private set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                _userName = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        public SystemVariables()
        {
            var eventclass = new EventClass();
            try
            {
                UserName = GetUserName();
                eventclass.Select(MethodBase.GetCurrentMethod()?.ReflectedType?.Name);
            }
            catch (Exception e)
            {
                eventclass.Error(MethodBase.GetCurrentMethod()?.ReflectedType?.Name, e);
            }
        }

        private static string GetUserName() => Environment.UserName;
    }
}