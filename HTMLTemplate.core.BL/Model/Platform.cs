using System;
using HTMLTemplate.core.BL.Interface;

namespace HTMLTemplate.core.BL.Model
{
    public sealed class Platform : IPlatformProperty
    {
        public PlatformID PlatformId { get; private set; }
        PlatformID IPlatformProperty.PlatformId
        {
            get => this.PlatformId;
            set => this.PlatformId = value;
        }

        public string UserName { get; private set; }

        string IPlatformProperty.UserName
        {
            get => this.UserName;
            set => this.UserName = value;
        }
    }
}