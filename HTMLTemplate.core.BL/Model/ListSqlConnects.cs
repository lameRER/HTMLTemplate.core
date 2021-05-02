using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using HTMLTemplate.core.BL.Interface;

namespace HTMLTemplate.core.BL.Model
{
    [Serializable]
    public class ListSqlConnects : IListSqlConnects
    {
        [JsonPropertyName("sqltools.connections")]
        public List<SqlConnectProperty> Connects { get; set; }
    }
}