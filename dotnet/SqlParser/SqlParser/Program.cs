using System;
using System.Collections.Generic;
using Microsoft.SqlServer.Management.SqlParser.Parser;
using Microsoft.SqlServer.Management.SqlParser.SqlCodeDom;

namespace SqlParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Program();
            p.Run();
            Console.WriteLine("Press Enter to quit.");
            Console.ReadLine();
        }

        private void Run()
        {
            var sqls = new[] { TSql1, TSql2, PlSql1, PgSql1 };

            foreach (var sql in sqls)
            {
                var rst = Parser.Parse(sql);
                var script = rst.Script;
                //var xml = script.Xml;
                //var sql = script.Sql;
                Process(script.Children, string.Empty);
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        private void Process(IEnumerable<SqlCodeObject> sqlCodeObjects, string indent)
        {
            foreach(var sqlCodeObject in sqlCodeObjects)
                Process(sqlCodeObject, indent);
        }

        private void Process(SqlCodeObject sqlCodeObject, string indent)
        {
            Console.WriteLine($"{indent}{sqlCodeObject.GetType().Name} = '{sqlCodeObject.Sql}'");
            Process(sqlCodeObject.Children, indent + "  ");
        }

        private const string TSql1 = "select * from someTable where Id > 10";
        private const string TSql2 = @"USE [DbTest]
GO

if object_id('tempdb..#datac', 'U') IS NOT NULL
	drop table #datac;

create table #datac(
	Category nvarchar(100),
	Name nvarchar(100),
	Type nvarchar(100),
	Provider nvarchar(100),
	ObjType nvarchar(100),
	ObjTypeId int,
	IsFlagged char(1)
)

declare	@list [DbTest].[ListTableType]

insert into #datac
exec [DbTest].[GetData]
		@List = @list,
		@IsFlagged = NULL


select * from #datac

drop table #datac

go
";
        private const string PlSql1 = @"SELECT TO_DATE('2015/05/15 8:30:25', 'YYYY/MM/DD HH:MI:SS')
            FROM dual;";

        private const string PgSql1 = @"CREATE OR REPLACE FUNCTION fnsomefunc(numtimes integer, msg text)
    RETURNS text AS
$$
DECLARE
    strresult text;
BEGIN
    strresult := '';
    IF numtimes > 0 THEN
        FOR i IN 1 .. numtimes LOOP
            strresult := strresult || msg || E'\r\n';
        END LOOP;
    END IF;
    RETURN strresult;
END;
$$
LANGUAGE 'plpgsql' IMMUTABLE
SECURITY DEFINER
  COST 10;

--To call the function we do this and it returns ten hello there's with 
carriage returns as a single text field.
SELECT fnsomefunc(10, 'Hello there');";
    }
}
