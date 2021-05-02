using System;
using System.Text.Json.Serialization;
using HTMLTemplate.core.BL.Interface;

namespace HTMLTemplate.core.BL.Model
{
    [Serializable]
    public class SqlConnectProperty : ISqlConnectProperty
    {
        [JsonPropertyName("server")]
        public string Server { get; set; }
        [JsonPropertyName("port")] 
        public int Port { get; set; }
        [JsonPropertyName("driver")]
        public string Driver { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("database")]
        public string Database { get; set; }
        [JsonPropertyName("username")]
        public string Username { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
        [JsonPropertyName("mysqlOptions")]
        public SqlAuthProtocol SqlOptions { get; set; }
    }
}