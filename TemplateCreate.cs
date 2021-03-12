using System;
using System.Collections.Generic;
using System.Reflection;

namespace HTMLTemplate
{
    public class TemplateCreate
    {
        
        public TemplateCreate(SqlConnect connect)
        {
            var eventclass = new EventClass();
            var property = new List<string>();
            try
            {
                using var file = new TemplateFile();
                var sqlSelect = $"SELECT apt.name FROM ActionPropertyType apt " +
                                $"JOIN ActionType at ON apt.actionType_id = at.id " +
                                $"WHERE at.name REGEXP '{file.FileName}' AND at.code = '{file.FileCode}' AND apt.deleted = 0 ORDER BY apt.idx";
                var sql = new SqlSelect(connect, sqlSelect);
                while (sql.Reader.Read())
                {
                    property.Add(sql.Reader[0].ToString());
                }

                // while (reader.Read())
                // Property.Add(reader[0].ToString());
                //Property.Add(reader[1].ToString());
                // }
                // else
                // {
                // throw new ArgumentNullException(nameof(Create));
                // }
//
//                 foreach (var slova in Property)
//                 //                     EndText.AddRange(from t in slova
// //                                      let flag = false
// //                                      where flag != true
// //                                     select "{if: prop.name ==u'" + t + "'}" + "<b>{prop.name} </b>{prop.value}{end:}<br>"+ Environment.NewLine);
//                     EndText.Add("				{if: prop.name == u'" + slova + "' and prop.value}" + "<br><b>{prop.name}:</b> {prop.value}{end:}" + Environment.NewLine);
// //                      EndText.Add("<br>" + Environment.NewLine);
//                 #region htmLwriter
//                 var result = "<html>" + Environment.NewLine
//                     + "" + Environment.NewLine
//                     + "<head>" + Environment.NewLine
//                     + "	{setPageSize('A4')}" + Environment.NewLine
//                     + "	{setOrientation('P')}" + Environment.NewLine
//                     + "	{: setLeftMargin(15)}" + Environment.NewLine
//                     + "	{: setTopMargin(10)}" + Environment.NewLine
//                     + "	{: setBottomMargin(10)}" + Environment.NewLine
//                     + "	{: setRightMargin(10)}" + Environment.NewLine
//                     + "</head>" + Environment.NewLine
//                     + "" + Environment.NewLine
//                     + "<body>" + Environment.NewLine
//                     + "	{if: action.person.shortName == ''}" + Environment.NewLine
//                     + "	<div class=\"check\">" + Environment.NewLine
//                     + "		<table width=\"100%\" border=\"0\">" + Environment.NewLine
//                     + "			<tr>" + Environment.NewLine
//                     + "				<td align=\"center\">" + Environment.NewLine
//                     + "					<h1><b>Документ не может быть распечатан.</b></h1>" + Environment.NewLine
//                     + "				</td>" + Environment.NewLine
//                     + "			</tr>" + Environment.NewLine
//                     + "			<tr>" + Environment.NewLine
//                     + "				<td align=\"center\">" + Environment.NewLine
//                     + "					<h1><b>Документ не подписан.</b></h1>" + Environment.NewLine
//                     + "				</td>" + Environment.NewLine
//                     + "			</tr>" + Environment.NewLine
//                     + "			<tr>" + Environment.NewLine
//                     + "				<td align=\"center\">" + Environment.NewLine
//                     + "					<table border=\"0\">" + Environment.NewLine
//                     + "						<tr>" + Environment.NewLine
//                     + "							<td align=\"center\" style=\"vertical-align: top;\">" + Environment.NewLine
//                     + "								<h1><b>Нажмите </b></h1>" + Environment.NewLine
//                     + "							</td>" + Environment.NewLine
//                     + "							<td align=\"center\" style=\"vertical-align: top;\">" + Environment.NewLine
//                     + "								<img" + Environment.NewLine
//                     + "									src=\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACEAAAAhCAYAAABX5MJvAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAR9SURBVFhHvVjbTxxVGP+duezO7JWFvZBKSA2BAqUaTTWpxr4Y4z/gKzya+OQbj/wDBIhPJjyQGImpqYloeDTxHpNGo6kNLYY04kMtUKAt7G12d8bvOzuzTGF2wC3wI98O5+yZ8/3OdzsfCMdxwFhcXHRKpRKEEHJ8VmB9LLFYDOPj41KZJPHRx/NOJJGEomq8jOfPGAJ2ow5rfw8ffvC+EAsLC05FN6FFDXfB+aFerUDsP4aYnJx0jPwFqFrE/er80KhbqG79CzE/P+/Y8TQU/fxJ2DUL1vbGyUlwwPYV8nh19BLSyYQ7exSPnzzFrdt38GDzEYSqhgY6k6jtbEJxx8ei0NON118aRZ6e0UikrRRyWbw2NoquuEnmrslMOA4nIpFKxHH96ivoTqfdmXD0ksXeeesNGKpCROrubHuIqakpR8/kodApgqCRSa+OjWBscMCdoaimjauWxUnvzhDI7GwJTeM0b+LWb7/j1zsr0AyT0l91Zw9g0x7V7YcQ09PTjpLuaRsTcdPAe+++jYiuy/HTvT189/0P+PPuqhwLOi1z0TUVl4eHcf3Na0gmEtINRSp+n3x+E0I3oEejkqgfXmASQRWKorQX+t6gDbzxvdVV3F65h2g8jlQ2h3Q2j65cHvFMD/5+uIn76//IdQx+z67VYdsNCG+/IOHolT/8DBK53QE4+iOmiVgyDTOZgkmV1pNoPIGyVXNX0kltGw4RcGxH7nN07+bzxNnhgS2nUnlXyT18Cj/kqWTpdwmQS5pRE54hgST4Fe81XWvGgof+vj769K94Fr25ngMCJLobS2E0xNzcnCOS3YHZce3KKC70FpBJp9yZZmCWyhVpEbKlO3sAk+LAiEZQq9XQaDSwsbWF1bX7WN94BJV0+K3H2WHtUGC640Bc7O97hgAjlUyiN59DjopWrjtzREwjKpWzcCp3pVK4PDSI4t4TWbyCEEpCD8jtMHiKWfxE2C1VStd2hSuUxHHY2d3FX2tr2NnZbSn1S4sMZQjHSbvA6JiERf789LMbuPHFl7j51dcol8stxZ5yVuwRCgvmjklw2eZCFqP7xEx1oVK1pHK/Yo+ILUm0R8ckYlSwhoeGEIkYGLjYLzPCr/gwkTB0RMLbPB4z4TRqMHSt5QK/OzwC/AzD/ybhKeKnRSWa6wHHh3/ek5ZFTtMSfkWsfHDgRbw8OoIXCoXW/GEiLKfmDk+BJzyOUnW8MnKJrvmmO4IIsDxXYO6XyrLQ8OZ8chZPmSeHgzBInssS3/z8C7apIBWLRVdKslHxxiUiyX+17cvfS7JW+KVSqVDqVvHtjz/Jjot7iiCEXmA2nbRcpAuLLq06WSH8LmwPjW7iGPUeBvUcqq/98y4wMTs76ygpau+CekxyhTQpEWiatDMSikI9CF3ph2/eVo85MzPjqN3U6LrNyHmCLV3degBFdscNR7JVBPd75ySkD9T2sXXE8vKy88fKXUSTGXkXsAvOHOQSTtvq3i76qWmS/xpYWlpy1tfXUaVIbq452jGdFvxtXzabxcTEhPgPn//iC+G7+moAAAAASUVORK5CYII=\">" + Environment.NewLine
//                     + "							</td>" + Environment.NewLine
//                     + "						</tr>" + Environment.NewLine
//                     + "					</table>" + Environment.NewLine
//                     + "				</td>" + Environment.NewLine
//                     + "			</tr>" + Environment.NewLine
//                     + "		</table>" + Environment.NewLine
//                     + "	</div>" + Environment.NewLine
//                     + "	{else:}" + Environment.NewLine
//                     + "	<div class=\"DOC\">" + Environment.NewLine
//                     + "		<div class=\"Title\">" + Environment.NewLine
//                     + "			<table width=\"100%\" border=\"0\">" + Environment.NewLine
//                     + "				<tr>" + Environment.NewLine
//                     + "					<td width=\"150\"></td>" + Environment.NewLine
//                     + "					<td align=\"center\">" + Environment.NewLine
//                     + "						<h2>{action.title}</h2>" + Environment.NewLine
//                     + "					</td>" + Environment.NewLine
//                     + "					<td width=\"150\"></td>" + Environment.NewLine
//                     + "				</tr>" + Environment.NewLine
//                     + "			</table>" + Environment.NewLine
//                     + "		</div>" + Environment.NewLine
//                     + "		<br>" + Environment.NewLine
//                     + "		<div class=\"Client\">" + Environment.NewLine
//                     + "			<table width=\"100%\" border=\"0\">" + Environment.NewLine
//                     + "				<tr>" + Environment.NewLine
//                     + "					<td>" + Environment.NewLine
//                     + "						<b>ФИО:</b> {client.fullName}, {client.birthDate}({client.age})" + Environment.NewLine
//                     + "						<br><b>Номер и/б:</b> {event.externalId}" + Environment.NewLine
//                     + "						<br><b>Дата выполнения:</b> {action.endDate}" + Environment.NewLine
//                     + "					</td>" + Environment.NewLine
//                     + "				</tr>" + Environment.NewLine
//                     + "			</table>" + Environment.NewLine
//                     + "		</div>" + Environment.NewLine
//                     + "		<div class=\"ActionProperty\">" + Environment.NewLine
//                     + "			{for: prop in action}" + Environment.NewLine;
//                 foreach (var item in result)
//                 {
//                     Thread.Sleep(Random(Ran));
//                     HtmLwriter(item);
//                 }
//                 #endregion                
//                 foreach (var item in EndText.SelectMany(s => s))
//                 {
//                     Thread.Sleep(Random(Ran));
//                     HtmLwriter(item);
//                 }
//
//                 #region htmLwriter2
//                 var return1 = "			{end:}" + Environment.NewLine
//                                           + "		</div>" + Environment.NewLine
//                                           + "	</div>" + Environment.NewLine
//                                           + "	<br>" + Environment.NewLine
//                                           + "	<div class=\"signature\">" + Environment.NewLine
//                                           + "		<table width=\"100%\" border=\"0\">" + Environment.NewLine
//                                           + "			<tr>" + Environment.NewLine
//                                           + "				<td><b>Врач {action.person.speciality}:</b> </td>" + Environment.NewLine
//                                           + "				<td align=\"right\">{action.person.shortName}</td>" + Environment.NewLine
//                                           + "			</tr>" + Environment.NewLine
//                                           + "		</table>" + Environment.NewLine
//                                           + "	</div>" + Environment.NewLine
//                                           + "	{end:}" + Environment.NewLine
//                                           + "</body>" + Environment.NewLine
//                                           + "" + Environment.NewLine
//                                           + "</html>" + Environment.NewLine;
//                 foreach (var item in return1)
//                 {
//                     Thread.Sleep(Random(Ran));
//                     HtmLwriter(item);
//                 }
//                 #endregion
//                 Console.WriteLine(Environment.ExpandEnvironmentVariables(@"C:\Users\%USERNAME%\AppData\Local\Programs\Microsoft VS Code\Code.exe"));
//                 Process.Start(Environment.ExpandEnvironmentVariables(@"C:\Users\%username%\AppData\Local\Programs\Microsoft VS Code\Code.exe"),
//                     $@"C:\VISTA_MED\lustik_ak\templates\{docContext}.html");
//                 eventclass.Select(MethodBase.GetCurrentMethod()?.Name);
//                 Console.Write("Сохранить шаблон?");
//                 if (Console.ReadLine() == "y" || Console.ReadLine() == "Y") Write(docContext, connect);
                eventclass.Select(MethodBase.GetCurrentMethod()?.ReflectedType?.Name);
            }
            catch (Exception e)
            {
                eventclass.Error(MethodBase.GetCurrentMethod()?.ReflectedType?.Name, e);
                Console.ReadKey();
            }
        }
    }
}