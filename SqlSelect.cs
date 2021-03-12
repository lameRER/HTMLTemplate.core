using System;
using System.Data.Common;
using System.Reflection;
using MySql.Data.MySqlClient;

namespace HTMLTemplate
{
    public class SqlSelect 
    {
        private SqlConnect _connect;
        private MySqlDataReader _reader;

        private SqlConnect Connect
        {
            get => _connect;
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                _connect = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        public MySqlDataReader Reader
        {
            get => _reader;
            private set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                _reader = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        public SqlSelect(SqlConnect connect, string sqlSelect)
        {
            var eventclass = new EventClass();
            try
            {
                Connect = connect;
                var sql =  SqlConnection(Connect);
                SqlState(sql);
                sql.Open();
                Reader = SqlCommand(sqlSelect, sql).ExecuteReader();
                eventclass.Select(MethodBase.GetCurrentMethod()?.ReflectedType?.Name);
            }
            catch (Exception e)
            {
                eventclass.Error(MethodBase.GetCurrentMethod()?.ReflectedType?.Name, e);
            }
        }

        private MySqlConnection SqlConnection(SqlConnect connect) => new MySqlConnection($"Server={connect.Server}; database={connect.Database}; UID={connect.Username}; port={connect.Port}; password={connect.Password}");

        private void SqlState(MySqlConnection mySqlConnection)
        {
            if (mySqlConnection != null) mySqlConnection.StateChange += EventClass.Mysql_StateChange;
        }

        private MySqlCommand SqlCommand(string sqlSelect, MySqlConnection sqlConnection) => new MySqlCommand(sqlSelect, sqlConnection);
    }
}