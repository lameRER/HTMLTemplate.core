using System.Linq;
using HTMLTemplate.core.BL.Controller;
using HTMLTemplate.core.BL.Model;
using NUnit.Framework;

namespace HTMLTemplate.core.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup(){}

        [Test]
        public void MysqlStatic()
        {
            var platform = new PlatformController();
            var sqlTools = new SqlConnectController(platform.GetSettingsFile());
            string text = null;
            const string fileCode = "nevr";
            const string fileName = "Осмотр врача-невролога";
            var context = new DataBaseContext(sqlTools.SqlToolsConnects?.Connects[34]);
            var printTemplate = context.ActionTypes.Where(e => e.Name == fileName &&  e.Code == fileCode);
            foreach (var item in printTemplate)
            {
                text =item.Name;
            }

            Assert.AreEqual(fileName, text);
        }

        [Test]
        public void TemplateCreateControllerTest()
        {
            var platform = new PlatformController();
            var sqlTools = new SqlConnectController(platform.GetSettingsFile());
            var connect = sqlTools.SqlToolsConnects?.Connects[34];
            const string fileCode = "nevr";
            const string fileName = "Осмотр врача-невролога";
            var templateReadAllController = new TemplateReadAllController(connect,platform);
            templateReadAllController.Create();
            var templateCreateController = new TemplateCreateController(fileName, fileCode, platform, connect);
            templateCreateController.Create();
            Assert.Pass(templateCreateController.TemplateFile.DirectoryFile);
        }
    }
}