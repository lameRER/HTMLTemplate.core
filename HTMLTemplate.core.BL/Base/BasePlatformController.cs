using System;
using HTMLTemplate.core.BL.InterfaceController;
using HTMLTemplate.core.BL.Model;

namespace HTMLTemplate.core.BL.Base
{
    public abstract class BasePlatformController :  IPlatformMethod
    {
        public abstract Platform Platform { get; }

        protected BasePlatformController()
        {
            if (Platform != null) Platform.PlatformId = ((IPlatformMethod) this).GetPlatform();
            if (Platform != null) Platform.UserName = ((IPlatformMethod) this).GetUserName();
        }

        PlatformID IPlatformMethod.GetPlatform()
        {
            return Environment.OSVersion.Platform;
        }

        string IPlatformMethod.GetUserName()
        {
            return Environment.UserName;
        }
    }
}