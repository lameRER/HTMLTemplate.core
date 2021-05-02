using System.Collections.Generic;
using HTMLTemplate.core.BL.Model;

namespace HTMLTemplate.core.BL.Interface
{
    public interface IListSqlConnects
    {
        public List<SqlConnectProperty> Connects { get; }
    }
}