#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
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
            GitPull("/home/sasha/VISTA_MED/sasha/templates/");
            var sql = new MysqlController(_connect);
            var actionTypes = sql.GetRbPrintTemplateValue(sql.MySqlConnect());
            if (actionTypes == null) throw new ArgumentNullException(nameof(actionTypes));
            TemplateFile.DirectoryTemplate = string.Concat(GetDirectory(Platform?.Platform), _connect.Name, '/');
            foreach (var at in actionTypes)
            {
                TemplateFile.DirectoryFile = GetFile(TemplateFile.DirectoryTemplate, at.Id, at.Name.Replace(".", "_").Replace(" ", "_").Replace("/", "_"));
                CreateFile(TemplateFile.DirectoryTemplate);
                WriteFile(TemplateFile.DirectoryFile, at.Default);
            }
            AutoCommit("/home/sasha/VISTA_MED/sasha/templates/");
            GitPush("/home/sasha/VISTA_MED/sasha/templates/");
        }

        private static void GitPull(string directory)
        {
            Plug();
            Process.Start("/bin/bash", "-c \"git -C " + directory + " checkout master && git -C " + directory + " fetch upstream master && git -C " + directory + " pull origin master && git -C " + directory + " pull upstream master && git -C " + directory + " checkout AUTO \"");
            Plug();
        }

        private static void GitPush(string directory)
        {
            Plug();
            Process.Start("/bin/bash", "-c \"git -C " + directory + " merge master && git -C " + directory + " checkout master && git -C " + directory + " merge --no-ff AUTO && git -C " + directory + " push origin master && git -C " + directory + " push upstream master\"");
            Plug();
        }

        private static void AutoCommit(string directory)
        {
            Plug();
            Process.Start("/bin/bash", "-c \"git -C "+directory+" add . && git -C "+directory+" commit -m '" + DateTime.Now + "'\"");
            Plug();
        }

        private static void Plug()
        {
            while (File.Exists("/home/sasha/VISTA_MED/sasha/templates/.git/index.lock")) { Thread.Sleep(5000); }
            Thread.Sleep(1000);
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
        private static string GetFile(string? getDirectory, int id, string? docName) => $@"{getDirectory}{id}_{docName}.html";

        private static MysqlController MysqlController(SqlConnectProperty? conn)
        {
            return new(conn);
        }
    }
}
