using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace HTMLTemplate
{
    [Serializable]
    public class SqlTools
    {

        [JsonPropertyName("sqltools.connections")]
        public List<Connect> Connections { get; set; }

        public SqlTools()
        {
            
        }
        public static SqlTools SqlToolsDeserialize(string getSettingsFile)
        {
            return JsonSerializer.Deserialize<SqlTools>(getSettingsFile);
        }
    }
}
