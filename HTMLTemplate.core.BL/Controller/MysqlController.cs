#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using HTMLTemplate.core.BL.Model;

namespace HTMLTemplate.core.BL.Controller
{
    public class MysqlController 
    {
        private readonly SqlConnectProperty _connect;
        private readonly string? _fileName;
        private readonly string? _fileCode;

        public MysqlController(SqlConnectProperty? connect, string? fileName, string? fileCode)
        {
            _connect = connect ?? throw new ArgumentNullException(nameof(connect));
            _fileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
            _fileCode = fileCode ?? throw new ArgumentNullException(nameof(fileCode));
        }

        public DataBaseContext MySqlConnect()
        {
            return new(_connect);
        }
        
        public IEnumerable<ActionType> GetValue(DataBaseContext context)
        {
            return context.ActionTypes.Where(e => e.Name == _fileName &&  e.Code == _fileCode);
        }
    }
}