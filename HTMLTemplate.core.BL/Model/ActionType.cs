using System;
using System.Collections.Generic;

#nullable disable

namespace HTMLTemplate.core.BL.Model
{
    public class ActionType
    {
        public ActionType()
        {
            ActionPropertyTypes = new HashSet<ActionPropertyType>();
            InverseGroup = new HashSet<ActionType>();
            InversePrescribedType = new HashSet<ActionType>();
        }

        public int Id { get; set; }
        public DateTime CreateDatetime { get; set; }
        public int? CreatePersonId { get; set; }
        public DateTime ModifyDatetime { get; set; }
        public int? ModifyPersonId { get; set; }
        public bool Deleted { get; set; }
        public bool Class { get; set; }
        public int? GroupId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string FlatCode { get; set; }
        public sbyte Sex { get; set; }
        public string Age { get; set; }
        public string Office { get; set; }
        public bool ShowInForm { get; set; }
        public bool GenTimetable { get; set; }
        public int? QuotaTypeId { get; set; }
        public string Context { get; set; }
        public double Amount { get; set; }
        public int AmountEvaluation { get; set; }
        public sbyte DefaultStatus { get; set; }
        public sbyte DefaultDirectionDate { get; set; }
        public bool DefaultPlannedEndDate { get; set; }
        public sbyte DefaultEndDate { get; set; }
        public int? DefaultExecPersonId { get; set; }
        public sbyte DefaultPersonInEvent { get; set; }
        public sbyte DefaultPersonInEditor { get; set; }
        public bool DefaultMkb { get; set; }
        public bool DefaultMorphology { get; set; }
        public bool IsMorphologyRequired { get; set; }
        public int? DefaultOrgId { get; set; }
        public int MaxOccursInEvent { get; set; }
        public int? IsMes { get; set; }
        public int? NomenclativeServiceId { get; set; }
        public bool ShowTime { get; set; }
        public bool? IsPreferable { get; set; }
        public int? PrescribedTypeId { get; set; }
        public int? SheduleId { get; set; }
        public bool IsRequiredCoordination { get; set; }
        public bool IsNomenclatureExpense { get; set; }
        public bool HasAssistant { get; set; }
        public bool? PropertyAssignedVisible { get; set; }
        public bool? PropertyUnitVisible { get; set; }
        public bool? PropertyNormVisible { get; set; }
        public bool? PropertyEvaluationVisible { get; set; }
        public bool ServiceType { get; set; }
        public short ActualAppointmentDuration { get; set; }
        public bool IsSubstituteEndDateToEvent { get; set; }
        public bool? IsPrinted { get; set; }
        public sbyte DefaultMes { get; set; }
        public int FrequencyCount { get; set; }
        public sbyte FrequencyPeriod { get; set; }
        public sbyte FrequencyPeriodType { get; set; }
        public bool IsStrictFrequency { get; set; }
        public sbyte IsFrequencyPeriodByCalendar { get; set; }
        public int? CounterId { get; set; }
        public bool IsCustomSum { get; set; }
        public int? RecommendationExpirePeriod { get; set; }
        public bool? RecommendationControl { get; set; }
        public bool? IsExecRequiredForEventExec { get; set; }
        public bool IsActiveGroup { get; set; }
        public string LisCode { get; set; }
        public bool Locked { get; set; }
        public bool? FilledLock { get; set; }
        public sbyte DefaultBeginDate { get; set; }
        public int? RefferalTypeId { get; set; }
        public bool? FilterPosts { get; set; }
        public bool? FilterSpecialities { get; set; }
        public bool? IsIgnoreEventExecDate { get; set; }
        public string FormulaAlias { get; set; }
        public virtual ActionType Group { get; set; }
        public virtual ActionType PrescribedType { get; set; }
        public virtual ICollection<ActionPropertyType> ActionPropertyTypes { get; set; }
        public virtual ICollection<ActionType> InverseGroup { get; set; }
        public virtual ICollection<ActionType> InversePrescribedType { get; set; }
    }
}
