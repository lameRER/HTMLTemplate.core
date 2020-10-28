using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace HTMLTemplate
{
    internal class Program
    {
        private static readonly Random Ran = new Random();
        private static readonly List<string> EndText = new List<string>();
        private static readonly List<string> Property = new List<string>();
        private static string _docName = string.Empty;
        private static string _docContext = string.Empty;
        private const string Path = @"Property.txt";
        private static string _connect;
        private static string _dataBase;

        private static void Main()
        {
            Consol();
        }

        private static void HtmLwriter(char item)
        {
            //Thread.Sleep(Random(Ran));
            var htmLwriter1 = new StreamWriter($@"C:\VISTA_MED\lustik_ak\templates\{_docContext}.html", true, Encoding.GetEncoding("UTF-8"));
            htmLwriter1.Write($"{item}");
            htmLwriter1.Close();
        }
        private static int Random(Random ran)
        {
            #if DEBUG
                return ran.Next(100, 200);
            #else
                return ran.Next(10, 100);
            #endif
        }

        private static void Consol()
        {
            Console.WriteLine("Connect:");
            _connect = Console.ReadLine();
            Console.WriteLine("Base:");
            _dataBase = Console.ReadLine();
            var countries = new Dictionary<int, string>(10)
            {
                {1, "Создание шаблона печати"},
                {2, "Чтение полей из файла"},
                {3, "Чтение шаблон печати"},
                {4, "Cохранить шаблон печати"},
                {5, "Заполнение rbThesaurus"},
                {6, "Заполнение ActionPropertyType"}
            };
            foreach (var (key, value) in countries)
            {
                Console.WriteLine($"{key}. {value}");
            }
            Console.Write("Value: ");
            //system.Console.WriteLine("1. Создание шаблона печати\n");
            switch (Convert.ToInt32(Console.ReadLine()))
            {
                case 1:
                    Create();
                    break;
                case 2:
                    ReadFile();
                    break;
                case 3:
                    Read();
                    break;
                case 4:
                    Write(string.Empty);
                    break;
                case 5:
                    Console.Write("Код: ; Idx: ");
                    RbThesaurus(Console.ReadLine(), Convert.ToInt16(Console.ReadLine()));
                    break;
                case 6:
                    Console.Write("Код: ; Idx: ");
                    ActionPropertyType(Console.ReadLine(), Convert.ToInt16(Console.ReadLine()));
                    break;
                case 7:
                    Console.Write("Parent ; Idx: ");
                    OrgStructure();
                    break;
                case 8:
                    Create_test();
                    break;
                default:
                    Console.WriteLine("Значение не верно");
                    break;
            }
        }
        #region Чтение полей из файла
        private static void ReadFile()
        {
            Property.Clear();
            using var fs = new FileStream("Property.txt", FileMode.OpenOrCreate, FileAccess.Read);
            using (var streamReader = new StreamReader(fs, Encoding.UTF8))
            {
                var listPropertyName = streamReader.ReadToEnd().Split((char)13);
                foreach (var propertyName in listPropertyName.Where(x => x.Contains(':')))
                {
                    Property.Add(propertyName.Substring(0, propertyName.LastIndexOf(':') + 1).Trim());
                }
            }
            foreach (var item in Property)
            {
                Thread.Sleep(1000);
                Console.WriteLine(item);
            }
            Console.WriteLine("\nЗагрузить данные? ");
            if (Console.ReadLine()?.ToUpper() == "Y") Insert();

            #region OLD
            /*
        string result;
        using (var reader = new StreamReader(@"C:\Users\lustik_ak\source\repos\Mysql_ActionPropirty\new"))
        {
            result = reader.ReadToEnd();
        }
        //System.Console.WriteLine(result);
        int index = 0;
        foreach (var item in result.Split(':'))
        {
            try
            {
                System.Console.WriteLine(item);
                using var connection = new MySqlConnection($"Server={DataBase}; database={_dataBase}; UID=dbuser; password=dbpassword");


                var command = new MySqlCommand($"INSERT ActionPropertyType (deleted, actionType_id, idx, template_id, name, shortName, descr, unit_id, typeName, valueDomain, defaultValue, isVector, norm, sex, age, penalty, penaltyUserProfile, visibleInJobTicket, visibleInTableRedactor, isAssignable, test_id, defaultEvaluation, canChangeOnlyOwner, isActionNameSpecifier, laboratoryCalculator, inActionsSelectionTable, redactorSizeFactor, isFrozen, typeEditable, visibleInDR, userProfile_id, userProfileBehaviour, copyModifier, isVitalParam, vitalParamId, isODIIParam) VALUES (0, 45741, {index}, 0, '{item}', '', '', null, 'String', '', '', 0, '', 0, '', 0, '', 0, 0, 0, NULL, 0, 0, 0, '', 0, 0, 0, 0, 0, NULL, 0, 0, 0, NULL, 0);",
                    connection);
                connection.Open();
                command.ExecuteNonQuery();
                index++;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
            }

        }*/

        }

        private static void Insert()
        {
            if (!string.IsNullOrWhiteSpace(_docName) && !string.IsNullOrWhiteSpace(_docContext))
            {
                Console.Write("Имя документа: ");
                _docName = Console.ReadLine();
                Console.Write("Код документа: ");
                _docContext = Console.ReadLine();
            }

            var index = 0;
            if (!string.IsNullOrWhiteSpace(_docName) && !string.IsNullOrWhiteSpace(_docContext))
            {
                using var connection = new MySqlConnection($"Server={_connect}; database={_dataBase}; UID=dbuser; password=dbpassword");
                connection.StateChange += Mysql_StateChange;

                var command = new MySqlCommand($"SELECT at.id FROM ActionType at JOIN rbPrintTemplate pt ON at.context = pt.context WHERE at.name REGEXP '{_docName}' AND at.context = '{_docContext}' AND at.deleted = 0", connection);
                connection.Open();
                var groupId = command.ExecuteScalar();
                Console.WriteLine("Run...");
                foreach (var propertyName in Property)
                {
                    try
                    {
                        //Thread.Sleep(1 * 30 * 1000);
                        using var connection1 = new MySqlConnection($"Server={_connect}; database={_dataBase}; UID=dbuser; password=dbpassword");
                        var command1 = new MySqlCommand($"INSERT ActionPropertyType (actionType_id, idx, template_id, name, shortName, descr, unit_id, typeName, valueDomain, defaultValue, isVector, norm, sex, age, penalty, penaltyUserProfile, visibleInJobTicket, visibleInTableRedactor, isAssignable, test_id, defaultEvaluation, canChangeOnlyOwner, isActionNameSpecifier, laboratoryCalculator, inActionsSelectionTable, redactorSizeFactor, isFrozen, typeEditable, visibleInDR, userProfile_id, userProfileBehaviour, copyModifier, isVitalParam, vitalParamId, isODIIParam) VALUES ({groupId}, {index}, 0, '{propertyName}', '', '', null, 'String', '', '', 0, '', 0, '', 0, '', 0, 0, 0, NULL, 0, 0, 0, '', 0, 0, 0, 1, 1, NULL, 0, 0, 0, NULL, 0);", connection1);
                        connection1.Open();
                        command1.ExecuteNonQuery();
                        index++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(Create));
            }
        }

        #endregion

        #region Cохранить шаблон печати
        private static void Write(string docContext)
        {
            const string file = @"C:\VISTA_MED\lustik_ak\templates";
            string text;
            if (docContext == string.Empty)
            {
                Console.Write("Код документа: ");
                docContext = Console.ReadLine();
            }
            using (var streamReader = new StreamReader($@"{file}\{docContext}.html"))
            {
                text = streamReader.ReadToEnd().Replace("\'", @"''");
            }

            //Console.WriteLine(text);
            try
            {
                var filename = string.Empty;
                using var connection = new MySqlConnection($"Server={_connect}; database={_dataBase}; UID=dbuser; password=dbpassword");
                connection.StateChange += Mysql_StateChange;

                var command = new MySqlCommand($"SELECT COUNT(*) FROM ActionType at JOIN rbPrintTemplate pt ON at.context = pt.context WHERE at.context ='{docContext}' AND at.name REGEXP '{_docName}' AND at.deleted = 0",
                    connection);
                connection.Open();
                var reader = command.ExecuteScalar().ToString();
                switch (Convert.ToInt32(reader))
                {
                    case 1:
                        {
                            Console.WriteLine("Прописать файл?");
                            switch (Console.ReadLine()?.ToUpper())
                            {
                                case "Y":
                                    filename = $@"{docContext}.html";
                                    break;
                                default:
                                    Console.WriteLine("Файл не прописан");
                                    break;
                            }
                            using var connection1 = new MySqlConnection($"Server={_connect}; database={_dataBase}; UID=dbuser; password=dbpassword");
                            //  connection.StateChange += Mysql_StateChange;

                            var command1 = new MySqlCommand($"UPDATE {_dataBase}.rbPrintTemplate pt JOIN ActionType at ON at.context = pt.context set pt.`default` = '{text}', pt.fileName = '{filename}' WHERE at.name REGEXP '{_docName}' AND pt.context = '{docContext}' AND at.deleted = 0 AND pt.deleted = 0",
                                connection1);
                            connection1.Open();
                            command1.ExecuteNonQuery();
                            Environment.Exit(0);
                            break;
                        }

                    case 0:
                        {
                            using var connection1 = new MySqlConnection($"Server={_connect}; database={_dataBase}; UID=dbuser; password=dbpassword");
                            //  connection.StateChange += Mysql_StateChange;
                            Console.Write("Имя документа: (Используется при создании документа)");
                            _docName = Console.ReadLine();
                            var command1 = new MySqlCommand($"INSERT {_dataBase}.rbPrintTemplate (createDatetime, createPerson_id, modifyDatetime, modifyPerson_id, code, name, context, fileName, `default`, dpdAgreement, type, hideParam, banUnkeptDate, counter_id, deleted, isPatientAgreed, groupName, documentType_id, isEditableInWeb)" +
                                                            $"VALUES (NOW(), 1, NOW(), 1, '{docContext}', '{_docName}', '{docContext}', '', '{text}', 0, 0, 0, 2, NULL, 0, 0, '', NULL, 0);",
                                connection1);
                            connection1.Open();
                            command1.ExecuteNonQuery();
                            Environment.Exit(0);
                            break;
                        }

                    default:
                        {
                            if (Convert.ToInt32(reader) > 1)
                            {
                                Console.Write("Имя документа: ");
                                _docName = Console.ReadLine();
                                Console.WriteLine("Прописать файл?");
                                if (Console.ReadLine()?.ToUpper() == "Y") filename = $@"{docContext}.html";

                                using var connection1 = new MySqlConnection($"Server={_connect}; database={_dataBase}; UID=dbuser; password=dbpassword");
                                //  connection.StateChange += Mysql_StateChange;

                                var command1 = new MySqlCommand($"UPDATE {_dataBase}.rbPrintTemplate pt JOIN ActionType at ON at.context = pt.context set pt.`default` = '{text}', pt.fileName = '{filename}' WHERE at.name REGEXP '{_docName}' AND pt.context = '{docContext}' AND at.deleted = 0 AND pt.deleted = 0",
                                    connection1);
                                connection1.Open();
                                command1.ExecuteNonQuery();
                                Environment.Exit(0);
                            }

                            break;
                        }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception(ex.Message);
            }

            //var file = @"C:\VISTA_MED\lustik_ak\templates";
            //if (!Directory.Exists(file)) { Directory.CreateDirectory(file); }
            //var html = new FileStream($@"{file}\programma.html", FileMode.Create);
            //var htmLwriter =
            //	new StreamWriter(html, Encoding.GetEncoding("UTF-8"));
            //foreach (var item in property)
            //{
            //	htmLwriter.WriteLine(item);
            //}
            //htmLwriter.Close();
            //Process.Start(Environment.ExpandEnvironmentVariables(@"C:\Users\%username%\AppData\Local\Programs\Microsoft VS Code\Code.exe"),
            //	@"C:\VISTA_MED\lustik_ak\templates\programma.html");
            #endregion

        }
        #endregion

        #region Чтение шаблон печати
        private static void Read()
        {
            Property.Clear();
            Console.Write("Код документа: ");
            _docContext = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(_docContext))
            {
                try
                {
                    using var connection = new MySqlConnection($"Server={_connect}; database={_dataBase}; UID=dbuser; password=dbpassword");
                    //  connection.StateChange += Mysql_StateChange;
                    var command = new MySqlCommand($"SELECT Count(*) FROM rbPrintTemplate pt JOIN ActionType at ON at.context = pt.context WHERE pt.context = '{_docContext}' AND at.deleted = 0 AND pt.deleted = 0", connection);
                    connection.Open();
                    var reader = Convert.ToInt64(command.ExecuteScalar());
                    if (reader > 1)
                    {
                        Console.Write("Имя документа: ");
                        _docName = Console.ReadLine();
                        using var connection1 = new MySqlConnection($"Server={_connect}; database={_dataBase}; UID=dbuser; password=dbpassword");
                        var command1 = new MySqlCommand($"SELECT pt.`default` FROM rbPrintTemplate pt JOIN ActionType at ON at.context = pt.context WHERE at.name = '{_docName}' AND pt.context = '{_docContext}' AND at.deleted = 0 AND pt.deleted = 0",
                            connection1);
                        connection1.Open();
                        Property.Add((string)command1.ExecuteScalar());
                    }
                    else if (reader == 1)
                    {
                        using var connection1 = new MySqlConnection($"Server={_connect}; database={_dataBase}; UID=dbuser; password=dbpassword");
                        //  connection.StateChange += Mysql_StateChange;
                        var command1 = new MySqlCommand($"SELECT pt.`default` FROM rbPrintTemplate pt JOIN ActionType at ON at.context = pt.context WHERE pt.context = '{_docContext}' AND at.deleted = 0 AND pt.deleted = 0",
                                                connection1);
                        connection1.Open();
                        Property.Add((string)command1.ExecuteScalar());
                    }
                    //while (reader.Read())
                    //{
                    //	Property.Add(reader[0].ToString());
                    //	//property.Add(reader[1].ToString());
                    //}
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(Create));
            }
            const string file = @"C:\VISTA_MED\lustik_ak\templates";
            if (!Directory.Exists(file)) { Directory.CreateDirectory(file); }
            var html = new FileStream($@"{file}\{_docContext}.html", FileMode.Create);
            var htmLwriter =
                new StreamWriter(html, Encoding.GetEncoding("UTF-8"));
            foreach (var item in Property)
            {
                htmLwriter.Write(item);
            }
            htmLwriter.Close();
            Process.Start(Environment.ExpandEnvironmentVariables(@"C:\Users\%username%\AppData\Local\Programs\Microsoft VS Code\Code.exe"),
                $@"C:\VISTA_MED\lustik_ak\templates\{_docContext}.html");
            Console.Write("Сохранить шаблон?");
            if (Console.ReadLine() == "y" || Console.ReadLine() == "Y")
            {
                Write(_docContext);
            }
        }
        #endregion





        #region Создание шаблона печати
        private static void Create()
        {
            var eventclass = new EventClass();
            eventclass.Notify += event_Notify;
            try
            {
                Console.Write("Имя документа: ");
                _docName = Console.ReadLine();
                Console.Write("Код документа: ");
                _docContext = Console.ReadLine();
                var file = @"C:\VISTA_MED\lustik_ak\templates";
                //var slovar =File.ReadAllLines("Slovar.txt",	Encoding.GetEncoding("windows-1251")); // Создаю массив и считываю все что находится в файле  
                if (!Directory.Exists(file)) { Directory.CreateDirectory(file); }
                var html = new FileStream($@"{file}\{_docContext}.html", FileMode.Create); //создаем файловый поток
                var htmLwriterCreate =
                    new StreamWriter(html, Encoding.GetEncoding("UTF-8")); // соединяем файловый поток с "Потоковым писателем"
                                                                           //const string a = "Text.txt"; //Присваиваю значение к файлу
                htmLwriterCreate.Close();
                //var OurText =
                //	File.ReadAllLines(a,
                //		Encoding.GetEncoding("windows-1251")); // Создаю массив и считываю все что находится в файле


                if (!string.IsNullOrWhiteSpace(_docName) && !string.IsNullOrWhiteSpace(_docContext))
                {
                    using var connection = new MySqlConnection($"Server={_connect}; database={_dataBase}; UID=dbuser; password=dbpassword");
                    connection.StateChange += Mysql_StateChange;

                    var command = new MySqlCommand($"SELECT apt.name FROM ActionPropertyType apt JOIN ActionType at ON apt.actionType_id = at.id WHERE at.name REGEXP '{_docName}' AND at.code = '{_docContext}' AND apt.deleted = 0 ORDER BY apt.idx",
                        connection);
                    connection.Open();
                    var reader = command.ExecuteReader();
                    Console.WriteLine("Run...");
                    while (reader.Read())
                    {
                        Property.Add(reader[0].ToString());
                        //Property.Add(reader[1].ToString());
                    }
                }
                else
                {
                    throw new ArgumentNullException(nameof(Create));
                }

                foreach (var slova in Property)
                {
                    // EndText.AddRange(from t in slova
                    //                  let flag = false
                    //                  where flag != true
                    //                 select "{if: prop.name ==u'" + t + "'}" + "<b>{prop.name} </b>{prop.value}{end:}<br>"+ Environment.NewLine);
                    EndText.Add("				{if: prop.name == u'" + slova + "' and prop.value}" + "<br><b>{prop.name}:</b> {prop.value}{end:}" + Environment.NewLine);
                    //  EndText.Add("<br>" + Environment.NewLine);
                }
                #region htmLwriter
                var result = "<html>" + Environment.NewLine
                    + "" + Environment.NewLine
                    + "<head>" + Environment.NewLine
                    + "	{setPageSize('A4')}" + Environment.NewLine
                    + "	{setOrientation('P')}" + Environment.NewLine
                    + "	{: setLeftMargin(15)}" + Environment.NewLine
                    + "	{: setTopMargin(10)}" + Environment.NewLine
                    + "	{: setBottomMargin(10)}" + Environment.NewLine
                    + "	{: setRightMargin(10)}" + Environment.NewLine
                    + "</head>" + Environment.NewLine
                    + "" + Environment.NewLine
                    + "<body>" + Environment.NewLine
                    + "	{if: action.person.shortName == ''}" + Environment.NewLine
                    + "	<div class=\"check\">" + Environment.NewLine
                    + "		<table width=\"100%\" border=\"0\">" + Environment.NewLine
                    + "			<tr>" + Environment.NewLine
                    + "				<td align=\"center\">" + Environment.NewLine
                    + "					<h1><b>Документ не может быть распечатан.</b></h1>" + Environment.NewLine
                    + "				</td>" + Environment.NewLine
                    + "			</tr>" + Environment.NewLine
                    + "			<tr>" + Environment.NewLine
                    + "				<td align=\"center\">" + Environment.NewLine
                    + "					<h1><b>Документ не подписан.</b></h1>" + Environment.NewLine
                    + "				</td>" + Environment.NewLine
                    + "			</tr>" + Environment.NewLine
                    + "			<tr>" + Environment.NewLine
                    + "				<td align=\"center\">" + Environment.NewLine
                    + "					<table border=\"0\">" + Environment.NewLine
                    + "						<tr>" + Environment.NewLine
                    + "							<td align=\"center\" style=\"vertical-align: top;\">" + Environment.NewLine
                    + "								<h1><b>Нажмите </b></h1>" + Environment.NewLine
                    + "							</td>" + Environment.NewLine
                    + "							<td align=\"center\" style=\"vertical-align: top;\">" + Environment.NewLine
                    + "								<img" + Environment.NewLine
                    + "									src=\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACEAAAAhCAYAAABX5MJvAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAR9SURBVFhHvVjbTxxVGP+duezO7JWFvZBKSA2BAqUaTTWpxr4Y4z/gKzya+OQbj/wDBIhPJjyQGImpqYloeDTxHpNGo6kNLYY04kMtUKAt7G12d8bvOzuzTGF2wC3wI98O5+yZ8/3OdzsfCMdxwFhcXHRKpRKEEHJ8VmB9LLFYDOPj41KZJPHRx/NOJJGEomq8jOfPGAJ2ow5rfw8ffvC+EAsLC05FN6FFDXfB+aFerUDsP4aYnJx0jPwFqFrE/er80KhbqG79CzE/P+/Y8TQU/fxJ2DUL1vbGyUlwwPYV8nh19BLSyYQ7exSPnzzFrdt38GDzEYSqhgY6k6jtbEJxx8ei0NON118aRZ6e0UikrRRyWbw2NoquuEnmrslMOA4nIpFKxHH96ivoTqfdmXD0ksXeeesNGKpCROrubHuIqakpR8/kodApgqCRSa+OjWBscMCdoaimjauWxUnvzhDI7GwJTeM0b+LWb7/j1zsr0AyT0l91Zw9g0x7V7YcQ09PTjpLuaRsTcdPAe+++jYiuy/HTvT189/0P+PPuqhwLOi1z0TUVl4eHcf3Na0gmEtINRSp+n3x+E0I3oEejkqgfXmASQRWKorQX+t6gDbzxvdVV3F65h2g8jlQ2h3Q2j65cHvFMD/5+uIn76//IdQx+z67VYdsNCG+/IOHolT/8DBK53QE4+iOmiVgyDTOZgkmV1pNoPIGyVXNX0kltGw4RcGxH7nN07+bzxNnhgS2nUnlXyT18Cj/kqWTpdwmQS5pRE54hgST4Fe81XWvGgof+vj769K94Fr25ngMCJLobS2E0xNzcnCOS3YHZce3KKC70FpBJp9yZZmCWyhVpEbKlO3sAk+LAiEZQq9XQaDSwsbWF1bX7WN94BJV0+K3H2WHtUGC640Bc7O97hgAjlUyiN59DjopWrjtzREwjKpWzcCp3pVK4PDSI4t4TWbyCEEpCD8jtMHiKWfxE2C1VStd2hSuUxHHY2d3FX2tr2NnZbSn1S4sMZQjHSbvA6JiERf789LMbuPHFl7j51dcol8stxZ5yVuwRCgvmjklw2eZCFqP7xEx1oVK1pHK/Yo+ILUm0R8ckYlSwhoeGEIkYGLjYLzPCr/gwkTB0RMLbPB4z4TRqMHSt5QK/OzwC/AzD/ybhKeKnRSWa6wHHh3/ek5ZFTtMSfkWsfHDgRbw8OoIXCoXW/GEiLKfmDk+BJzyOUnW8MnKJrvmmO4IIsDxXYO6XyrLQ8OZ8chZPmSeHgzBInssS3/z8C7apIBWLRVdKslHxxiUiyX+17cvfS7JW+KVSqVDqVvHtjz/Jjot7iiCEXmA2nbRcpAuLLq06WSH8LmwPjW7iGPUeBvUcqq/98y4wMTs76ygpau+CekxyhTQpEWiatDMSikI9CF3ph2/eVo85MzPjqN3U6LrNyHmCLV3degBFdscNR7JVBPd75ySkD9T2sXXE8vKy88fKXUSTGXkXsAvOHOQSTtvq3i76qWmS/xpYWlpy1tfXUaVIbq452jGdFvxtXzabxcTEhPgPn//iC+G7+moAAAAASUVORK5CYII=\">" + Environment.NewLine
                    + "							</td>" + Environment.NewLine
                    + "						</tr>" + Environment.NewLine
                    + "					</table>" + Environment.NewLine
                    + "				</td>" + Environment.NewLine
                    + "			</tr>" + Environment.NewLine
                    + "		</table>" + Environment.NewLine
                    + "	</div>" + Environment.NewLine
                    + "	{else:}" + Environment.NewLine
                    + "	<div class=\"DOC\">" + Environment.NewLine
                    + "		<div class=\"Title\">" + Environment.NewLine
                    + "			<table width=\"100%\" border=\"0\">" + Environment.NewLine
                    + "				<tr>" + Environment.NewLine
                    + "					<td width=\"150\"></td>" + Environment.NewLine
                    + "					<td align=\"center\">" + Environment.NewLine
                    + "						<h2>{action.title}</h2>" + Environment.NewLine
                    + "					</td>" + Environment.NewLine
                    + "					<td width=\"150\"></td>" + Environment.NewLine
                    + "				</tr>" + Environment.NewLine
                    + "			</table>" + Environment.NewLine
                    + "		</div>" + Environment.NewLine
                    + "		<br>" + Environment.NewLine
                    + "		<div class=\"Client\">" + Environment.NewLine
                    + "			<table width=\"100%\" border=\"0\">" + Environment.NewLine
                    + "				<tr>" + Environment.NewLine
                    + "					<td>" + Environment.NewLine
                    + "						<b>ФИО:</b> {client.fullName}, {client.birthDate}({client.age})" + Environment.NewLine
                    + "						<br><b>Номер и/б:</b> {event.externalId}" + Environment.NewLine
                    + "						<br><b>Дата выполнения:</b> {action.endDate}" + Environment.NewLine
                    + "					</td>" + Environment.NewLine
                    + "				</tr>" + Environment.NewLine
                    + "			</table>" + Environment.NewLine
                    + "		</div>" + Environment.NewLine
                    + "		<div class=\"ActionProperty\">" + Environment.NewLine
                    + "			{for: prop in action}" + Environment.NewLine;
                foreach (var item in result)
                {
                    HtmLwriter(item);
                }
                #endregion                
                foreach (var s in EndText)
                {

                    foreach (var item in s)
                    {
                        HtmLwriter(item);
                    }
                }
                #region htmLwriter2
                string return1 = "			{end:}" + Environment.NewLine
                + "		</div>" + Environment.NewLine
                + "	</div>" + Environment.NewLine
                + "	<br>" + Environment.NewLine
                + "	<div class=\"signature\">" + Environment.NewLine
                + "		<table width=\"100%\" border=\"0\">" + Environment.NewLine
                + "			<tr>" + Environment.NewLine
                + "				<td><b>Врач {action.person.speciality}:</b> </td>" + Environment.NewLine
                + "				<td align=\"right\">{action.person.shortName}</td>" + Environment.NewLine
                + "			</tr>" + Environment.NewLine
                + "		</table>" + Environment.NewLine
                + "	</div>" + Environment.NewLine
                + "	{end:}" + Environment.NewLine
                + "</body>" + Environment.NewLine
                + "" + Environment.NewLine
                + "</html>" + Environment.NewLine;
                foreach (var item in return1)
                {
                    HtmLwriter(item);
                }
                #endregion
                Console.WriteLine(Environment.ExpandEnvironmentVariables(@"C:\Users\%USERNAME%\AppData\Local\Programs\Microsoft VS Code\Code.exe"));
                Process.Start(Environment.ExpandEnvironmentVariables(@"C:\Users\%username%\AppData\Local\Programs\Microsoft VS Code\Code.exe"),
                    $@"C:\VISTA_MED\lustik_ak\templates\{_docContext}.html");
                eventclass.Select(MethodBase.GetCurrentMethod().Name);
                Console.Write("Сохранить шаблон?");
                if (Console.ReadLine() == "y" || Console.ReadLine() == "Y")
                {
                    Write(_docContext);
                }
            }
            catch (Exception e)
            {
                eventclass.Error(MethodBase.GetCurrentMethod().Name, e);
                Console.ReadKey();
            }
        }
        #endregion



        private static void Create_test()
        {
            Property.Clear();
            var eventclass = new EventClass();
            eventclass.Notify += event_Notify;
            try
            {
                // Console.Write("Имя документа: ");
                // _docName = Console.ReadLine();
                Console.Write("Код документа: ");
                _docContext = Console.ReadLine();
                const string file = @"C:\VISTA_MED\lustik_ak\templates";
                //var slovar =File.ReadAllLines("Slovar.txt",	Encoding.GetEncoding("windows-1251")); // Создаю массив и считываю все что находится в файле  
                if (!Directory.Exists(file)) { Directory.CreateDirectory(file); }
                var html = new FileStream($@"{file}\{_docContext}.html", FileMode.Create); //создаем файловый поток
                var htmLwriterCreate =
                    new StreamWriter(html, Encoding.GetEncoding("UTF-8")); // соединяем файловый поток с "Потоковым писателем"
                                                                           //const string a = "Text.txt"; //Присваиваю значение к файлу
                htmLwriterCreate.Close();
                //var OurText =
                //	File.ReadAllLines(a,
                //		Encoding.GetEncoding("windows-1251")); // Создаю массив и считываю все что находится в файле


                if (/*!string.IsNullOrWhiteSpace(_docName) && */!string.IsNullOrWhiteSpace(_docContext))
                {
                    using var fs = new FileStream("Property.txt", FileMode.OpenOrCreate, FileAccess.Read);
                    using (var streamReader = new StreamReader(fs, Encoding.UTF8))
                    {
                        var listPropertyName = streamReader.ReadToEnd().Split((char)13);
                        foreach (var propertyName in listPropertyName.Where(x => x.Contains(':')))
                        {
                            Property.Add(propertyName.Substring(0, propertyName.LastIndexOf(':') + 1).Trim());
                        }
                    }
                    foreach (var item in Property)
                    {
                        Thread.Sleep(1000);
                        Console.WriteLine(item);
                        //Property.Add(item.ToString());
                    }
                }
                else
                {
                    throw new ArgumentNullException(nameof(Create));
                }

                foreach (var slova in Property)
                {
                    // EndText.AddRange(from t in slova
                    //                  let flag = false
                    //                  where flag != true
                    //                 select "{if: prop.name ==u'" + t + "'}" + "<b>{prop.name} </b>{prop.value}{end:}<br>"+ Environment.NewLine);
                    EndText.Add("				{if: prop.name == u'" + slova + "' and prop.value}" + "<br><b>{prop.name}:</b> {prop.value}{end:}" + Environment.NewLine);
                    //  EndText.Add("<br>" + Environment.NewLine);
                }
                #region htmLwriter
                var result = "<html>" + Environment.NewLine
                    + "" + Environment.NewLine
                    + "<head>" + Environment.NewLine
                    + "	{setPageSize('A4')}" + Environment.NewLine
                    + "	{setOrientation('P')}" + Environment.NewLine
                    + "	{: setLeftMargin(15)}" + Environment.NewLine
                    + "	{: setTopMargin(10)}" + Environment.NewLine
                    + "	{: setBottomMargin(10)}" + Environment.NewLine
                    + "	{: setRightMargin(10)}" + Environment.NewLine
                    + "</head>" + Environment.NewLine
                    + "" + Environment.NewLine
                    + "<body>" + Environment.NewLine
                    + "	{if: action.person.shortName == ''}" + Environment.NewLine
                    + "	<div class=\"check\">" + Environment.NewLine
                    + "		<table width=\"100%\" border=\"0\">" + Environment.NewLine
                    + "			<tr>" + Environment.NewLine
                    + "				<td align=\"center\">" + Environment.NewLine
                    + "					<h1><b>Документ не может быть распечатан.</b></h1>" + Environment.NewLine
                    + "				</td>" + Environment.NewLine
                    + "			</tr>" + Environment.NewLine
                    + "			<tr>" + Environment.NewLine
                    + "				<td align=\"center\">" + Environment.NewLine
                    + "					<h1><b>Документ не подписан.</b></h1>" + Environment.NewLine
                    + "				</td>" + Environment.NewLine
                    + "			</tr>" + Environment.NewLine
                    + "			<tr>" + Environment.NewLine
                    + "				<td align=\"center\">" + Environment.NewLine
                    + "					<table border=\"0\">" + Environment.NewLine
                    + "						<tr>" + Environment.NewLine
                    + "							<td align=\"center\" style=\"vertical-align: top;\">" + Environment.NewLine
                    + "								<h1><b>Нажмите </b></h1>" + Environment.NewLine
                    + "							</td>" + Environment.NewLine
                    + "							<td align=\"center\" style=\"vertical-align: top;\">" + Environment.NewLine
                    + "								<img" + Environment.NewLine
                    + "									src=\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACEAAAAhCAYAAABX5MJvAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAR9SURBVFhHvVjbTxxVGP+duezO7JWFvZBKSA2BAqUaTTWpxr4Y4z/gKzya+OQbj/wDBIhPJjyQGImpqYloeDTxHpNGo6kNLYY04kMtUKAt7G12d8bvOzuzTGF2wC3wI98O5+yZ8/3OdzsfCMdxwFhcXHRKpRKEEHJ8VmB9LLFYDOPj41KZJPHRx/NOJJGEomq8jOfPGAJ2ow5rfw8ffvC+EAsLC05FN6FFDXfB+aFerUDsP4aYnJx0jPwFqFrE/er80KhbqG79CzE/P+/Y8TQU/fxJ2DUL1vbGyUlwwPYV8nh19BLSyYQ7exSPnzzFrdt38GDzEYSqhgY6k6jtbEJxx8ei0NON118aRZ6e0UikrRRyWbw2NoquuEnmrslMOA4nIpFKxHH96ivoTqfdmXD0ksXeeesNGKpCROrubHuIqakpR8/kodApgqCRSa+OjWBscMCdoaimjauWxUnvzhDI7GwJTeM0b+LWb7/j1zsr0AyT0l91Zw9g0x7V7YcQ09PTjpLuaRsTcdPAe+++jYiuy/HTvT189/0P+PPuqhwLOi1z0TUVl4eHcf3Na0gmEtINRSp+n3x+E0I3oEejkqgfXmASQRWKorQX+t6gDbzxvdVV3F65h2g8jlQ2h3Q2j65cHvFMD/5+uIn76//IdQx+z67VYdsNCG+/IOHolT/8DBK53QE4+iOmiVgyDTOZgkmV1pNoPIGyVXNX0kltGw4RcGxH7nN07+bzxNnhgS2nUnlXyT18Cj/kqWTpdwmQS5pRE54hgST4Fe81XWvGgof+vj769K94Fr25ngMCJLobS2E0xNzcnCOS3YHZce3KKC70FpBJp9yZZmCWyhVpEbKlO3sAk+LAiEZQq9XQaDSwsbWF1bX7WN94BJV0+K3H2WHtUGC640Bc7O97hgAjlUyiN59DjopWrjtzREwjKpWzcCp3pVK4PDSI4t4TWbyCEEpCD8jtMHiKWfxE2C1VStd2hSuUxHHY2d3FX2tr2NnZbSn1S4sMZQjHSbvA6JiERf789LMbuPHFl7j51dcol8stxZ5yVuwRCgvmjklw2eZCFqP7xEx1oVK1pHK/Yo+ILUm0R8ckYlSwhoeGEIkYGLjYLzPCr/gwkTB0RMLbPB4z4TRqMHSt5QK/OzwC/AzD/ybhKeKnRSWa6wHHh3/ek5ZFTtMSfkWsfHDgRbw8OoIXCoXW/GEiLKfmDk+BJzyOUnW8MnKJrvmmO4IIsDxXYO6XyrLQ8OZ8chZPmSeHgzBInssS3/z8C7apIBWLRVdKslHxxiUiyX+17cvfS7JW+KVSqVDqVvHtjz/Jjot7iiCEXmA2nbRcpAuLLq06WSH8LmwPjW7iGPUeBvUcqq/98y4wMTs76ygpau+CekxyhTQpEWiatDMSikI9CF3ph2/eVo85MzPjqN3U6LrNyHmCLV3degBFdscNR7JVBPd75ySkD9T2sXXE8vKy88fKXUSTGXkXsAvOHOQSTtvq3i76qWmS/xpYWlpy1tfXUaVIbq452jGdFvxtXzabxcTEhPgPn//iC+G7+moAAAAASUVORK5CYII=\">" + Environment.NewLine
                    + "							</td>" + Environment.NewLine
                    + "						</tr>" + Environment.NewLine
                    + "					</table>" + Environment.NewLine
                    + "				</td>" + Environment.NewLine
                    + "			</tr>" + Environment.NewLine
                    + "		</table>" + Environment.NewLine
                    + "	</div>" + Environment.NewLine
                    + "	{else:}" + Environment.NewLine
                    + "	<div class=\"DOC\">" + Environment.NewLine
                    + "		<div class=\"Title\">" + Environment.NewLine
                    + "			<table width=\"100%\" border=\"0\">" + Environment.NewLine
                    + "				<tr>" + Environment.NewLine
                    + "					<td width=\"150\"></td>" + Environment.NewLine
                    + "					<td align=\"center\">" + Environment.NewLine
                    + "						<h2>{action.title}</h2>" + Environment.NewLine
                    + "					</td>" + Environment.NewLine
                    + "					<td width=\"150\"></td>" + Environment.NewLine
                    + "				</tr>" + Environment.NewLine
                    + "			</table>" + Environment.NewLine
                    + "		</div>" + Environment.NewLine
                    + "		<br>" + Environment.NewLine
                    + "		<div class=\"Client\">" + Environment.NewLine
                    + "			<table width=\"100%\" border=\"0\">" + Environment.NewLine
                    + "				<tr>" + Environment.NewLine
                    + "					<td>" + Environment.NewLine
                    + "						<b>ФИО:</b> {client.fullName}, {client.birthDate}({client.age})" + Environment.NewLine
                    + "						<br><b>Номер и/б:</b> {event.externalId}" + Environment.NewLine
                    + "						<br><b>Дата выполнения:</b> {action.endDate}" + Environment.NewLine
                    + "					</td>" + Environment.NewLine
                    + "				</tr>" + Environment.NewLine
                    + "			</table>" + Environment.NewLine
                    + "		</div>" + Environment.NewLine
                    + "		<div class=\"ActionProperty\">" + Environment.NewLine
                    + "			{for: prop in action}" + Environment.NewLine;
                foreach (var item in result)
                {
                    HtmLwriter(item);
                }
                #endregion
                foreach (var item in EndText.SelectMany(s => s))
                {
                    HtmLwriter(item);
                }
                #region htmLwriter2
                var return1 = "			{end:}" + Environment.NewLine
                                          + "		</div>" + Environment.NewLine
                                          + "	</div>" + Environment.NewLine
                                          + "	<br>" + Environment.NewLine
                                          + "	<div class=\"signature\">" + Environment.NewLine
                                          + "		<table width=\"100%\" border=\"0\">" + Environment.NewLine
                                          + "			<tr>" + Environment.NewLine
                                          + "				<td><b>Врач {action.person.speciality}:</b> </td>" + Environment.NewLine
                                          + "				<td align=\"right\">{action.person.shortName}</td>" + Environment.NewLine
                                          + "			</tr>" + Environment.NewLine
                                          + "		</table>" + Environment.NewLine
                                          + "	</div>" + Environment.NewLine
                                          + "	{end:}" + Environment.NewLine
                                          + "</body>" + Environment.NewLine
                                          + "" + Environment.NewLine
                                          + "</html>" + Environment.NewLine;
                foreach (var item in return1)
                {
                    HtmLwriter(item);
                }
                #endregion
                Console.WriteLine(Environment.ExpandEnvironmentVariables(@"C:\Users\%USERNAME%\AppData\Local\Programs\Microsoft VS Code\Code.exe"));
                Process.Start(Environment.ExpandEnvironmentVariables(@"C:\Users\%username%\AppData\Local\Programs\Microsoft VS Code\Code.exe"),
                    $@"C:\VISTA_MED\lustik_ak\templates\{_docContext}.html");
                eventclass.Select(MethodBase.GetCurrentMethod().Name);
                Console.Write("Сохранить шаблон?");
                if (Console.ReadLine() == "y" || Console.ReadLine() == "Y")
                {
                    Write(_docContext);
                }
            }
            catch (Exception e)
            {
                eventclass.Error(MethodBase.GetCurrentMethod().Name, e);
                Console.ReadKey();
            }
        }



        #region Заполнение rbThesaurus
        private static void RbThesaurus(string code, int startCode = 1)
        {
            //var start_code = 1;
            //var code = "13-25";
            ////const string path = @"Property.txt";
            try
            {
                Query1(code, startCode);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Query1(code, startCode);
            }

        }
        private static void Query1(string code, int startCode)
        {
            var insCode = $"{code}-";
            var selCode = $"^{code}";
            
                string groupId;
                using (var connection = new MySqlConnection($"Server={_connect}; database={_dataBase}; UID=dbuser; password=dbpassword"))
                {
                    var sqlSel = $"SELECT t.id FROM rbThesaurus t WHERE t.code REGEXP '{selCode}';";
                    var command = new MySqlCommand(sqlSel, connection);
                    connection.Open();
                    groupId = command.ExecuteScalar().ToString();
                }
                Console.WriteLine(groupId);
                using (var sr = new StreamReader(Path, Encoding.Default))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var sql = "INSERT LOW_PRIORITY rbThesaurus (createDatetime, createPerson_id, modifyDatetime, modifyPerson_id, group_id, code, name, template)" +
                        $"VALUES (NOW(), 1, NOW(), 1, '{groupId}', CONCAT('{insCode}', {startCode}), '{line}', '%s: {line}');";
                        using var connection = new MySqlConnection($"Server={_connect}; database={_dataBase}; UID=dbuser; password=dbpassword");
                        var command = new MySqlCommand(sql, connection);
                        connection.Open();
                        command.ExecuteNonQuery();
                        startCode++;
                    }
                }
                using (var connection = new MySqlConnection($"Server={_connect}; database={_dataBase}; UID=dbuser; password=dbpassword"))
                {
                    var sqlSel = $"SELECT * FROM rbThesaurus t WHERE t.code REGEXP '{selCode}';";
                    var command = new MySqlCommand(sqlSel, connection);
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        for (var i = 0; i <= 8; i++)
                        {
                            Console.Write($"{reader[i]} ");
                        }
                        Console.WriteLine();
                    }
                }
            }
        #endregion

        #region Заполнение ActionPropertyType
        private static void ActionPropertyType(string code, int startCode = 0)
        {
            //var start_code = 40;
            //var code = "1193-26958";
            //const string path = @"Property.txt";
            try
            {
                Query(code, startCode);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Query(code, startCode);
            }
        }

        private static void Query(string code, int startCode)
        {
            using (var sr = new StreamReader(Path, Encoding.Default))
            {
                using var connection = new MySqlConnection($"Server={_connect}; database={_dataBase}; UID=dbuser; password=dbpassword");
                connection.StateChange += Mysql_StateChange;
                var command = new MySqlCommand($"SELECT at.id FROM ActionType at WHERE at.name REGEXP '{_docName}' AND at.code = '{code}' AND at.deleted = 0",
                    connection);
                connection.Open();
                var groupId = command.ExecuteScalar();
                Console.WriteLine("Run...");
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    //Thread.Sleep(30 * 1000);

                    //var sql = $"INSERT ActionPropertyType (deleted, actionType_id, idx, template_id, name, shortName, descr, unit_id, typeName, valueDomain, defaultValue, isVector, norm, sex, age, penalty, penaltyUserProfile, visibleInJobTicket, visibleInTableRedactor, isAssignable, test_id, defaultEvaluation, canChangeOnlyOwner, isActionNameSpecifier, laboratoryCalculator, inActionsSelectionTable, redactorSizeFactor, isFrozen, typeEditable, visibleInDR, userProfile_id, userProfileBehaviour, copyModifier, isVitalParam, vitalParamId, isODIIParam) SELECT apt.deleted, apt.actionType_id, {start_code}, apt.template_id, '{line}', apt.shortName, apt.descr, apt.unit_id, apt.typeName, apt.valueDomain, apt.defaultValue, apt.isVector, apt.norm, apt.sex, apt.age, apt.penalty, apt.penaltyUserProfile, apt.visibleInJobTicket, apt.visibleInTableRedactor, apt.isAssignable, apt.test_id, apt.defaultEvaluation, apt.canChangeOnlyOwner, apt.isActionNameSpecifier, apt.laboratoryCalculator, apt.inActionsSelectionTable, apt.redactorSizeFactor, apt.isFrozen, apt.typeEditable, apt.visibleInDR, apt.userProfile_id, apt.userProfileBehaviour, apt.copyModifier, apt.isVitalParam, apt.vitalParamId, apt.isODIIParam FROM ActionPropertyType apt WHERE apt.actionType_id = (SELECT at.id FROM ActionType at WHERE at.code = '{code}') AND apt.idx = 0 AND apt.deleted = 0";
                    var sql = $"INSERT ActionPropertyType (actionType_id, idx, template_id, name, shortName, descr, unit_id, typeName, valueDomain, defaultValue, isVector, norm, sex, age, penalty, penaltyUserProfile, visibleInJobTicket, visibleInTableRedactor, isAssignable, test_id, defaultEvaluation, canChangeOnlyOwner, isActionNameSpecifier, laboratoryCalculator, inActionsSelectionTable, redactorSizeFactor, isFrozen, typeEditable, visibleInDR, userProfile_id, userProfileBehaviour, copyModifier, isVitalParam, vitalParamId, isODIIParam) VALUES ({groupId}, {startCode}, 0, '{line}', '', '', null, 'String', '', '', 0, '', 0, '', 0, '', 0, 0, 0, NULL, 0, 0, 0, '', 0, 0, 0, 1, 1, NULL, 0, 0, 0, NULL, 0);";
                    using var connection1 = new MySqlConnection($"Server={_connect}; database={_dataBase}; UID=dbuser; password=dbpassword");
                    var command1 = new MySqlCommand(sql, connection1);
                    connection1.Open();
                    command1.ExecuteNonQuery();
                    startCode++;
                }
            }
            using (var connection1 = new MySqlConnection($"Server={_connect}; database={_dataBase}; UID=dbuser; password=dbpassword"))
            {
                var sqlSel = $"SELECT * FROM ActionPropertyType apt WHERE apt.actionType_id = (SELECT at.id FROM ActionType at WHERE at.code = '{code}') AND apt.deleted=0 ORDER BY apt.idx DESC";
                var command1 = new MySqlCommand(sqlSel, connection1);
                connection1.Open();
                var reader = command1.ExecuteReader();
                while (reader.Read())
                {
                    for (var i = 0; i <= 9; i++)
                    {
                        Console.Write($"{reader[i]} ");
                    }
                    Console.WriteLine();
                }

                Console.WriteLine("Создать шаблоп печати?");
                if (Console.ReadLine()?.ToUpper() == "Y") Create();
            }
        }
        #endregion

        #region Заполнение OrgStructure
        private static void OrgStructure()
        {
            //var start_code = 40;
            //var code = "1193-26958";
            //const string path = @"Property.txt";
            try
            {
                using (var sr = new StreamReader(Path, Encoding.Default))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        //Thread.Sleep(30 * 1000);
                        var sql = $"INSERT LOW_PRIORITY OrgStructure (createDatetime, createPerson_id, modifyDatetime, modifyPerson_id, deleted, organisation_id, code, name, parent_id, type, net_id, chief_id, headNurse_id, isArea, hasHospitalBeds, hasStocks, hasDayStationary, infisCode, infisInternalCode, infisDepTypeCode, availableForExternal, Address, infisTariffCode, inheritEventTypes, inheritActionTypes, inheritGaps, bookkeeperCode, dayLimit, storageCode, salaryPercentage, attachCode, isVisibleInDR, tfomsCode, syncGUID, quota, miacCode, netrica_Code, idLPU_egisz, netrica_Code_UO, netrica_Code_IEMK) VALUES (NOW(), 1193, NOW(), 1193, 0, 0, '{line}', '{line}', 167, 0, NULL, NULL, NULL, 0, 0, 0, 0, '', '', '', 0, '', '', 0, 0, 0, '', 0, '', 0, 0, 1, NULL, '', 0, '', '', NULL, '', NULL);";
                        using var connection = new MySqlConnection($"Server={_connect}; database={_dataBase}; UID=dbuser; password=dbpassword");
                        var command = new MySqlCommand(sql, connection);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
                using (var connection = new MySqlConnection($"Server={_connect}; database={_dataBase}; UID=dbuser; password=dbpassword"))
                {
                    const string sqlSel = "SELECT * FROM OrgStructure os WHERE os.parent_id = (SELECT os1.id FROM OrgStructure os1 WHERE os1.name REGEXP 'ручной');";
                    var command = new MySqlCommand(sqlSel, connection);
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        for (var i = 0; i <= 9; i++)
                        {
                            Console.Write($"{reader[i]} ");
                        }
                        Console.WriteLine();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        #endregion
        private static void Mysql_StateChange(object sender, StateChangeEventArgs e)
        {
            Console.WriteLine($"Mysql Connect: {e.CurrentState}");
        }

        private static void event_Notify(object sender, MyEventArgs e)
        {
            Console.WriteLine(e.GetInfo());
        }
    }
}
