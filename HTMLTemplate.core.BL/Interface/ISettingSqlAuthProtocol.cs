using System.Text.Json.Serialization;

namespace HTMLTemplate.core.BL.Interface
{
    public interface ISettingSqlAuthProtocol
    {
        public abstract string AuthProtocol { get; set; }
    }
}