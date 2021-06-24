#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using HTMLTemplate.core.BL.Base;
using HTMLTemplate.core.BL.Model;

namespace HTMLTemplate.core.BL.Controller
{
    public class TemplateReadAllController : BaseTemplateFileController
    {
        private readonly SqlConnectProperty _connect;
        public override TemplateFile TemplateFile { get; set; } = new();
        public override PlatformController? Platform { get; }

        public TemplateReadAllController(SqlConnectProperty connect, PlatformController? platform)
        {
            _connect = connect ?? throw new ArgumentNullException(nameof(connect));
            Platform = platform ?? throw new ArgumentNullException(nameof(platform));
        }

        public override void Create()
        {
            var sql = new MysqlController(_connect);
            var actionTypes = sql.GetRbPrintTemplateValue(sql.MySqlConnect());
            if (actionTypes == null) throw new ArgumentNullException(nameof(actionTypes));
            TemplateFile.DirectoryTemplate = string.Concat(GetDirectory(Platform?.Platform), _connect.Name, '/');
            foreach (var at in actionTypes)
            {
                TemplateFile.DirectoryFile = GetFile(TemplateFile.DirectoryTemplate, _connect, at.Id, at.Name.Replace(".", "_").Replace(" ", "_").Replace("/", "_"));
                CreateFile(TemplateFile.DirectoryTemplate);
                WriteFile(TemplateFile.DirectoryFile, at.Default);
            }
        }

        private static void WriteFile(string file, string templateFileDirectoryFile)
        {
            var html = new FileStream(file, FileMode.Create);
            var htmlWriterCreate =
                new StreamWriter(html,
                    Encoding.GetEncoding("UTF-8"));
            htmlWriterCreate.Write(templateFileDirectoryFile);
            htmlWriterCreate.Close();
        }
        private static string GetFile(string? getDirectory, SqlConnectProperty connect, int id, string? docName) => $@"{getDirectory}/{id}_{docName}.html";

        private static MysqlController MysqlController(SqlConnectProperty? conn)
        {
            return new(conn);
        }
    }
}
