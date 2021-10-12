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
            GitPull(GetDirectory());
            var sql = new MysqlController(_connect);
            var actionTypes = sql.GetRbPrintTemplateValue(sql.MySqlConnect());
            if (actionTypes == null) throw new ArgumentNullException(nameof(actionTypes));
            TemplateFile.DirectoryTemplate = string.Concat(GetDirectory(), _connect.Name, '/');
            foreach (var at in actionTypes)
            {
                TemplateFile.DirectoryFile = GetFile(TemplateFile.DirectoryTemplate, at.Id, at.Name.Replace(".", "_").Replace(" ", "_").Replace("/", "_"));
                CreateFile(TemplateFile.DirectoryTemplate);
                WriteFile(TemplateFile.DirectoryFile, at.Default);
            }
            AutoCommit(GetDirectory());
            GitPush(GetDirectory());
        }

        private static void GitPull(string directory)
        {
            Plug(directory);
            Process.Start("/bin/bash", "-c \"git -C " + directory + " checkout master && git -C " + directory + " pull && git -C " + directory + " checkout AUTO \"");
            Plug(directory);
        }

        private static void GitPush(string directory)
        {
            Plug(directory);
            Process.Start("/bin/bash", "-c \"git -C " + directory + " merge master && git -C " + directory + " checkout master && git -C " + directory + " merge --no-ff AUTO && git -C " + directory + " push \"");
            Plug(directory);
        }

        private static void AutoCommit(string directory)
        {
            Plug(directory);
            Process.Start("/bin/bash", "-c \"git -C "+directory+" add . && git -C "+directory+" commit -m '" + DateTime.Now + "'\"");
            Plug(directory);
        }

        private static void Plug(string directory)
        {
            while (File.Exists(directory + ".git/index.lock")) { Thread.Sleep(5000); }
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
