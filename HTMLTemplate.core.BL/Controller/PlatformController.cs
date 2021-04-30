using HTMLTemplate.core.BL.Base;
using HTMLTemplate.core.BL.Model;

namespace HTMLTemplate.core.BL.Controller
{
    public class PlatformController : BasePlatformController
    {
        public override Platform Platform { get; } = new();

        public PlatformController() : base()
        {
            
        }
    }
}