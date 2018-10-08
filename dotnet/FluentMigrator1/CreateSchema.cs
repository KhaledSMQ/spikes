using FluentMigrator;

namespace FluentMigrator1
{
	[Migration(1)]
	public class CreateSchema : Migration 
	{
		public override void Up()
		{
			Create.Schema("FluentMigrator2");
		}

		public override void Down()
		{
			Delete.Schema("FluentMigrator2");
		}
	}
}
