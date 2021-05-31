#nullable enable
using System.Collections.Generic;
using HTMLTemplate.core.BL.Interface;
using HTMLTemplate.core.BL.Model;

namespace HTMLTemplate.core.BL.Base
{
    public abstract class BaseTemplateFileController : IBaseTemplateFile
    {
        public abstract TemplateFile TemplateFile { get; set; }
        protected abstract void WriteFile(string file);
        protected abstract string? GetDirectory(Platform? getUserName);
        protected abstract string GetFile(string? getDirectory, string docContext, string docName);
        protected abstract void Create(string? directory);
        protected abstract bool FileExist(string? file);
        protected abstract string GetFileName(string? fileName);
        protected abstract string GetFileCode(string? fileCode);
        protected abstract void HtmlWriter(char item);
        public abstract void WriteTemplate(IEnumerable<ActionType> listProperty);
    }
}