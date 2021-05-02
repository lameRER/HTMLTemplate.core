#nullable enable
using System;
using System.Reflection;
using System.Text.Json;
using HTMLTemplate.core.BL.Model;

namespace HTMLTemplate.core.BL.Controller
{
    public class SqlConnectController
    {
        public ListSqlConnects? SqlToolsConnects { get; } = new();

        public SqlConnectController(string getSettingsFile)
        {
            var eventNotify = new EventNotify();
            try
            {
                SqlToolsConnects = SqlToolsDeserialize(getSettingsFile);
            }
            catch (Exception e)
            {
                eventNotify.Error(MethodBase.GetCurrentMethod()?.ReflectedType?.Name, e);
            }
        }

        private static ListSqlConnects? SqlToolsDeserialize(string getSettingsFile)
        {
            return JsonSerializer.Deserialize<ListSqlConnects>(getSettingsFile);
        }
    }
}