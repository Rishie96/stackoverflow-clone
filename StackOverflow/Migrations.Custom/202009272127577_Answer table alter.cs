namespace StackOverflow.Migrations.Custom
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Answertablealter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Answers", "Answer", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Answers", "Answer");
        }
    }
}
