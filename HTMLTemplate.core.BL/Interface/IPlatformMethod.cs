using System;

namespace HTMLTemplate.core.BL.InterfaceController
{
    internal interface IPlatformMethod
    {
        public abstract PlatformID GetPlatform();
        public abstract string GetUserName();
    }
}