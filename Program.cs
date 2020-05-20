using System.Threading;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using MySql.Data.MySqlClient;

namespace HTMLTemplate
{
    internal class Program
    {
        private static readonly List<string> EndText = new List<string>();
        private static readonly List<string> Property = new List<string>();
        private static string _docName = string.Empty;
        private static string _docContext = string.Empty;
        private static void Main()
        {
            Consol();
        }

        private static void Consol()
        {
            var countries = new Dictionary<int, string>(10)
            {
                {1, "Создание шаблона печати"},
                {2, "Чтение полей из файла"},
                {3, "Чтение шаблон печати"},
                {4, "Cохранить шаблон печати"}
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
                default:
                    Console.WriteLine("Значение не верно");
                    break;
            }
        }
        #region Чтение полей из файла
        private static void ReadFile()
        {
            Property.Clear();
            using (FileStream fs = new FileStream("Property.txt", FileMode.OpenOrCreate, FileAccess.Read))
            {
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
                switch (Console.ReadLine().ToUpper())
                {
                    //case "y":
                    case "Y":
                        Insert();
                        break;
                }
            }

            void Insert()
            {
                Console.Write("Имя документа: ");
                _docName = Console.ReadLine();
                Console.Write("Код документа: ");
                _docContext = Console.ReadLine();
                var index = 0;
                if (!string.IsNullOrWhiteSpace(_docName) && !string.IsNullOrWhiteSpace(_docContext))
                {
                    using var connection = new MySqlConnection("Server=192.168.3.200; database=s11; UID=report; password=dbreport");
                    connection.StateChange += Mysql_StateChange;

                    var command = new MySqlCommand($"SELECT at.id FROM ActionType at JOIN rbPrintTemplate pt ON at.context = pt.context WHERE at.name REGEXP '{_docName}' AND at.context = '{_docContext}' AND at.deleted = 0",
                        connection);
                    connection.Open();
                    var groupId = command.ExecuteScalar();
                    foreach (var propertyName in Property)
                    {
                        try
                        {
                            Thread.Sleep(1 * 30 * 1000);
                            using var connection1 = new MySqlConnection("Server=192.168.3.200; database=s11; UID=dbuser; password=dbpassword");
                            var command1 = new MySqlCommand($"INSERT ActionPropertyType (deleted, actionType_id, idx, template_id, name, shortName, descr, unit_id, typeName, valueDomain, defaultValue, isVector, norm, sex, age, penalty, penaltyUserProfile, visibleInJobTicket, visibleInTableRedactor, isAssignable, test_id, defaultEvaluation, canChangeOnlyOwner, isActionNameSpecifier, laboratoryCalculator, inActionsSelectionTable, redactorSizeFactor, isFrozen, typeEditable, visibleInDR, userProfile_id, userProfileBehaviour, copyModifier, isVitalParam, vitalParamId, isODIIParam) VALUES (0, {groupId}, {index}, 0, '{propertyName}', '', '', null, 'String', '', '', 0, '', 0, '', 0, '', 0, 0, 0, NULL, 0, 0, 0, '', 0, 0, 0, 0, 1, NULL, 0, 0, 0, NULL, 0);",
                                connection1);
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
                using var connection = new MySqlConnection("Server=192.168.3.200; database=s11; UID=dbuser; password=dbpassword");


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
        #endregion

        #region Cохранить шаблон печати
        private static void Write(string _docContext)
        {
            var file = @"C:\VISTA_MED\lustik_ak\templates";
            string text;
            if (_docContext == string.Empty)
            {
                Console.Write("Имя документа: ");
                _docName = Console.ReadLine();
                Console.Write("Код документа: ");
                _docContext = Console.ReadLine();
            }
            using (var streamReader = new StreamReader($@"{file}\{_docContext}.html"))
            {
                text = streamReader.ReadToEnd().Replace("\'", @"''");
            }

            //Console.WriteLine(text);
            try
            {
                var Filename = string.Empty;
                using var connection = new MySqlConnection("Server=192.168.3.200; database=s11; UID=report; password=dbreport");
                connection.StateChange += Mysql_StateChange;

                var command = new MySqlCommand($"SELECT COUNT(*) FROM ActionType at JOIN rbPrintTemplate pt ON at.context = pt.context WHERE at.context ='{_docContext}' AND at.name REGEXP '{_docName}' AND at.deleted = 0",
                    connection);
                connection.Open();
                var reader = command.ExecuteScalar().ToString();
                if (Convert.ToInt32(reader) == 1)
                {
                    System.Console.WriteLine("Прописать файл?");
                    switch (Console.ReadLine().ToUpper())
                    {
                        case "Y":
                        Filename = $@"{_docContext}.html";
                        break;
                        default:
                        break;
                    }
                    using var connection1 = new MySqlConnection("Server=192.168.3.200; database=s11; UID=dbuser; password=dbpassword");
                    //  connection.StateChange += Mysql_StateChange;

                    var command1 = new MySqlCommand($"UPDATE s11.rbPrintTemplate pt JOIN ActionType at ON at.context = pt.context set pt.`default` = '{text}', pt.fileName = '{Filename}' WHERE at.name REGEXP '{_docName}' AND pt.context = '{_docContext}' AND at.deleted = 0 AND pt.deleted = 0",
                        connection1);
                    connection1.Open();
                    command1.ExecuteNonQuery();
                }
                else if (Convert.ToInt32(reader) == 0)
                {
                    using var connection1 = new MySqlConnection("Server=192.168.3.200; database=s11; UID=dbuser; password=dbpassword");
                    //  connection.StateChange += Mysql_StateChange;

                    var command1 = new MySqlCommand($"INSERT s11.rbPrintTemplate (createDatetime, createPerson_id, modifyDatetime, modifyPerson_id, code, name, context, fileName, `default`, dpdAgreement, type, hideParam, banUnkeptDate, counter_id, deleted, isPatientAgreed, groupName, documentType_id, isEditableInWeb, pageOrientation)" +
                    $"VALUES (NOW(), 1193, NOW(), 1193, '{_docContext}', '{_docName}', '{_docContext}', '', '{text}', 0, 0, 0, 2, NULL, 0, 0, '', NULL, 0, 'P');",
                        connection1);
                    connection1.Open();
                    command1.ExecuteNonQuery();
                }
                else if (Convert.ToInt32(reader) > 1)
                {
                    Console.Write("Имя документа: ");
                    _docName = Console.ReadLine();
                    System.Console.WriteLine("Прописать файл?");
                    switch (Console.ReadLine().ToUpper())
                    {
                        case "Y":
                        Filename = $@"{_docContext}.html";
                        break;
                        default:
                        break;
                    }
                    using var connection1 = new MySqlConnection("Server=192.168.3.200; database=s11; UID=dbuser; password=dbpassword");
                    //  connection.StateChange += Mysql_StateChange;

                    var command1 = new MySqlCommand($"UPDATE s11.rbPrintTemplate pt JOIN ActionType at ON at.context = pt.context set pt.`default` = '{text}', pt.fileName = '{Filename}' WHERE at.name REGEXP '{_docName}' AND pt.context = '{_docContext}' AND at.deleted = 0 AND pt.deleted = 0",
                        connection1);
                    connection1.Open();
                    command1.ExecuteNonQuery();
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
                    using var connection = new MySqlConnection("Server=192.168.3.200; database=s11; UID=report; password=dbreport");
                    //  connection.StateChange += Mysql_StateChange;
                    var command = new MySqlCommand($"SELECT Count(*) FROM rbPrintTemplate pt JOIN ActionType at ON at.context = pt.context WHERE pt.context = '{_docContext}' AND at.deleted = 0 AND pt.deleted = 0",
                                            connection);
                    connection.Open();
                    var reader = Convert.ToInt64(command.ExecuteScalar());
                    if (reader > 1)
                    {
                        Console.Write("Имя документа: ");
                        _docName = Console.ReadLine();
                        using var connection1 = new MySqlConnection("Server=192.168.3.200; database=s11; UID=report; password=dbreport");
                        var command1 = new MySqlCommand($"SELECT pt.`default` FROM rbPrintTemplate pt JOIN ActionType at ON at.context = pt.context WHERE at.name = '{_docName}' AND pt.context = '{_docContext}' AND at.deleted = 0 AND pt.deleted = 0",
                            connection1);
                        connection1.Open();
                        Property.Add((string)command1.ExecuteScalar());
                    }
                    else if (reader == 1)
                    {
                        using var connection1 = new MySqlConnection("Server=192.168.3.200; database=s11; UID=report; password=dbreport");
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
                var htmLwriter =
                    new StreamWriter(html, Encoding.GetEncoding("UTF-8")); // соединяем файловый поток с "Потоковым писателем"
                                                                           //const string a = "Text.txt"; //Присваиваю значение к файлу

                //var OurText =
                //	File.ReadAllLines(a,
                //		Encoding.GetEncoding("windows-1251")); // Создаю массив и считываю все что находится в файле


                if (!string.IsNullOrWhiteSpace(_docName) && !string.IsNullOrWhiteSpace(_docContext))
                {
                    using var connection = new MySqlConnection("Server=192.168.3.200; database=s11; UID=report; password=dbreport");
                    connection.StateChange += Mysql_StateChange;

                    var command = new MySqlCommand($"SELECT apt.name FROM ActionPropertyType apt JOIN ActionType at ON apt.actionType_id = at.id WHERE at.name REGEXP '{_docName}' AND at.context = '{_docContext}' AND apt.deleted = 0 ORDER BY apt.idx",
                        connection);
                    connection.Open();
                    var reader = command.ExecuteReader();
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
                    EndText.Add("{if: prop.name == u'" + slova + "' and prop.value}" + "<b>{prop.name}:</b> {prop.value}<br />{end:}" + Environment.NewLine);
                    //  EndText.Add("<br>" + Environment.NewLine);
                }


                htmLwriter.WriteLine("<!DOCTYPE HTML>");
                htmLwriter.WriteLine("<html>");
                htmLwriter.WriteLine("<head>");
                htmLwriter.WriteLine("{setPageSize('A4')}" + Environment.NewLine +
                                     "{setOrientation('P')}" + Environment.NewLine +
                                     "{: setLeftMargin(30)}" + Environment.NewLine +
                                     "{: setTopMargin(25)}" + Environment.NewLine +
                                     "{: setBottomMargin(15)}" + Environment.NewLine +
                                     "{: setRightMargin(15)}");
                htmLwriter.WriteLine("</head>");
                htmLwriter.WriteLine("<body>");
                htmLwriter.WriteLine("	{if: action.person.shortName == ''}");
                htmLwriter.WriteLine("	<div class=\"check\">");
                htmLwriter.WriteLine("		<table width=\"100%\" border=\"0\">");
                htmLwriter.WriteLine("			<tr>");
                htmLwriter.WriteLine("				<td align=\"center\">");
                htmLwriter.WriteLine("					<h1><b>Документ не может быть распечатан.</b></h1>");
                htmLwriter.WriteLine("				</td>");
                htmLwriter.WriteLine("			</tr>");
                htmLwriter.WriteLine("			<tr>");
                htmLwriter.WriteLine("				<td align=\"center\">");
                htmLwriter.WriteLine("					<h1><b>Документ не подписан.</b></h1>");
                htmLwriter.WriteLine("				</td>");
                htmLwriter.WriteLine("			</tr>");
                htmLwriter.WriteLine("			<tr>");
                htmLwriter.WriteLine("				<td align=\"center\">");
                htmLwriter.WriteLine("					<table border=\"0\">");
                htmLwriter.WriteLine("						<tr>");
                htmLwriter.WriteLine("							<td align=\"center\" style=\"vertical-align: top;\">");
                htmLwriter.WriteLine("								<h1><b>Нажмите </b></h1>");
                htmLwriter.WriteLine("							</td>");
                htmLwriter.WriteLine("							<td align=\"center\" style=\"vertical-align: top;\">");
                htmLwriter.WriteLine("								<img");
                htmLwriter.WriteLine("									src=\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACEAAAAhCAYAAABX5MJvAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAR9SURBVFhHvVjbTxxVGP+duezO7JWFvZBKSA2BAqUaTTWpxr4Y4z/gKzya+OQbj/wDBIhPJjyQGImpqYloeDTxHpNGo6kNLYY04kMtUKAt7G12d8bvOzuzTGF2wC3wI98O5+yZ8/3OdzsfCMdxwFhcXHRKpRKEEHJ8VmB9LLFYDOPj41KZJPHRx/NOJJGEomq8jOfPGAJ2ow5rfw8ffvC+EAsLC05FN6FFDXfB+aFerUDsP4aYnJx0jPwFqFrE/er80KhbqG79CzE/P+/Y8TQU/fxJ2DUL1vbGyUlwwPYV8nh19BLSyYQ7exSPnzzFrdt38GDzEYSqhgY6k6jtbEJxx8ei0NON118aRZ6e0UikrRRyWbw2NoquuEnmrslMOA4nIpFKxHH96ivoTqfdmXD0ksXeeesNGKpCROrubHuIqakpR8/kodApgqCRSa+OjWBscMCdoaimjauWxUnvzhDI7GwJTeM0b+LWb7/j1zsr0AyT0l91Zw9g0x7V7YcQ09PTjpLuaRsTcdPAe+++jYiuy/HTvT189/0P+PPuqhwLOi1z0TUVl4eHcf3Na0gmEtINRSp+n3x+E0I3oEejkqgfXmASQRWKorQX+t6gDbzxvdVV3F65h2g8jlQ2h3Q2j65cHvFMD/5+uIn76//IdQx+z67VYdsNCG+/IOHolT/8DBK53QE4+iOmiVgyDTOZgkmV1pNoPIGyVXNX0kltGw4RcGxH7nN07+bzxNnhgS2nUnlXyT18Cj/kqWTpdwmQS5pRE54hgST4Fe81XWvGgof+vj769K94Fr25ngMCJLobS2E0xNzcnCOS3YHZce3KKC70FpBJp9yZZmCWyhVpEbKlO3sAk+LAiEZQq9XQaDSwsbWF1bX7WN94BJV0+K3H2WHtUGC640Bc7O97hgAjlUyiN59DjopWrjtzREwjKpWzcCp3pVK4PDSI4t4TWbyCEEpCD8jtMHiKWfxE2C1VStd2hSuUxHHY2d3FX2tr2NnZbSn1S4sMZQjHSbvA6JiERf789LMbuPHFl7j51dcol8stxZ5yVuwRCgvmjklw2eZCFqP7xEx1oVK1pHK/Yo+ILUm0R8ckYlSwhoeGEIkYGLjYLzPCr/gwkTB0RMLbPB4z4TRqMHSt5QK/OzwC/AzD/ybhKeKnRSWa6wHHh3/ek5ZFTtMSfkWsfHDgRbw8OoIXCoXW/GEiLKfmDk+BJzyOUnW8MnKJrvmmO4IIsDxXYO6XyrLQ8OZ8chZPmSeHgzBInssS3/z8C7apIBWLRVdKslHxxiUiyX+17cvfS7JW+KVSqVDqVvHtjz/Jjot7iiCEXmA2nbRcpAuLLq06WSH8LmwPjW7iGPUeBvUcqq/98y4wMTs76ygpau+CekxyhTQpEWiatDMSikI9CF3ph2/eVo85MzPjqN3U6LrNyHmCLV3degBFdscNR7JVBPd75ySkD9T2sXXE8vKy88fKXUSTGXkXsAvOHOQSTtvq3i76qWmS/xpYWlpy1tfXUaVIbq452jGdFvxtXzabxcTEhPgPn//iC+G7+moAAAAASUVORK5CYII=\">");
                htmLwriter.WriteLine("							</td>");
                htmLwriter.WriteLine("						</tr>");
                htmLwriter.WriteLine("					</table>");
                htmLwriter.WriteLine("				</td>");
                htmLwriter.WriteLine("			</tr>");
                htmLwriter.WriteLine("		</table>");
                htmLwriter.WriteLine("	</div>");
                htmLwriter.WriteLine("	{else:}");
                htmLwriter.WriteLine("	<h2 align=\"center\"><b>{action.title}</b></h2><br>");
                htmLwriter.WriteLine("	<table width=\"100%\">");
                htmLwriter.WriteLine("		<tr><td>ФИО: {client.fullName}, {client.birthDate}({client.age}) Номер и/б: {event.externalId}</td></tr>");
                htmLwriter.WriteLine("		<tr>");
                htmLwriter.WriteLine("			<td>Дата/Время: {action.endDate}</td>");
                htmLwriter.WriteLine("		</tr>");
                htmLwriter.WriteLine("	</table>");
                htmLwriter.WriteLine("	{for: prop in action}");
                htmLwriter.Close();
                foreach (var s in EndText)
                {
                    Thread.Sleep(30 * 1000);
                    var htmLwriter1 =
                        new StreamWriter($@"{file}\{_docContext}.html", true, Encoding.GetEncoding("UTF-8"));

                    htmLwriter1.Write($"	{s}"); // происходит запись в файл HTML
                    htmLwriter1.Close();
                }
                var htmLwriter2 =
                    new StreamWriter($@"{file}\{_docContext}.html", true, Encoding.GetEncoding("UTF-8"));
                htmLwriter2.WriteLine("	{end:}");
                htmLwriter2.WriteLine("	{end:}");
                htmLwriter2.WriteLine("</body>");
                htmLwriter2.WriteLine("</html>");
                htmLwriter2.Close(); // закрываем файловый поток
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
