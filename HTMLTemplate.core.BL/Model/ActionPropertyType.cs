#nullable disable

namespace HTMLTemplate.core.BL.Model
{
    public class ActionPropertyType
    {
        public int Id { get; set; }
        public bool Deleted { get; set; }
        public int ActionTypeId { get; set; }
        public int Idx { get; set; }
        public int? TemplateId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Descr { get; set; }
        public int? UnitId { get; set; }
        public string TypeName { get; set; }
        public string ValueDomain { get; set; }
        public string DefaultValue { get; set; }
        public bool IsVector { get; set; }
        public string Norm { get; set; }
        public sbyte Sex { get; set; }
        public string Age { get; set; }
        public int Penalty { get; set; }
        public string PenaltyUserProfile { get; set; }
        public bool VisibleInJobTicket { get; set; }
        public bool VisibleInTableRedactor { get; set; }
        public bool IsAssignable { get; set; }
        public int? TestId { get; set; }
        public bool DefaultEvaluation { get; set; }
        public bool CanChangeOnlyOwner { get; set; }
        public bool IsActionNameSpecifier { get; set; }
        public string LaboratoryCalculator { get; set; }
        public bool InActionsSelectionTable { get; set; }
        public bool RedactorSizeFactor { get; set; }
        public bool IsFrozen { get; set; }
        public bool? TypeEditable { get; set; }
        public bool VisibleInDr { get; set; }
        public int? UserProfileId { get; set; }
        public sbyte UserProfileBehaviour { get; set; }
        public sbyte CopyModifier { get; set; }
        public sbyte? TicketsNeeded { get; set; }
        public string CustomSelect { get; set; }
        public string AutoFieldUserProfile { get; set; }
        public string FormulaAlias { get; set; }
        public virtual ActionType ActionType { get; set; }
    }
}