using HTMLTemplate.core.BL.Model;

namespace HTMLTemplate.core.BL.Interface
{
    public interface ISqlConnectProperty
    {
        public string Server {get; set; }
        public int Port {get; set; }
        public string Driver {get; set; }
        public string Name {get; set; }
        public string Database {get; set; }
        public string Username {get; set; }
        public string Password {get; set; }
        public SqlAuthProtocol SqlOptions { get; set; }
    }
}