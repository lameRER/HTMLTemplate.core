using System.Collections.Generic;
using HTMLTemplate.core.BL.Model;

namespace HTMLTemplate.core.BL.Interface
{
    public interface ISettingSqlToolsConnects
    {
        public abstract List<SettingSqlConnect> Connects { get; }
    }
}