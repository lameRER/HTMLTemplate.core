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
        private readonly PlatformController _platform;
        private readonly SqlConnectProperty _connect;

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
                var sql = MysqlController();
                TemplateFileController(sql.GetValue(sql.MySqlConnect()));
            }
            catch (Exception e)
            {
                eventNotify.Error(MethodBase.GetCurrentMethod()?.ReflectedType?.Name, e);
            }
        }

        private MysqlController MysqlController()
        {
            return new(_connect, _fileName, _fileCode);
        }

        private TemplateFileController TemplateFileController(IEnumerable<ActionType> actionTypes)
        {
            return new(_fileName, _fileCode, _platform, actionTypes);
        }
    }
}