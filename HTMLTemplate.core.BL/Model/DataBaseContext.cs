using System;
using System.Diagnostics;
using HTMLTemplate.core.BL.Models;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace HTMLTemplate.core.BL.Model
{
    public partial class DataBaseContext : DbContext
    {
        private readonly SqlConnectProperty _sqlConnect;

        public DataBaseContext(SqlConnectProperty sqlConnect)
        {
            _sqlConnect = sqlConnect ?? throw new ArgumentNullException(nameof(sqlConnect));
        }

        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<RbPrintTemplate> RbPrintTemplates { get; set; }
        public virtual DbSet<ActionPropertyType> ActionPropertyTypes { get; set; }
        public virtual DbSet<ActionType> ActionTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(
                    $"server={_sqlConnect.Server};database={_sqlConnect.Database};user={_sqlConnect.Username};pwd={_sqlConnect.Password};port={_sqlConnect.Port};Persist Security Info=True;Convert Zero Datetime=True",
                    Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.17-mariadb"),
                    builder =>
                    {
                    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                    });
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8")
                .UseCollation("utf8_general_ci");

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("Person");

                entity.HasComment("Сотрудники");

                entity.HasIndex(e => e.CitizenshipId, "Person_citizenship_id");

                entity.HasIndex(e => e.CreatePersonId, "createPerson_id");

                entity.HasIndex(e => e.FinanceId, "finance_id");

                entity.HasIndex(e => e.UserId, "fk_user_id");

                entity.HasIndex(e => new {e.LastName, e.FirstName, e.PatrName}, "lastName");

                entity.HasIndex(e => e.ModifyPersonId, "modifyPerson_id");

                entity.HasIndex(e => e.OrgStructureId, "orgStructure_id");

                entity.HasIndex(e => e.OrgId, "org_id");

                entity.HasIndex(e => e.ConsultDepartmentId, "person_ibfk_14");

                entity.HasIndex(e => e.PostId, "post_id");

                entity.HasIndex(e => e.RetireDate, "retireDate");

                entity.HasIndex(e => e.SpecialityId, "speciality_id");

                entity.HasIndex(e => e.TariffCategoryId, "tariffCategory_id");

                entity.HasIndex(e => e.UserProfileId, "userProfile_id");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.AcademicDegree)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("academicDegree")
                    .HasComment("Ученая степень (0-неопределено, 1-к.м.н, 2-д.м.н)");

                entity.Property(e => e.AccessToDateSchedule)
                    .HasColumnType("date")
                    .HasColumnName("accessToDateSchedule")
                    .HasComment("Доступ к данным расписания врача до даты");

                entity.Property(e => e.AddComment)
                    .HasColumnName("addComment")
                    .HasComment("флаг необходимости добавления комментария пользователя");

                entity.Property(e => e.AmbNorm)
                    .HasColumnType("smallint(4)")
                    .HasColumnName("ambNorm")
                    .HasComment("Норма амбулаторного приёма на 1 час");

                entity.Property(e => e.AmbPlan)
                    .HasColumnType("smallint(4)")
                    .HasColumnName("ambPlan")
                    .HasComment("Количество человек на весь амбулаторный приём");

                entity.Property(e => e.AmbPlan2)
                    .HasColumnType("smallint(4)")
                    .HasColumnName("ambPlan2")
                    .HasComment("Количество человек на весь амбулаторный приём");

                entity.Property(e => e.AvailableForExternal)
                    .HasColumnType("int(1)")
                    .HasColumnName("availableForExternal")
                    .HasDefaultValueSql("'1'")
                    .HasComment("Доступно для внешних систем (инфоматов и пр.)");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("date")
                    .HasColumnName("birthDate")
                    .HasComment("Дата рождения");

                entity.Property(e => e.BirthPlace)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("birthPlace")
                    .HasComment("Место рождения");

                entity.Property(e => e.CanSeeDays)
                    .HasColumnType("int(11)")
                    .HasColumnName("canSeeDays")
                    .HasComment(
                        "Ограничение количества дней, на которое пользователь может видеть чье-либо расписание. (0 - нет ограничения)");

                entity.Property(e => e.CashierCode)
                    .HasColumnType("int(11)")
                    .HasColumnName("cashier_code")
                    .HasComment("ID кассира");

                entity.Property(e => e.CitizenshipId)
                    .HasColumnType("int(11)")
                    .HasColumnName("citizenship_id")
                    .HasComment("Гражданство {rbCitizenship}");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(12)
                    .HasColumnName("code")
                    .HasComment("Код");

                entity.Property(e => e.CommentText)
                    .HasMaxLength(200)
                    .HasColumnName("commentText")
                    .HasComment("Текст комментария пользователя");

                entity.Property(e => e.ConsultDepartmentId)
                    .HasColumnType("int(11)")
                    .HasColumnName("consultDepartment_id")
                    .HasComment("Отделение для консультантов (WEB-120) {OrgStructure}");

                entity.Property(e => e.ConsultancyQuota)
                    .HasColumnType("smallint(4)")
                    .HasColumnName("consultancyQuota")
                    .HasDefaultValueSql("'25'")
                    .HasComment("Консультативная квота");

                entity.Property(e => e.ContactNumber)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnName("contactNumber")
                    .HasDefaultValueSql("''")
                    .HasComment("Телефон");

                entity.Property(e => e.CreateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("createDatetime")
                    .HasComment("Дата создания записи");

                entity.Property(e => e.CreatePersonId)
                    .HasColumnType("int(11)")
                    .HasColumnName("createPerson_id")
                    .HasComment("Автор записи {Person}");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasComment("Отметка удаления записи");

                entity.Property(e => e.DepthDaysSchedule)
                    .HasColumnType("int(11)")
                    .HasColumnName("depthDaysSchedule")
                    .HasComment("Глубина отображения расписания врача во внешней системе");

                entity.Property(e => e.DisableSignDoc)
                    .HasColumnName("disableSignDoc")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Запрещать подписывать документы этого врача");

                entity.Property(e => e.DoctorRoomAccessDenied)
                    .HasColumnName("doctorRoomAccessDenied")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Вход в DoctorRoom запрещен");

                entity.Property(e => e.EcpPassword)
                    .HasMaxLength(100)
                    .HasColumnName("ecp_password")
                    .HasDefaultValueSql("''")
                    .HasComment("Пароль от ЭЦП (если есть)");

                entity.Property(e => e.Email)
                    .HasMaxLength(64)
                    .HasColumnName("email")
                    .HasComment("Электронная почта");

                entity.Property(e => e.EmploymentType)
                    .HasColumnName("employmentType")
                    .HasComment("Режим работы (0-не известно, 1-постоянно, 2-временно, 3-по срочному договору)");

                entity.Property(e => e.ExpNorm)
                    .HasColumnType("smallint(4)")
                    .HasColumnName("expNorm")
                    .HasComment("Норма экспертизы на 1 час");

                entity.Property(e => e.ExpPlan)
                    .HasColumnType("smallint(4)")
                    .HasColumnName("expPlan")
                    .HasComment("Количество человек на экспертизу");

                entity.Property(e => e.ExternalQuota)
                    .HasColumnType("smallint(4)")
                    .HasColumnName("externalQuota")
                    .HasDefaultValueSql("'10'")
                    .HasComment("Внешняя квота");

                entity.Property(e => e.FederalCode)
                    .IsRequired()
                    .HasMaxLength(16)
                    .HasColumnName("federalCode")
                    .HasComment("Какой-то федеральный код");

                entity.Property(e => e.FinanceId)
                    .HasColumnType("int(11)")
                    .HasColumnName("finance_id")
                    .HasComment("Тип финансирования для Visit {rbFinance}");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("firstName")
                    .HasComment("Имя");

                entity.Property(e => e.HomNorm)
                    .HasColumnType("smallint(4)")
                    .HasColumnName("homNorm")
                    .HasComment("Норма д.приёма на 1 час");

                entity.Property(e => e.HomPlan)
                    .HasColumnType("smallint(4)")
                    .HasColumnName("homPlan")
                    .HasComment("Количество человек на весь д.приём");

                entity.Property(e => e.HomPlan2)
                    .HasColumnType("smallint(4)")
                    .HasColumnName("homPlan2")
                    .HasComment("Количество человек на вызов");

                entity.Property(e => e.Inn)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnName("INN")
                    .IsFixedLength(true)
                    .HasComment("ИНН");

                entity.Property(e => e.IsDefaultInHb)
                    .IsRequired()
                    .HasColumnName("isDefaultInHB")
                    .HasDefaultValueSql("'1'")
                    .HasComment("0 - не фильтровать; 1 - фильтровать");

                entity.Property(e => e.IsInvestigator)
                    .HasColumnName("isInvestigator")
                    .HasComment("Является главным исследователем");

                entity.Property(e => e.IsReservist)
                    .HasColumnName("isReservist")
                    .HasComment("Военнообязан (0-не известно, 1-нет, 2-да)");

                entity.Property(e => e.LastAccessibleTimelineDate)
                    .HasColumnType("date")
                    .HasColumnName("lastAccessibleTimelineDate")
                    .HasComment("Последняя доступная дата в расписании врача");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("lastName")
                    .HasComment("Фамилия");

                entity.Property(e => e.LloCode)
                    .HasMaxLength(15)
                    .HasColumnName("llo_code")
                    .HasComment("Код пользователя в подсистеме РЕГИЗ ЛЛО");

                entity.Property(e => e.LloLogin)
                    .HasMaxLength(20)
                    .HasColumnName("llo_login")
                    .HasComment("Логин пользователя в подсистеме РЕГИЗ ЛЛО");

                entity.Property(e => e.LloPassword)
                    .HasMaxLength(100)
                    .HasColumnName("llo_password")
                    .HasComment("Пароль пользователя в подсистеме РЕГИЗ ЛЛО");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("login")
                    .HasComment("имя для входа в систему");

                entity.Property(e => e.MaritalStatus)
                    .HasColumnType("int(11)")
                    .HasColumnName("maritalStatus")
                    .HasComment("Состояние в браке (ОКИН 10)");

                entity.Property(e => e.ModifyDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("modifyDatetime")
                    .HasComment("Дата изменения записи");

                entity.Property(e => e.ModifyPersonId)
                    .HasColumnType("int(11)")
                    .HasColumnName("modifyPerson_id")
                    .HasComment("Автор изменения записи {Person}");

                entity.Property(e => e.MseSpecialityId)
                    .HasColumnType("int(4)")
                    .HasColumnName("mse_speciality_id");

                entity.Property(e => e.OccupationType)
                    .HasColumnName("occupationType")
                    .HasComment("Тип занятия должности (0-не известно, 1-основное, 2-совмещение)");

                entity.Property(e => e.Office)
                    .IsRequired()
                    .HasMaxLength(8)
                    .HasColumnName("office")
                    .HasComment("Кабинет");

                entity.Property(e => e.Office2)
                    .IsRequired()
                    .HasMaxLength(8)
                    .HasColumnName("office2")
                    .HasComment("Кабинет2");

                entity.Property(e => e.OrgId)
                    .HasColumnType("int(11)")
                    .HasColumnName("org_id")
                    .HasComment("Место работы {Organisation}");

                entity.Property(e => e.OrgStructureId)
                    .HasColumnType("int(11)")
                    .HasColumnName("orgStructure_id")
                    .HasComment("Организационная структура {OrgStructure}");

                entity.Property(e => e.OwnQuota)
                    .HasColumnType("smallint(4)")
                    .HasColumnName("ownQuota")
                    .HasDefaultValueSql("'25'")
                    .HasComment("Квота врача");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("password")
                    .HasDefaultValueSql("''")
                    .HasComment("hash от пароля");

                entity.Property(e => e.PatrName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("patrName")
                    .HasComment("Отчество");

                entity.Property(e => e.PostId)
                    .HasColumnType("int(11)")
                    .HasColumnName("post_id")
                    .HasComment("Должность {rbPost}");

                entity.Property(e => e.PrimaryQuota)
                    .HasColumnType("smallint(4)")
                    .HasColumnName("primaryQuota")
                    .HasDefaultValueSql("'50'")
                    .HasComment("Первичная квота");

                entity.Property(e => e.RegBegDate)
                    .HasColumnType("date")
                    .HasColumnName("regBegDate")
                    .HasComment("Дата начала регистрации");

                entity.Property(e => e.RegEndDate)
                    .HasColumnType("date")
                    .HasColumnName("regEndDate")
                    .HasComment("Дата окончания регистрации");

                entity.Property(e => e.RegType)
                    .HasColumnName("regType")
                    .HasComment("Тип регистрации");

                entity.Property(e => e.RegionalCode)
                    .IsRequired()
                    .HasMaxLength(16)
                    .HasColumnName("regionalCode")
                    .HasComment("Какой-то региональный код");

                entity.Property(e => e.RetireDate)
                    .HasColumnType("date")
                    .HasColumnName("retireDate")
                    .HasComment("Дата, после которой на сотрудника нельзя подавать сведения (Дата увольнения)");

                entity.Property(e => e.Retired)
                    .HasColumnName("retired")
                    .HasComment("Вход в систему запрещён");

                entity.Property(e => e.Sex)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("sex")
                    .HasComment("Пол (0-неопределено, 1-М, 2-Ж)");

                entity.Property(e => e.Snils)
                    .IsRequired()
                    .HasMaxLength(11)
                    .HasColumnName("SNILS")
                    .IsFixedLength(true)
                    .HasComment("СНИЛС");

                entity.Property(e => e.SpecialityId)
                    .HasColumnType("int(11)")
                    .HasColumnName("speciality_id")
                    .HasComment("Специальность {rbSpeciality}");

                entity.Property(e => e.SyncGuid)
                    .HasMaxLength(36)
                    .HasColumnName("syncGUID")
                    .HasComment("Используется при синхронизации справочников в 1С");

                entity.Property(e => e.TariffCategoryId)
                    .HasColumnType("int(11)")
                    .HasColumnName("tariffCategory_id")
                    .HasComment("Тарифная категория {rbTariffCategory}");

                entity.Property(e => e.TestPerson)
                    .HasColumnName("Test_Person")
                    .HasComment("Отметка о тестовой записи");

                entity.Property(e => e.TimelineAccessibleDays)
                    .HasColumnType("int(11)")
                    .HasColumnName("timelineAccessibleDays")
                    .HasComment("Количество дней, на которые доступно расписание врача");

                entity.Property(e => e.TypeTimeLinePerson)
                    .HasColumnType("int(11)")
                    .HasColumnName("typeTimeLinePerson")
                    .HasComment("Тип персонального графика");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("user_id")
                    .HasComment("Профиль авторизации");

                entity.Property(e => e.UserProfileId)
                    .HasColumnType("int(11)")
                    .HasColumnName("userProfile_id")
                    .HasComment(
                        "(deprecated in r16290, see Person_UserProfile) Ссылка на профиль прав доступа {rbUserProfile}");

                entity.HasOne(d => d.CreatePerson)
                    .WithMany(p => p.InverseCreatePerson)
                    .HasForeignKey(d => d.CreatePersonId)
                    .HasConstraintName("person_ibfk_7");

                entity.HasOne(d => d.ModifyPerson)
                    .WithMany(p => p.InverseModifyPerson)
                    .HasForeignKey(d => d.ModifyPersonId)
                    .HasConstraintName("person_ibfk_8");
            });

            modelBuilder.Entity<RbPrintTemplate>(entity =>
            {
                entity.ToTable("rbPrintTemplate");

                entity.HasComment("Шаблоны печати");

                entity.HasIndex(e => e.CreatePersonId, "PrintTemplate_createPerson_id");

                entity.HasIndex(e => e.ModifyPersonId, "PrintTemplate_modifyPerson_id");

                entity.HasIndex(e => e.DocumentTypeId, "rbPrintTemplate_documentType");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(16)
                    .HasColumnName("code")
                    .HasComment("Код");

                entity.Property(e => e.Context)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("context")
                    .HasComment("Контекст (order, token, F131 и т.п.) ");

                entity.Property(e => e.CreateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("createDatetime")
                    .HasComment("Дата создания записи");

                entity.Property(e => e.CreatePersonId)
                    .HasColumnType("int(11)")
                    .HasColumnName("createPerson_id")
                    .HasComment("Автор записи (внешний id)");

                entity.Property(e => e.Default)
                    .HasColumnType("mediumtext")
                    .HasColumnName("default");

                entity.Property(e => e.Deleted)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("deleted")
                    .HasComment("отметка об удалении");

                entity.Property(e => e.DocumentTypeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("documentType_id")
                    .HasComment("Тип документа по ИЭМК");

                entity.Property(e => e.DpdAgreement)
                    .HasColumnName("dpdAgreement")
                    .HasComment(
                        "Меняет ли ДПД клиента при печати: 0-Не меняет, 1-Меняет на \"Да\", 2-Меняет на \"Нет\" ");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("fileName")
                    .HasComment("Имя файла шаблона");

                entity.Property(e => e.GroupName)
                    .HasMaxLength(20)
                    .HasColumnName("groupName")
                    .HasComment("Группа");

                entity.Property(e => e.IsEditableInWeb)
                    .IsRequired()
                    .HasColumnName("isEditableInWeb")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.IsPatientAgreed)
                    .HasColumnName("isPatientAgreed")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Необходимость согласования с клиентом");

                entity.Property(e => e.ModifyDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("modifyDatetime")
                    .HasComment("Дата изменения записи");

                entity.Property(e => e.ModifyPersonId)
                    .HasColumnType("int(11)")
                    .HasColumnName("modifyPerson_id")
                    .HasComment("Автор изменения записи {Person}");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name")
                    .HasComment("Наименование");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasComment("Тип шаблона: 0-HTML,1-Exaro,2-SVG");

                entity.HasOne(d => d.CreatePerson)
                    .WithMany(p => p.RbPrintTemplateCreatePeople)
                    .HasForeignKey(d => d.CreatePersonId)
                    .HasConstraintName("PrintTemplate_createPerson_id");

                entity.HasOne(d => d.ModifyPerson)
                    .WithMany(p => p.RbPrintTemplateModifyPeople)
                    .HasForeignKey(d => d.ModifyPersonId)
                    .HasConstraintName("PrintTemplate_modifyPerson_id");
            });

            modelBuilder.Entity<ActionPropertyType>(entity =>
            {
                entity.ToTable("ActionPropertyType");

                entity.HasComment("Описание свойства типа действия");

                entity.HasIndex(e => e.UserProfileId, "ActionPropertyType_rbUserProfile_idx");

                entity.HasIndex(e => e.ActionTypeId, "actionType_id");

                entity.HasIndex(e => e.Name, "name_idx");

                entity.HasIndex(e => e.TemplateId, "template_id");

                entity.HasIndex(e => e.TestId, "test_id");

                entity.HasIndex(e => e.UnitId, "unit_id");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.ActionTypeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("actionType_id")
                    .HasComment("Тип действия, к которому относится это свойство {ActionType}");

                entity.Property(e => e.Age)
                    .IsRequired()
                    .HasMaxLength(9)
                    .HasColumnName("age")
                    .HasComment(
                        "Применимо для указанного интервала возрастов пусто-нет ограничения, \"{NNN{д|н|м|г}-{MMM{д|н|м|г}}\" - с NNN дней/недель/месяцев/лет по MMM дней/недель/месяцев/лет; пустая нижняя или верхняя граница - нет ограничения снизу или сверху");

                entity.Property(e => e.AutoFieldUserProfile)
                    .HasColumnType("text")
                    .HasColumnName("autoFieldUserProfile")
                    .HasComment(
                        "Список профилей прав, которые могут редактировать автоматически заполняемые поля. Сепаратор - \";\"");

                entity.Property(e => e.CanChangeOnlyOwner)
                    .HasColumnName("canChangeOnlyOwner")
                    .HasComment("Право редактировать свойство: 0 - все, 1 - назначивший действие, 2 - никто");

                entity.Property(e => e.CopyModifier)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("copyModifier")
                    .HasComment("Модификатор копирования");

                entity.Property(e => e.CustomSelect)
                    .HasColumnType("text")
                    .HasColumnName("customSelect")
                    .HasComment(
                        "Поле для пользовательского запроса, использующегося при автоматическом заполнении свойства");

                entity.Property(e => e.DefaultEvaluation)
                    .HasColumnName("defaultEvaluation")
                    .HasComment("0-не определять, 1-автомат, 2-полуавтомат, 3-ручное");

                entity.Property(e => e.DefaultValue).HasColumnName("defaultValue");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasComment("Отметка удаления записи");

                entity.Property(e => e.Descr)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("descr")
                    .HasComment("Описание свойства");

                entity.Property(e => e.FormulaAlias)
                    .HasMaxLength(10)
                    .HasColumnName("formulaAlias")
                    .HasComment("Короткий алиас для использования в формулах, используемых в автозаполнении свойств");

                entity.Property(e => e.Idx)
                    .HasColumnType("int(11)")
                    .HasColumnName("idx")
                    .HasComment("относительный индекс (для сортировки в списке)");

                entity.Property(e => e.InActionsSelectionTable)
                    .HasColumnName("inActionsSelectionTable")
                    .HasComment("0-Не определено, 1-Recipe(Возьми), 2-Doses(Доза), 3-Signa(Выдай)");

                entity.Property(e => e.IsActionNameSpecifier)
                    .HasColumnName("isActionNameSpecifier")
                    .HasComment("Является уточняющим имя действия");

                entity.Property(e => e.IsAssignable)
                    .HasColumnName("isAssignable")
                    .HasComment("является назначаемым");

                entity.Property(e => e.IsFrozen)
                    .HasColumnName("isFrozen")
                    .HasComment("Свойство закреплено (0-нет, 1-да)");

                entity.Property(e => e.IsVector)
                    .HasColumnName("isVector")
                    .HasComment("Это векторное значение");

                entity.Property(e => e.LaboratoryCalculator)
                    .HasMaxLength(3)
                    .HasColumnName("laboratoryCalculator")
                    .HasComment(
                        "три знака: первый-клавиша калькулятора, второй-тип результата(А абсалютное значение, % относительное), третий-группа");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasComment("Наименование свойства");

                entity.Property(e => e.Norm)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("norm")
                    .HasComment("Норматив");

                entity.Property(e => e.Penalty)
                    .HasColumnType("int(3)")
                    .HasColumnName("penalty")
                    .HasComment("Штраф в баллах(max 100)");

                entity.Property(e => e.PenaltyUserProfile)
                    .HasColumnType("text")
                    .HasColumnName("penaltyUserProfile")
                    .HasComment("Список профилей прав, которых касается штраф. Сепаратор - \";\"");

                entity.Property(e => e.RedactorSizeFactor)
                    .HasColumnName("redactorSizeFactor")
                    .HasComment("Коофицент размера редактора свойства действия");

                entity.Property(e => e.Sex)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("sex")
                    .HasComment("Применимо для указанного пола (0-любой, 1-М, 2-Ж)");

                entity.Property(e => e.ShortName)
                    .IsRequired()
                    .HasMaxLength(16)
                    .HasColumnName("shortName")
                    .HasComment("Короткое наименование");

                entity.Property(e => e.TemplateId)
                    .HasColumnType("int(11)")
                    .HasColumnName("template_id")
                    .HasComment("Ссылка на библиотеку {ActionPropertyTemplate}");

                entity.Property(e => e.TestId)
                    .HasColumnType("int(11)")
                    .HasColumnName("test_id")
                    .HasComment("Если свойство является показателем теста, то это ссылка на показатель {rbTest}");

                entity.Property(e => e.TicketsNeeded)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("ticketsNeeded")
                    .HasComment("Количество номерков(JobTicket) необходимое для проведения услуги данного типа");

                entity.Property(e => e.TypeEditable)
                    .IsRequired()
                    .HasColumnName("typeEditable")
                    .HasDefaultValueSql("'1'")
                    .HasComment("Конструктор доступен для редактирования");

                entity.Property(e => e.TypeName)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("typeName")
                    .HasComment("Имя типа значения, строка \"integer\",\"time\" и т.п.");

                entity.Property(e => e.UnitId)
                    .HasColumnType("int(11)")
                    .HasColumnName("unit_id")
                    .HasComment("Единица измерения {rbUnit}");

                entity.Property(e => e.UserProfileBehaviour)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("userProfileBehaviour")
                    .HasComment("Поведение при отсутствии прав: 0 - отключать редактир. 1 - скрывать");

                entity.Property(e => e.UserProfileId)
                    .HasColumnType("int(11)")
                    .HasColumnName("userProfile_id")
                    .HasComment("Профиль прав, необходимый для редактирования/просмотра {rbUserProfile}");

                entity.Property(e => e.ValueDomain)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("valueDomain")
                    .HasComment("для типов enum и вариант - наборы строчных значений через |");

                entity.Property(e => e.VisibleInDr)
                    .HasColumnName("visibleInDR")
                    .HasComment("0=не видимо, 1=видимо");

                entity.Property(e => e.VisibleInJobTicket)
                    .HasColumnName("visibleInJobTicket")
                    .HasComment("0=не видимо при редактировании Job_Ticket, 1=видимо");

                entity.Property(e => e.VisibleInTableRedactor)
                    .HasColumnName("visibleInTableRedactor")
                    .HasComment("0-Не видно, 1-Режим редактирвоания, 2-Без редактирования");

                entity.HasOne(d => d.ActionType)
                    .WithMany(p => p.ActionPropertyTypes)
                    .HasForeignKey(d => d.ActionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("actionpropertytype_ibfk_1");
            });

            modelBuilder.Entity<ActionType>(entity =>
            {
                entity.ToTable("ActionType");

                entity.HasComment("Описание мероприятия, связанного с событием: направления, вы");

                entity.HasIndex(e => e.DefaultExecPersonId, "ActionType_defaultExecPerson_id");

                entity.HasIndex(e => e.DefaultOrgId, "ActionType_defaultOrg_id");

                entity.HasIndex(e => e.RefferalTypeId, "ReferralType_Awards_refferalType_id");

                entity.HasIndex(e => e.Class, "class");

                entity.HasIndex(e => e.Code, "code");

                entity.HasIndex(e => e.CreatePersonId, "createPerson_id");

                entity.HasIndex(e => e.FlatCode, "flatCode");

                entity.HasIndex(e => e.GroupId, "group_id");

                entity.HasIndex(e => e.LisCode, "lis_code");

                entity.HasIndex(e => e.ModifyPersonId, "modifyPerson_id");

                entity.HasIndex(e => e.NomenclativeServiceId, "nomenclativeService_id");

                entity.HasIndex(e => e.PrescribedTypeId, "prescribedType_id");

                entity.HasIndex(e => e.QuotaTypeId, "quotaType_id");

                entity.HasIndex(e => e.SheduleId, "shedule_id");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.ActualAppointmentDuration)
                    .HasColumnType("smallint(6)")
                    .HasColumnName("actualAppointmentDuration")
                    .HasComment("Актуальность при назначении");

                entity.Property(e => e.Age)
                    .IsRequired()
                    .HasMaxLength(9)
                    .HasColumnName("age")
                    .HasComment(
                        "Применимо для указанного интервала возрастов пусто-нет ограничения, \"{NNN{д|н|м|г}-{MMM{д|н|м|г}}\" - с NNN дней/недель/месяцев/лет по MMM дней/недель/месяцев/лет; пустая нижняя или верхняя граница - нет ограничения снизу или сверху");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasDefaultValueSql("'1'")
                    .HasComment("Количество по умолчанию");

                entity.Property(e => e.AmountEvaluation)
                    .HasColumnType("int(1)")
                    .HasColumnName("amountEvaluation")
                    .HasComment(
                        "0-Количество вводится непосредственно, 1-По числу визитов, 2-По длительности события, 3-По длительности события без выходных дней, 4-По длительности действия, 5-По длительности действия без выходных дней, 6-По заполненным свойствам действия");

                entity.Property(e => e.Class)
                    .HasColumnName("class")
                    .HasComment("0-статус, 1-диагностика, 2-лечение, 3-прочие мероприятия");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnName("code")
                    .HasComment("Код");

                entity.Property(e => e.Context)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("context")
                    .HasComment("Контекст печати ");

                entity.Property(e => e.CounterId)
                    .HasColumnType("int(11)")
                    .HasColumnName("counter_id")
                    .HasComment("счетчик");

                entity.Property(e => e.CreateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("createDatetime")
                    .HasComment("Дата создания записи");

                entity.Property(e => e.CreatePersonId)
                    .HasColumnType("int(11)")
                    .HasColumnName("createPerson_id")
                    .HasComment("Автор записи {Person}");

                entity.Property(e => e.DefaultBeginDate)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("defaultBeginDate")
                    .HasComment(
                        "Дата начала действия по умолчанию: 0-Не задано, 1-По дате начала события, 2-Текущая дата, 3-Синхронизация по дате выполнения");

                entity.Property(e => e.DefaultDirectionDate)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("defaultDirectionDate")
                    .HasComment(
                        "Код значение по умолчанию для даты назначения действияия: 0-Не задано, 1-По дате начала события, 2-Текущая дата, 3-Синхронизация по дате выполнения");

                entity.Property(e => e.DefaultEndDate)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("defaultEndDate")
                    .HasComment(
                        "Код значение по умолчанию для даты выполнения события: 0-Пусто, 1-Тек.дата, 2-Дата начала события, 3-Дата окончания события");

                entity.Property(e => e.DefaultExecPersonId)
                    .HasColumnType("int(11)")
                    .HasColumnName("defaultExecPerson_id")
                    .HasComment("Предопределенный ответственный за действие в событии{Person}");

                entity.Property(e => e.DefaultMes)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("defaultMES")
                    .HasComment("МЭС по умолчанию. 0 - Не используется; 1 - Стандарт из события; 2 - Пустой");

                entity.Property(e => e.DefaultMkb)
                    .HasColumnName("defaultMKB")
                    .HasComment(
                        "Правило заполнения по умолчанию поля Action.`MKB` (0-не используется, 1-по диагнозу назначившего действие, 2-синхронизировать с заключительным, 3-синхронизировать с диагнозом назначившего действие)");

                entity.Property(e => e.DefaultMorphology)
                    .HasColumnName("defaultMorphology")
                    .HasComment(
                        "Правило заполнения по умолчанию поля Action.`morphologyMKB` (0-не используется, 1-по диагнозу назначившего действие, 2-синхронизировать с заключительным, 3-синхронизировать с диагнозом назначившего действие)");

                entity.Property(e => e.DefaultOrgId)
                    .HasColumnType("int(11)")
                    .HasColumnName("defaultOrg_id")
                    .HasComment("Организация выполняющая действие по умолчанию {Organisation}");

                entity.Property(e => e.DefaultPersonInEditor)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("defaultPersonInEditor")
                    .HasComment(
                        "исполнитель в отдельном редакторе: 0-Не определено, 1-Не заполняется, 2-Назначивший действие, 3-Ответственный за событие, 4-Пользователь");

                entity.Property(e => e.DefaultPersonInEvent)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("defaultPersonInEvent")
                    .HasComment(
                        "исполнитель в редакторе события: 0-Не определено, 1-Не заполняется, 2-Назначивший действие, 3-Ответственный за событие, 4-Пользователь");

                entity.Property(e => e.DefaultPlannedEndDate)
                    .HasColumnName("defaultPlannedEndDate")
                    .HasComment(
                        "Планируемя дата выполнения (0=не определено, 1=След. день, 2=След. рабочий день, 3=Дата талона на Работу, 4=Дата начала + количество, 5=Дата начала + длительность)");

                entity.Property(e => e.DefaultStatus)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("defaultStatus")
                    .HasComment(
                        "Значение по умолчанию для статуса выполнения: 0-Начато, 1-Ожидание, 2-Закончено, 3-Отменено");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasComment("Отметка удаления записи");

                entity.Property(e => e.FilledLock)
                    .HasColumnName("filledLock")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Запрещать удаление если заполнено хотя бы одно свойство");

                entity.Property(e => e.FilterPosts)
                    .HasColumnName("filterPosts")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.FilterSpecialities)
                    .HasColumnName("filterSpecialities")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.FlatCode)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("flatCode")
                    .HasComment(
                        "\"Плоский\" код. в противовес code должен быть уникальным, используется для отчётов, импортов и экспортов");

                entity.Property(e => e.FormulaAlias)
                    .HasMaxLength(10)
                    .HasColumnName("formulaAlias")
                    .HasComment("Короткий алиас для использования в формулах, используемых в автозаполнении свойств");

                entity.Property(e => e.FrequencyCount)
                    .HasColumnType("int(11)")
                    .HasColumnName("frequencyCount")
                    .HasComment(
                        "Допустимая частота повторного назначения услуги пациенту за определенный период. 0 - нет ограничений на количество повторов.");

                entity.Property(e => e.FrequencyPeriod)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("frequencyPeriod")
                    .HasComment(
                        "Размер периода для контроля частоты назначений услуги. Если 0, то учитывается весь возможный диапазон.");

                entity.Property(e => e.FrequencyPeriodType)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("frequencyPeriodType")
                    .HasComment(
                        "Тип периода для контроля частоты повторного назначения: 0-нет, 1-неделя, 2-месяц, 3-квартал, 4-полугодие, 5-год");

                entity.Property(e => e.GenTimetable)
                    .HasColumnName("genTimetable")
                    .HasComment("Генерировать график (приём)");

                entity.Property(e => e.GroupId)
                    .HasColumnType("int(11)")
                    .HasColumnName("group_id")
                    .HasComment("Поле для группировки действия {ActionType}");

                entity.Property(e => e.HasAssistant)
                    .HasColumnName("hasAssistant")
                    .HasComment("Ввод ассистента: 0 - не треб, 1 - не обяз, 2 - обяз");

                entity.Property(e => e.IsActiveGroup)
                    .HasColumnName("isActiveGroup")
                    .HasComment("Событие, которое заменяет параметры дочерних действий");

                entity.Property(e => e.IsCustomSum)
                    .HasColumnName("isCustomSum")
                    .HasComment("Возможен ручной ввод цены");

                entity.Property(e => e.IsExecRequiredForEventExec)
                    .IsRequired()
                    .HasColumnName("isExecRequiredForEventExec")
                    .HasDefaultValueSql("'1'")
                    .HasComment("Необходимо состояние не \"начато\" для закрытия обращения");

                entity.Property(e => e.IsFrequencyPeriodByCalendar)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("isFrequencyPeriodByCalendar")
                    .HasComment(
                        "Использовать календарные периоды вместо абсолютных. То есть правило \"раз в неделю\" будет трактоваться как \"раз в календарную неделю (с Пн по Вс), а не  \"раз в 7 дней\".");

                entity.Property(e => e.IsIgnoreEventExecDate)
                    .HasColumnName("isIgnoreEventExecDate")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Игнорировать дату окончания события");

                entity.Property(e => e.IsMes)
                    .HasColumnType("int(11)")
                    .HasColumnName("isMES")
                    .HasComment("Является стандартом");

                entity.Property(e => e.IsMorphologyRequired)
                    .HasColumnName("isMorphologyRequired")
                    .HasComment(
                        "0-Не контролировать, 1-Запполнять не обязательно(мягкий контроль), 2-нужно заполнить(жесткий контроль)");

                entity.Property(e => e.IsNomenclatureExpense)
                    .HasColumnName("isNomenclatureExpense")
                    .HasComment("Является тратой ЛСиИМН (Возможно списание ЛСиИМН)");

                entity.Property(e => e.IsPreferable)
                    .IsRequired()
                    .HasColumnName("isPreferable")
                    .HasDefaultValueSql("'1'")
                    .HasComment("Является предпочитаемым (выполняемым?) в данном ЛПУ");

                entity.Property(e => e.IsPrinted)
                    .IsRequired()
                    .HasColumnName("isPrinted")
                    .HasDefaultValueSql("'1'")
                    .HasComment(
                        "Выводить на печать действие этого типа (проверяться должно в шаблоне печати: Action.isPrinted)");

                entity.Property(e => e.IsRequiredCoordination)
                    .HasColumnName("isRequiredCoordination")
                    .HasComment("Требуется обязательное согласование");

                entity.Property(e => e.IsStrictFrequency)
                    .HasColumnName("isStrictFrequency")
                    .HasComment("0-мягкая проверка, 1-жесткая проверка");

                entity.Property(e => e.IsSubstituteEndDateToEvent)
                    .HasColumnName("isSubstituteEndDateToEvent")
                    .HasComment("Подстановка даты окончания дейтсвия в дату окончания события");

                entity.Property(e => e.LisCode)
                    .HasMaxLength(32)
                    .HasColumnName("lis_code")
                    .HasComment("Код анализа в ЛИС");

                entity.Property(e => e.Locked)
                    .HasColumnName("locked")
                    .HasComment(
                        "Удаление разрешено только администратору и пользователям, имеющим соответствующее право");

                entity.Property(e => e.MaxOccursInEvent)
                    .HasColumnType("int(11)")
                    .HasColumnName("maxOccursInEvent")
                    .HasComment("Ограничение регистрации действий по по количеству в событии");

                entity.Property(e => e.ModifyDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("modifyDatetime")
                    .HasComment("Дата изменения записи");

                entity.Property(e => e.ModifyPersonId)
                    .HasColumnType("int(11)")
                    .HasColumnName("modifyPerson_id")
                    .HasComment("Автор изменения записи {Person}");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(355)
                    .HasColumnName("name")
                    .HasComment("Наименование действия");

                entity.Property(e => e.NomenclativeServiceId)
                    .HasColumnType("int(11)")
                    .HasColumnName("nomenclativeService_id")
                    .HasComment("Номенклатурная услуга {rbService}");

                entity.Property(e => e.Office)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("office")
                    .HasComment("Кабинет по умолчанию");

                entity.Property(e => e.PrescribedTypeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("prescribedType_id")
                    .HasComment("Предписываемое действие {ActionType}");

                entity.Property(e => e.PropertyAssignedVisible)
                    .IsRequired()
                    .HasColumnName("propertyAssignedVisible")
                    .HasDefaultValueSql("'1'")
                    .HasComment("Визуализация `назначено` в свойствах действия");

                entity.Property(e => e.PropertyEvaluationVisible)
                    .IsRequired()
                    .HasColumnName("propertyEvaluationVisible")
                    .HasDefaultValueSql("'1'")
                    .HasComment("Визуализация `оценка` в свойствах действия");

                entity.Property(e => e.PropertyNormVisible)
                    .IsRequired()
                    .HasColumnName("propertyNormVisible")
                    .HasDefaultValueSql("'1'")
                    .HasComment("Визуализация `норма` в свойствах действия");

                entity.Property(e => e.PropertyUnitVisible)
                    .IsRequired()
                    .HasColumnName("propertyUnitVisible")
                    .HasDefaultValueSql("'1'")
                    .HasComment("Визуализация `ед.изм.` в свойствах действия");

                entity.Property(e => e.QuotaTypeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("quotaType_id")
                    .HasComment("Вид квоты {QuotaType}");

                entity.Property(e => e.RecommendationControl)
                    .HasColumnName("recommendationControl")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Контроль назначившего");

                entity.Property(e => e.RecommendationExpirePeriod)
                    .HasColumnType("int(11)")
                    .HasColumnName("recommendationExpirePeriod")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Срок актуальности направления в днях");

                entity.Property(e => e.RefferalTypeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("refferalType_id")
                    .HasComment("Тип направления");

                entity.Property(e => e.ServiceType)
                    .HasColumnName("serviceType")
                    .HasComment(
                        "Вид услуги: 0-Прочие, 1-первичный осмотр, 2-повторный осмотр, 3-процедура/манипуляция, 4-операция, 5-исследование, 6-лечение");

                entity.Property(e => e.Sex)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("sex")
                    .HasComment("Применимо для указанного пола (0-любой, 1-М, 2-Ж)");

                entity.Property(e => e.SheduleId)
                    .HasColumnType("int(11)")
                    .HasColumnName("shedule_id")
                    .HasComment("График по умолчанию {rbActionShedule}");

                entity.Property(e => e.ShowInForm)
                    .HasColumnName("showInForm")
                    .HasComment("Разрешается выбор в формах ввода событий");

                entity.Property(e => e.ShowTime)
                    .HasColumnName("showTime")
                    .HasComment("Показывать в интерфейсе не только дату, но и время назначения/начала/окончания");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("title")
                    .HasComment("Наименование для печати");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.InverseGroup)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("ActionType_group");

                entity.HasOne(d => d.PrescribedType)
                    .WithMany(p => p.InversePrescribedType)
                    .HasForeignKey(d => d.PrescribedTypeId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("ActionType_prescribedType");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}