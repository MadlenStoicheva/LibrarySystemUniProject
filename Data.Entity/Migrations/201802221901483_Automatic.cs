namespace Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Automatic : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        numberISBN = c.String(),
                        title = c.String(),
                        author = c.String(),
                        publishingHouse = c.String(),
                        releaseDate = c.DateTime(nullable: false),
                        availability = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Readers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        firstName = c.String(),
                        lastName = c.String(),
                        readerCard = c.String(),
                        expireDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TakeABooks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        ReaderId = c.Int(nullable: false),
                        dateTaken = c.DateTime(nullable: false),
                        dateForReturn = c.DateTime(nullable: false),
                        dateReturn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Readers", t => t.ReaderId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.ReaderId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        username = c.String(),
                        password = c.String(),
                        firstName = c.String(),
                        lastName = c.String(),
                        isAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TakeABooks", "ReaderId", "dbo.Readers");
            DropForeignKey("dbo.TakeABooks", "BookId", "dbo.Books");
            DropIndex("dbo.TakeABooks", new[] { "ReaderId" });
            DropIndex("dbo.TakeABooks", new[] { "BookId" });
            DropTable("dbo.Users");
            DropTable("dbo.TakeABooks");
            DropTable("dbo.Readers");
            DropTable("dbo.Books");
        }
    }
}
