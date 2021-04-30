using System;
using HTMLTemplate.core.BL.InterfaceModel;

namespace HTMLTemplate.core.BL.Model
{
    public abstract class BasePlatform : IPlatformProperty
    {
        public abstract PlatformID PlatformId { get; protected internal set; }

        PlatformID IPlatformProperty.PlatformId
        {
            get => this.PlatformId;
            set => this.PlatformId = value;
        }

        public abstract string UserName { get; protected internal set; }

        string IPlatformProperty.UserName
        {
            get => this.UserName;
            set => this.UserName = value;
        }
    }
}