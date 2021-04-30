using System;
using HTMLTemplate.core.BL.Base;
using HTMLTemplate.core.BL.Interface;

namespace HTMLTemplate.core.BL.Model
{
    public sealed class Platform : IPlatform
    {
        public PlatformID PlatformId { get; internal set; }
        PlatformID IPlatform.PlatformId
        {
            get => this.PlatformId;
            set => this.PlatformId = value;
        }

        public string UserName { get; internal set; }
        string IPlatform.UserName
        {
            get => this.UserName;
            set => this.UserName = value;
        }
    }
}