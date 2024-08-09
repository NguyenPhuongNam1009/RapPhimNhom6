namespace Nhom6_DoAn_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.IdentityUserClaim", newName: "AspNetUserClaims");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.AspNetUserClaims", newName: "IdentityUserClaim");
        }
    }
}
