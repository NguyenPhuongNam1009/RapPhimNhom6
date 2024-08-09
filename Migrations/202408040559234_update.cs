namespace Nhom6_DoAn_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_Users", "FullName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tbl_Users", "FullName");
        }
    }
}
