using System;

namespace HTMLTemplate.core.BL.InterfaceModel
{
    internal interface IPlatformProperty
    {
        public abstract PlatformID PlatformId { get; internal set; }
        public abstract string UserName { get; internal set; }
    }
}