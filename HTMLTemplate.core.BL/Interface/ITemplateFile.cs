using System.Collections.Generic;

namespace HTMLTemplate.core.BL.Interface
{
    public interface ITemplateFile
    {
        public string DirectoryFile {get; set;}
        public string DirectoryTemplate {get; set;}
        public string FileName {get; set;}
        public string FileCode {get; set;}
        public List<string> TemplateLine { get; set; }
    }
}