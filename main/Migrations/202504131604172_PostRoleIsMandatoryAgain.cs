namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostRoleIsMandatoryAgain : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "Role_Value", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "Role_Value", c => c.Int());
        }
    }
}
