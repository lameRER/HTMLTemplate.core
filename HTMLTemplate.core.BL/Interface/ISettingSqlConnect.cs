using HTMLTemplate.core.BL.Model;

namespace HTMLTemplate.core.BL.Interface
{
    public interface ISettingSqlConnect
    {
        public abstract string Server {get; set; }
        public abstract int Port {get; set; }
        public abstract string Driver {get; set; }
        public abstract string Name {get; set; }
        public abstract string Database {get; set; }
        public abstract string Username {get; set; }
        public abstract string Password {get; set; }
        public abstract SettingSqlAuthProtocol SqlOptions { get; set; }
    }
}