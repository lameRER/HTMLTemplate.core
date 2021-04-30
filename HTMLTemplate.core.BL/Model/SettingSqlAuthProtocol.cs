using System;
using System.Text.Json.Serialization;
using HTMLTemplate.core.BL.Interface;

namespace HTMLTemplate.core.BL.Model
{
    [Serializable]
    public class SettingSqlAuthProtocol : ISettingSqlAuthProtocol
    {
        [JsonPropertyName("authProtocol")]
        public string AuthProtocol { get; set; }
    }
}