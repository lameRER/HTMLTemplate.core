using System;
using HTMLTemplate.core.BL.Interface;

namespace HTMLTemplate.core.BL.Model
{
    public class RbPrintTemplate : IRbPrintTemplate
    {
        public DateTime CreateDatetime { get; set; }
        public uint? CreatePerson_id { get; set; }
        public DateTime modifyDatetime { get; set; }
        public uint? ModifyPerson_id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Context { get; set; }
        public string FileName { get; set; }
        public string Default { get; set; }
        public string FlatCode { get; set; }
    }
}