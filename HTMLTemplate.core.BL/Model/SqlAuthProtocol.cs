using System;
using System.Text.Json.Serialization;
using HTMLTemplate.core.BL.Interface;

namespace HTMLTemplate.core.BL.Model
{
    [Serializable]
    public class SqlAuthProtocol : ISqlAuthProtocol
    {
        [JsonPropertyName("authProtocol")]
        public string AuthProtocol { get; set; }
    }
}