using System;
using System.Collections.Generic;
using HTMLTemplate.core.BL.Model;

#nullable disable

namespace HTMLTemplate.core.BL.Models
{
    public partial class RbPrintTemplate
    {
        public int Id { get; set; }
        public DateTime CreateDatetime { get; set; }
        public int? CreatePersonId { get; set; }
        public DateTime ModifyDatetime { get; set; }
        public int? ModifyPersonId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Context { get; set; }
        public string FileName { get; set; }
        public string Default { get; set; }
        public bool DpdAgreement { get; set; }
        public bool Type { get; set; }
        public bool HideParam { get; set; }
        public bool BanUnkeptDate { get; set; }
        public int? CounterId { get; set; }
        public sbyte Deleted { get; set; }
        public bool? IsPatientAgreed { get; set; }
        public string GroupName { get; set; }
        public int? DocumentTypeId { get; set; }
        public bool? IsEditableInWeb { get; set; }
        public string PageOrientation { get; set; }

        public virtual Person CreatePerson { get; set; }
        public virtual Person ModifyPerson { get; set; }
    }
}
