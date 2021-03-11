using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;


namespace HTMLTemplate
{
    [Serializable]
    public class SqlTools
    {
        private Connect _connect;

        [JsonPropertyName("sqltools.connections")]
        public List<Connect> Connections { get; set; }

        public SqlTools()
        {
        }
        
        public SqlTools(Connect connect)
        {
            _connect = connect;
        }
    }
}
