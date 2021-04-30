using HTMLTemplate.core.BL.Interface;

namespace HTMLTemplate.core.BL.Model
{
    public class TemplateFile : ITemplateFile
    {
        public string DirectoryFile { get; set; }
        public string DirectoryTemplate { get; set; }
        public string FileName { get; set; }
        public string FileCode { get; set; }
    }
}