using System;
using System.IO;
using System.Text;
using HTMLTemplate.core.BL.Model;

namespace HTMLTemplate.core.BL.Base
{
    public abstract class BaseTemplateFileController
    {
        public abstract TemplateFile TemplateFile { get; set; }
        
        protected virtual void WriteFile(string file)
        {
            var html = new FileStream(file, FileMode.Create); //создаем файловый поток
            var htmlWriterCreate = new StreamWriter(html, Encoding.GetEncoding("UTF-8")); // соединяем файловый поток с "Потоковым писателем"
            htmlWriterCreate.Close();
        }

        protected virtual string GetDirectory(string getUserName)
        {
            return $@"/run/media/sasha/OS/VISTA_MED/{getUserName}/templates/";
        }
        protected virtual string GetFile(string getDirectory, string docContext, string docName)
        {
            return $@"{getDirectory}{docContext}_{docName}.html";
            // return Environment.ExpandEnvironmentVariables($@"%HOME%/VISTA_MED/{getUserName}/templates/{docContext}_{docName}.html");
        }

        protected virtual void Create(string directory)
        {
            if (FileExist(directory)) Directory.CreateDirectory(directory);
        }

        protected virtual bool FileExist(string file)
        {
            return !Directory.Exists(file);
        }

        protected virtual string GetFileName()
        {
            Console.Write("Имя документа: ");
            var writeTextName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(writeTextName))
                return writeTextName;
            else
                throw new ArgumentNullException(nameof(writeTextName));
        }

        protected virtual string GetFileCode()
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