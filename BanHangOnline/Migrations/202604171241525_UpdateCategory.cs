namespace BanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCategory : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tb_Category", "Title", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.tb_Category", "seoTitle", c => c.String(maxLength: 250));
            AlterColumn("dbo.tb_Category", "seoDescription", c => c.String(maxLength: 250));
            AlterColumn("dbo.tb_Category", "seoKeywords", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tb_Category", "seoKeywords", c => c.String());
            AlterColumn("dbo.tb_Category", "seoDescription", c => c.String());
            AlterColumn("dbo.tb_Category", "seoTitle", c => c.String());
            AlterColumn("dbo.tb_Category", "Title", c => c.String());
        }
    }
}
