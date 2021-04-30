using System;
using System.IO;
using HTMLTemplate.core.BL.Base;
using HTMLTemplate.core.BL.Model;

namespace HTMLTemplate.core.BL.Controller
{
    public class SettingController : BaseSetting
    {
        public override string GetSettingsFile()
        {
            return Environment.OSVersion.Platform switch
            {
                PlatformID.Unix => File.ReadAllText(
                    Environment.ExpandEnvironmentVariables($"%HOME%/.config/Code/User/settings.json")),
                PlatformID.Win32Windows => File.ReadAllText(
                    Environment.ExpandEnvironmentVariables(@"%APPDATA%\Code\User\settings.json")),
                _ => File.ReadAllText(
                    Environment.ExpandEnvironmentVariables($"{Directory.GetCurrentDirectory()}/settings.json"))
            };
        }
    }
}