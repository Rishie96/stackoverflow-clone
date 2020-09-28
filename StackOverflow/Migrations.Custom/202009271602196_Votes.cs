namespace StackOverflow.Migrations.Custom
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Votes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnswerVotes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.String(),
                        AnswerID = c.Int(nullable: false),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.QuestionVotes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.String(),
                        PostID = c.Int(nullable: false),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.QuestionVotes");
            DropTable("dbo.AnswerVotes");
        }
    }
}
