using System;
using HTMLTemplate.core.BL.Base;
using HTMLTemplate.core.BL.Model;

namespace HTMLTemplate.core.BL.Controller
{
    public sealed class TemplateFileController : BaseTemplateFileController
    {
        public sealed override TemplateFile TemplateFile { get; set; }

        public TemplateFileController()
        {
            TemplateFile.FileName = GetFileName();
            TemplateFile.FileCode = GetFileCode();
            TemplateFile.DirectoryTemplate = GetDirectory(Environment.UserName);
            TemplateFile.
        }
        
    }
}