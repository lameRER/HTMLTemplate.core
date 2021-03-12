using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace HTMLTemplate
{
    [Serializable]
    public class SqlTools
    {
        private List<SqlConnect> _connections;

        [JsonPropertyName("sqltools.connections")]
        public List<SqlConnect> Connections
        {
            get => _connections;
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                _connections = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        public SqlTools()
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
        public static SqlTools SqlToolsDeserialize(string getSettingsFile) => JsonSerializer.Deserialize<SqlTools>(getSettingsFile);
    }
}
