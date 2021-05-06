#nullable enable
using System;
using System.Collections.Generic;
using System.Reflection;
using HTMLTemplate.core.BL.Model;

namespace HTMLTemplate.core.BL.Controller
{
    public class TemplateCreateController
    {
        private readonly string? _fileName;
        private readonly string? _fileCode;
        private readonly PlatformController? _platform;
        private readonly SqlConnectProperty? _connect;

        public TemplateCreateController(string? fileName, string? fileCode, PlatformController platform,
            SqlConnectProperty connect)
        {
            var eventNotify = new EventNotify();
            try
            {
                if (string.IsNullOrWhiteSpace(fileName))
                    throw new ArgumentException("Value cannot be null or whitespace.", nameof(fileName));
                if (string.IsNullOrWhiteSpace(fileCode))
                    throw new ArgumentException("Value cannot be null or whitespace.", nameof(fileCode));
                _fileName = fileName;
                _fileCode = fileCode;
                _platform = platform ?? throw new ArgumentNullException(nameof(platform));
                _connect = connect;
                var sql = MysqlController(_connect, _fileName, _fileCode);
                TemplateFileController(sql.GetValue(sql.MySqlConnect()), _fileName, _fileCode, _platform);
            }
            catch (Exception e)
            {
                eventNotify.Error(MethodBase.GetCurrentMethod()?.ReflectedType?.Name, e);
            }
        }

        private MysqlController MysqlController(SqlConnectProperty conn, string name, string code)
        {
            return new(conn, name, code);
        }

        private TemplateFileController TemplateFileController(IEnumerable<ActionType> actionTypes, string name, string code, PlatformController pt)
        {
            return new(name, code, pt, actionTypes);
        }
    }
}