using System;
using System.Text.Json.Serialization;

namespace HTMLTemplate
{
    [Serializable]
    public class Connect
    {
        private string _server;
        private int _port;
        private string _driver;
        private string _name;
        private string _database;
        private string _username;
        private string _password;
        private SqlOptions _sqlOptions;

        public SqlOptions SqlOption
        {
            get => _sqlOptions;
            set => _sqlOptions = value;
        }

        [JsonPropertyName("server")]
        public string Server
        {
            get => _server;
            set => _server = value;
        }

        [JsonPropertyName("port")]
        public int Port
        {
            get => _port;
            set => _port = value;
        }

        [JsonPropertyName("driver")]
        public string Driver
        {
            get => _driver;
            set => _driver = value;
        }

        [JsonPropertyName("name")]
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        [JsonPropertyName("database")]
        public string Database
        {
            get => _database;
            set => _database = value;
        }

        [JsonPropertyName("username")]
        public string Username
        {
            get => _username;
            set => _username = value;
        }

        [JsonPropertyName("password")]
        public string Password
        {
            get => _password;
            set => _password = value;
        }
        public Connect()
        {
            
        }
        public Connect(SqlOptions sqlOptions, string server, int port, string driver, string name, string database, string username, string password)
        {
            SqlOption = sqlOptions;
            Server = server;
            Port = port;
            Driver = driver;
            Name = name;
            Database = database;
            Username = username;
            Password = password;
        }
    }
}
