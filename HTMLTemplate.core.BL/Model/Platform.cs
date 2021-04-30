using System;
using HTMLTemplate.core.BL.Base;

namespace HTMLTemplate.core.BL.Model
{
    public sealed class Platform : BasePlatform
    {
        public override PlatformID PlatformId { get; protected internal set; }
        public override string UserName { get; protected internal set; }
    }
}