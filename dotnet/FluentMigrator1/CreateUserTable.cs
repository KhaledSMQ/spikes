using FluentMigrator;

namespace FluentMigrator1
{
	[Migration(2)]
	public class CreateUserTable : Migration 
	{
		public override void Up()
		{
			Create.Table("Users").WithColumn("Id").AsInt64().Identity().PrimaryKey()
				 .WithColumn("Firstname").AsAnsiString(40).Nullable()
				 .WithColumn("Lastname").AsAnsiString(60).Nullable()
				 .WithColumn("Email").AsAnsiString(128).NotNullable();
		}

		public override void Down()
		{
			Delete.Table("Users");
		}
	}
}
