using System;

namespace HTMLTemplate.core.BL.Interface
{
    public interface IRbPrintTemplate
    {
        public abstract DateTime CreateDatetime {get; set;}
        public abstract uint? CreatePerson_id { get; set; }
        public abstract DateTime modifyDatetime {get; set;} 
        public abstract uint? ModifyPerson_id {get; set;} 
        public abstract string Code {get; set;} 
        public abstract string Name {get; set;} 
        public abstract string Context {get; set;} 
        public abstract string FileName {get; set;} 
        public abstract string Default {get; set;}
        
        public abstract string FlatCode { get; set; }
    }
}