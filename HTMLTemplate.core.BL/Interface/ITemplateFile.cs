namespace HTMLTemplate.core.BL.Interface
{
    public interface ITemplateFile
    {
        public abstract string DirectoryFile {get; set;}
        public abstract string DirectoryTemplate {get; set;}
        public abstract string FileName {get; set;}
        public abstract string FileCode {get; set;}
    }
}