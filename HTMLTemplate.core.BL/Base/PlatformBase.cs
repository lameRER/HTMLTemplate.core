using System;
using System.ComponentModel;

namespace HTMLTemplate.core.BL.Base
{
    public abstract class PlatformBase : IPlatform
    {
        public virtual PlatformID PlatformId { get; }

        public virtual string UserName { get; }

        PlatformID IPlatform.GetPlatform()
        {
            return Environment.OSVersion.Platform;
        }

        string IPlatform.GetUserName()
        {
            return Environment.UserName;
        }
    }
}