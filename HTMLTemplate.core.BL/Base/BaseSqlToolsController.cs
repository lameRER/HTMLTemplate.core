using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using HTMLTemplate.core.BL.Interface;
using HTMLTemplate.core.BL.Model;

namespace HTMLTemplate.core.BL.Base
{
    [Serializable]
    public abstract class BaseSqlToolsController
    {
        public abstract SettingSqlToolsConnects SqlToolsConnects { get; set; }
        public abstract SettingSqlToolsConnects SqlToolsDeserialize(string getSettingsFile);
    }
}