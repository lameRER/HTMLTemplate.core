#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using HTMLTemplate.core.BL.Model;
using HTMLTemplate.core.BL.Models;

namespace HTMLTemplate.core.BL.Controller
{
    public class MysqlController
    {
        private readonly SqlConnectProperty _connect;

        public MysqlController(SqlConnectProperty? connect)
        {
            _connect = connect ?? throw new ArgumentNullException(nameof(connect));
        }

        public DataBaseContext MySqlConnect()
        {
            return new(_connect);
        }

        public IEnumerable<RbPrintTemplate> GetRbPrintTemplateValue(DataBaseContext context)
        {
            return context.RbPrintTemplates.ToList();
        }
        public IEnumerable<ActionType> GetActionTypeValue(DataBaseContext context, string? fileName, string? fileCode)
        {
            return context.ActionTypes.Where(e => e.Name == fileName && e.Code == fileCode);
        }

        public IEnumerable<ActionPropertyType> GetActionPropertyTypeValue(DataBaseContext context, IEnumerable<ActionType> action)
        {
            return context.ActionPropertyTypes.Where(e => e.ActionTypeId == action.Select(a => a.Id).First());
        }

        public IEnumerable<ActionPropertyType> GetActionPropertyTypeValue(DataBaseContext context, IEnumerable<ActionType> action)
        {
            return context.ActionPropertyTypes.Where(e => e.ActionTypeId == action.Select(a => a.Id).First());
        }
    }
}