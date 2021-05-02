using System.Linq;
using HTMLTemplate.core.BL.Controller;
using HTMLTemplate.core.BL.Model;
using NUnit.Framework;

namespace HTMLTemplate.core.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void MysqlStatic()
        {
            var platform = new PlatformController();
            var sqlTools = new SqlConnectController(platform.GetSettingsFile());
            string text = null;
            const string fileCode = "16490-1";
            const string fileName = "Рентгенография всего черепа, в одной или более проекциях";
            var context = new DataBaseContext(sqlTools.SqlToolsConnects?.Connects[0]);
            var printTemplate = context.ActionTypes.Where(e => e.Name == fileName &&  e.Code == fileCode);
            foreach (var item in printTemplate)
            {
                text =item.Name;
            }
            Assert.AreEqual(fileName, text);
        }
    }
}