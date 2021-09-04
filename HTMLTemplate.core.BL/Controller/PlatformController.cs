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
            if (((IPlatformProperty)Platform).PlatformId == PlatformID.Unix)
                return File.ReadAllText(
                    Environment.ExpandEnvironmentVariables($"%HOME%/.config/Code/User/settings.json"));
            if (((IPlatformProperty)Platform).PlatformId == PlatformID.Win32Windows)
                return File.ReadAllText(
                    Environment.ExpandEnvironmentVariables(@"%APPDATA%\Code\User\settings.json"));

            if(File.Exists(Environment.ExpandEnvironmentVariables($"{Directory.GetCurrentDirectory()}/settings.json")))
                return File.ReadAllText(
                    Environment.ExpandEnvironmentVariables($"{Directory.GetCurrentDirectory()}/settings.json"));
            return null;
        }
    }
}