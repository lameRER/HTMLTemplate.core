using System;
using HTMLTemplate.core.BL.Model;

namespace HTMLTemplate.core.BL.Base
{
    public abstract class BasePlatformController 
    {
        public abstract Platform Platform { get; }

        protected BasePlatformController()
        {
            if (Platform != null) Platform.PlatformId = GetPlatform();
            if (Platform != null) Platform.UserName = GetUserName();
        }

        private static PlatformID GetPlatform()
        {
            return Environment.OSVersion.Platform;
        }

        private static string GetUserName()
        {
            return Environment.UserName;
        }
    }
}