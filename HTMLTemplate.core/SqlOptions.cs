using System;
using System.Reflection;
using System.Text.Json.Serialization;

namespace HTMLTemplate
{
    [Serializable]
    public class SqlOptions
    {
        private string _authProtocol;

        [JsonPropertyName("authProtocol")]
        public string AuthProtocol
        {
            get => _authProtocol;
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                _authProtocol = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        public SqlOptions()
        {
            var eventclass = new EventClass();
            try
            {
                eventclass.Select(MethodBase.GetCurrentMethod()?.ReflectedType?.Name);
            }
            catch (Exception e)
            {
                eventclass.Error(MethodBase.GetCurrentMethod()?.ReflectedType?.Name, e);
            }
        }
    }
}
