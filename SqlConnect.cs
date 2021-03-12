using System;
using System.Reflection;
using System.Text.Json.Serialization;

namespace HTMLTemplate
{
    [Serializable]
    public class SqlConnect
    {
        private string _server;
        private int _port;
        private string _driver;
        private string _name;
        private string _database;
        private string _username;
        private string _password;
        private SqlOptions _sqlOptions;

        [JsonPropertyName("mysqlOptions")]
        public SqlOptions SqlOption
        {
            get => _sqlOptions;
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                _sqlOptions = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        [JsonPropertyName("server")]
        public string Server
        {
            get => _server;
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                _server = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        [JsonPropertyName("port")]
        public int Port
        {
            get => _port;
            set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value));
                _port = value;
            }
        }

        [JsonPropertyName("driver")]
        public string Driver
        {
            get => _driver;
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                _driver = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        [JsonPropertyName("name")]
        public string Name
        {
            get => _name;
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                _name = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        [JsonPropertyName("database")]
        public string Database
        {
            get => _database;
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                _database = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        [JsonPropertyName("username")]
        public string Username
        {
            get => _username;
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                _username = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        [JsonPropertyName("password")]
        public string Password
        {
            get => _password;
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                _password = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        public SqlConnect()
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
