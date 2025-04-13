namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostRoleIsOptional : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "Role_Value", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "Role_Value", c => c.Int(nullable: false));
        }
    }
}
