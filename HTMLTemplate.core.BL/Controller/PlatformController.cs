using System;
using System.IO;
using System.Reflection;
using HTMLTemplate.core.BL.Interface;
using HTMLTemplate.core.BL.Model;

namespace HTMLTemplate.core.BL.Controller
{
    public class PlatformController 
    {
        public Platform Platform { get; } = new();
        public PlatformController()
        {
            var eventNotify = new EventNotify();
            try
            {
                if (Platform != null) ((IPlatformProperty) Platform).PlatformId = GetPlatform();
                if (Platform != null) ((IPlatformProperty) Platform).UserName = GetUserName();
            }
            catch (Exception e)
            {
                eventNotify.Error(MethodBase.GetCurrentMethod()?.ReflectedType?.Name, e);
            }
        }

        private static PlatformID GetPlatform()
        {
            return Environment.OSVersion.Platform;
        }

        private static string GetUserName()
        {
            return Environment.UserName;
        }
        public string GetSettingsFile()
        {
            return ((IPlatformProperty) Platform).PlatformId switch
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