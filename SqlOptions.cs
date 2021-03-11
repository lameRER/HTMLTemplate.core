using System;
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
            set => _authProtocol = value;
        }

        public SqlOptions(string authProtocol)
        {
            AuthProtocol = authProtocol;
        }
    }
}
