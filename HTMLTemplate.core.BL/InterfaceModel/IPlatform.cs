using System;

namespace HTMLTemplate.core.BL.Base
{
    public interface IPlatform
    {
        public abstract PlatformID PlatformId { get; }
        public abstract string UserName { get; }
        protected internal abstract PlatformID GetPlatform();
        protected internal abstract string GetUserName();
    }
}