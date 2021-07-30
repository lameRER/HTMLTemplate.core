using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using HTMLTemplate.core.BL.Controller;
using HTMLTemplate.core.BL.Interface;
using HTMLTemplate.core.BL.Model;

namespace HTMLTemplate
{
    public static class Program
    {
        internal static void Main()
        {
            StartUp();
        }

        private static void StartUp()
        {
            var eventNotify = new EventNotify();
            try
            {
                var platform = new PlatformController();
                var sqlTools = new SqlConnectController(platform.GetSettingsFile());
                ReadConnect(sqlTools.SqlToolsConnects);
                var i = (int) GetConnect();
                if (sqlTools.SqlToolsConnects == null || i < 0 || sqlTools.SqlToolsConnects?.Connects.Count <= i)
                {
                    throw new Exception("Неверное значение!");
                }
                GetFunction(sqlTools.SqlToolsConnects?.Connects[i], platform);
            }
            catch (Exception e)
            {
                eventNotify.Error(MethodBase.GetCurrentMethod()?.ReflectedType?.Name, e);
            }
            finally
            {
                StartUp();
            }
        }
        private static int? GetConnect()
        {
            if (int.TryParse(Console.ReadLine(), out var read)) return read - 1;
            return null;
        }

        private static void ReadConnect(IListSqlConnects sql)
        {
            var i = 1;
            foreach (var item in sql.Connects) WriteLineConnect(i++, item.Name);
            Console.Write("Выберите подключение:");
        }

        private static void WriteLineConnect(int i, string itemName)
        {
            Console.WriteLine("{0}. {1}", i, itemName);
        }

        private static void GetFunction(SqlConnectProperty connect, PlatformController platform)
        {
            var countries = new Dictionary<int, string>(10) {{1, "Загрузка шаблонов печати"},{2, "Создание шаблона печати"}};
            foreach (var (key, value) in countries) Console.WriteLine($"{key}. {value}");
            Console.Write("Value: ");
            switch (Convert.ToInt32(Console.ReadLine()))
            {
                case 1:
                    TemplateReadAllController(connect,platform).Create();
                    break;
                case 2:
                    Console.Write("Имя документа: ");
                    var fileName = Console.ReadLine();
                    Console.Write("Код документа: ");
                    var fileCode = Console.ReadLine();
                    TemplateCreateController(fileName, fileCode, platform, connect).Create();
                    break;
                default:
                    throw new Exception("Неверное значение!");
            }
        }
        private static TemplateReadAllController TemplateReadAllController(SqlConnectProperty connect, PlatformController platform) => new(connect, platform);
        private static TemplateCreateController TemplateCreateController(string fileName, string fileCode, PlatformController platform, SqlConnectProperty connect) => new(fileName, fileCode, platform, connect);
    }
}
