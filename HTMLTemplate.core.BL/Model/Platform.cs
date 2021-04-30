using System;
using HTMLTemplate.core.BL.Base;

namespace HTMLTemplate.core.BL.Model
{
    public sealed class Platform : PlatformBase
    {
        public sealed override PlatformID PlatformId { get; }
        public override string UserName { get; }

        public Platform()
        {
            PlatformId = ((IPlatform) this).GetPlatform();
            UserName = ((IPlatform) this).GetUserName();
        }
    }
}