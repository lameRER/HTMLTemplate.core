using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using HTMLTemplate.core.BL.Interface;

namespace HTMLTemplate.core.BL.Model
{
    [Serializable]
    public class SettingSqlToolsConnects : ISettingSqlToolsConnects
    {
        [JsonPropertyName("sqltools.connections")]
        public List<SettingSqlConnect> Connects { get; set; }

        
        
        public SettingSqlToolsConnects():base(){}
    }
}