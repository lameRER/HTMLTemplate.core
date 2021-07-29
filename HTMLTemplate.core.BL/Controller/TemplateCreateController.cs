#nullable enable
using System;
using System.Collections.Generic;
using System.Reflection;
using HTMLTemplate.core.BL.Base;
using HTMLTemplate.core.BL.Model;
using System.Linq;

namespace HTMLTemplate.core.BL.Controller
{
    public class TemplateCreateController : BaseTemplateFileController
    {
        public sealed override TemplateFile TemplateFile { get; set; } = new();
        public override PlatformController? Platform { get; }
        
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
                TemplateFile.FileName = fileName;
                TemplateFile.FileCode = fileCode;
                Platform = platform ?? throw new ArgumentNullException(nameof(platform));
                _connect = connect;
            }
            catch (Exception e)
            {
                eventNotify.Error(MethodBase.GetCurrentMethod()?.ReflectedType?.Name, e);
            }
        }

        public override void Create()
        {
            var sql = MysqlController(_connect);
            var actionTypes = sql.GetActionTypeValue(sql.MySqlConnect(), TemplateFile.FileName, TemplateFile.FileCode);
            if (actionTypes == null) throw new ArgumentNullException(nameof(actionTypes));
            var actionPropertyTypes = sql.GetActionPropertyTypeValue(sql.MySqlConnect(), actionTypes);
            if (actionPropertyTypes == null) throw new ArgumentNullException(nameof(actionPropertyTypes));
            TemplateFile.TemplateLine = new List<string>();
            TemplateFile.DirectoryTemplate = GetDirectory(Platform?.Platform);
            TemplateFile.DirectoryFile = GetFile(TemplateFile.DirectoryTemplate, TemplateFile.FileCode, TemplateFile.FileName);
            CreateFile(TemplateFile.DirectoryTemplate);
            WriteFile(TemplateFile.DirectoryFile);
            WriteFileProperty(actionPropertyTypes);
        }

        private static MysqlController MysqlController(SqlConnectProperty? conn)
        {
            return new(conn);
        }
    }
}