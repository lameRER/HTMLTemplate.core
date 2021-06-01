using HTMLTemplate.core.BL.Controller;
using HTMLTemplate.core.BL.Model;

namespace HTMLTemplate.core.BL.Interface
{
    public interface IBaseTemplateFile
    {
        public TemplateFile TemplateFile { get; set; }
        public PlatformController? Platform { get; }
    }
}