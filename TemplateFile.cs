using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace HTMLTemplate
{
    public class TemplateFile 
    {
        private string _directoryFile;
        private string _directoryTemplate;
        private string _fileName;
        private string _fileCode;

        public string DirectoryFile
        {
            get => _directoryFile;
            private set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                _directoryFile = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        public string DirectoryTemplate
        {
            get => _directoryTemplate;
            private set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                _directoryTemplate = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        public string FileName
        {
            get => _fileName;
            private set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                _fileName = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        public string FileCode
        {
            get => _fileCode;
            private set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                _fileCode = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        public TemplateFile()
        {
            var eventclass = new EventClass();
            try
            {
                var systemVariables = new SystemVariables();
                FileName = GetFileName();
                FileCode = GetFileCode();
                DirectoryTemplate = GetDirectory(systemVariables.UserName);
                DirectoryFile = GetFile(DirectoryTemplate, FileCode, FileName);
                eventclass.Select(MethodBase.GetCurrentMethod()?.ReflectedType?.Name);
                Create(DirectoryTemplate);
                WriteFile(DirectoryFile);
            }
            catch (Exception e)
            {
                eventclass.Error(MethodBase.GetCurrentMethod()?.ReflectedType?.Name, e);
            }
        }

        private static void WriteFile(string file)
        {
            var html = new FileStream(file, FileMode.Create); //создаем файловый поток
            var htmlWriterCreate = new StreamWriter(html, Encoding.GetEncoding("UTF-8")); // соединяем файловый поток с "Потоковым писателем"
            htmlWriterCreate.Close();
        }

        private static string GetDirectory(string getUserName)
        {
            return $@"C:\VISTA_MED\{getUserName}\templates\";
        }
        private static string GetFile(string getDirectory, string docContext, string docName)
        {
            return $@"{getDirectory}{docContext}_{docName}.html";
            // return Environment.ExpandEnvironmentVariables($@"%HOME%/VISTA_MED/{getUserName}/templates/{docContext}_{docName}.html");
        }

        private static void Create(string directory)
        {
            if (FileExist(directory)) Directory.CreateDirectory(directory);
        }

        private static bool FileExist(string file)
        {
            return !Directory.Exists(file);
        }

        private static string GetFileName()
        {
            Console.Write("Имя документа: ");
            var writeTextName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(writeTextName))
                return writeTextName;
            else
                throw new ArgumentNullException(nameof(writeTextName));
        }

        private static string GetFileCode()
        {
            Console.Write("Код документа: ");
            var writeTextCode = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(writeTextCode))
                return writeTextCode;
            else
                throw new ArgumentNullException(nameof(writeTextCode));
        }
    }
}