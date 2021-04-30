using System;

namespace HTMLTemplate.core.BL.Interface
{
    internal interface IPlatform
    {
        public abstract PlatformID PlatformId { get; internal set; }
        public abstract string UserName { get; internal set; }
    }
}