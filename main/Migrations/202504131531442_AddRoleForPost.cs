namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRoleForPost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Role_Value", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "Role_Value");
        }
    }
}
