using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using HTMLTemplate.core.BL.Base;
using HTMLTemplate.core.BL.Interface;
using HTMLTemplate.core.BL.Model;

namespace HTMLTemplate.core.BL.Controller
{
    public class SqlToolsController : BaseSqlToolsController
    {
        public override SettingSqlToolsConnects SqlToolsConnects { get; set; }

        public SqlToolsController()
        {
            
        }
        
        public override SettingSqlToolsConnects SqlToolsDeserialize(string getSettingsFile)
        {
            return JsonSerializer.Deserialize<SettingSqlToolsConnects>(getSettingsFile);   
        }
    }
}