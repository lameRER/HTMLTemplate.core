using System;
using System.Collections.Generic;
using HTMLTemplate.core.BL.Models;

#nullable disable

namespace HTMLTemplate.core.BL.Model
{
    public partial class Person
    {
        public Person()
        {
            InverseCreatePerson = new HashSet<Person>();
            InverseModifyPerson = new HashSet<Person>();
            RbPrintTemplateCreatePeople = new HashSet<RbPrintTemplate>();
            RbPrintTemplateModifyPeople = new HashSet<RbPrintTemplate>();
        }

        public int Id { get; set; }
        public DateTime CreateDatetime { get; set; }
        public int? CreatePersonId { get; set; }
        public DateTime ModifyDatetime { get; set; }
        public int? ModifyPersonId { get; set; }
        public bool Deleted { get; set; }
        public string Code { get; set; }
        public string FederalCode { get; set; }
        public string RegionalCode { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string PatrName { get; set; }
        public int? PostId { get; set; }
        public int? SpecialityId { get; set; }
        public int? OrgId { get; set; }
        public int? OrgStructureId { get; set; }
        public string Office { get; set; }
        public string Office2 { get; set; }
        public int? TariffCategoryId { get; set; }
        public int? FinanceId { get; set; }
        public DateTime? RetireDate { get; set; }
        public short AmbPlan { get; set; }
        public short AmbPlan2 { get; set; }
        public short AmbNorm { get; set; }
        public short HomPlan { get; set; }
        public short HomPlan2 { get; set; }
        public short HomNorm { get; set; }
        public short ExpPlan { get; set; }
        public short ExpNorm { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int? UserProfileId { get; set; }
        public bool Retired { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public sbyte Sex { get; set; }
        public string Snils { get; set; }
        public string Inn { get; set; }
        public int? CashierCode { get; set; }
        public int AvailableForExternal { get; set; }
        public short PrimaryQuota { get; set; }
        public short OwnQuota { get; set; }
        public short ConsultancyQuota { get; set; }
        public short ExternalQuota { get; set; }
        public DateTime? AccessToDateSchedule { get; set; }
        public int DepthDaysSchedule { get; set; }
        public DateTime? LastAccessibleTimelineDate { get; set; }
        public int TimelineAccessibleDays { get; set; }
        public int CanSeeDays { get; set; }
        public sbyte AcademicDegree { get; set; }
        public int TypeTimeLinePerson { get; set; }
        public bool AddComment { get; set; }
        public string CommentText { get; set; }
        public int MaritalStatus { get; set; }
        public string ContactNumber { get; set; }
        public bool RegType { get; set; }
        public DateTime? RegBegDate { get; set; }
        public DateTime? RegEndDate { get; set; }
        public bool IsReservist { get; set; }
        public bool EmploymentType { get; set; }
        public bool OccupationType { get; set; }
        public int? CitizenshipId { get; set; }
        public bool? IsDefaultInHb { get; set; }
        public bool? IsInvestigator { get; set; }
        public string SyncGuid { get; set; }
        public bool? DoctorRoomAccessDenied { get; set; }
        public int? ConsultDepartmentId { get; set; }
        public bool TestPerson { get; set; }
        public string EcpPassword { get; set; }
        public int? UserId { get; set; }
        public string Email { get; set; }
        public bool? DisableSignDoc { get; set; }
        public int? MseSpecialityId { get; set; }
        public string LloLogin { get; set; }
        public string LloPassword { get; set; }
        public string LloCode { get; set; }

        public virtual Person CreatePerson { get; set; }
        public virtual Person ModifyPerson { get; set; }
        public virtual ICollection<Person> InverseCreatePerson { get; set; }
        public virtual ICollection<Person> InverseModifyPerson { get; set; }
        public virtual ICollection<RbPrintTemplate> RbPrintTemplateCreatePeople { get; set; }
        public virtual ICollection<RbPrintTemplate> RbPrintTemplateModifyPeople { get; set; }
    }
}
