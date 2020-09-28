namespace StackOverflow.Migrations.Custom
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCustom : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        AnswerID = c.Int(nullable: false, identity: true),
                        PostID = c.Int(nullable: false),
                        UserID = c.String(),
                        Date = c.DateTime(nullable: false),
                        Likes = c.Int(nullable: false),
                        Dislikes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AnswerID);
            
            CreateTable(
                "dbo.UserDetails",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Gender = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Country = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        PostID = c.Int(nullable: false, identity: true),
                        UserID = c.String(),
                        Date = c.DateTime(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        Category = c.String(),
                    })
                .PrimaryKey(t => t.PostID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Questions");
            DropTable("dbo.UserDetails");
            DropTable("dbo.Answers");
        }
    }
}
