namespace ThiThu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbInit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HocPhans",
                c => new
                    {
                        MaHP = c.String(nullable: false, maxLength: 128),
                        TenHP = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.MaHP);
            
            CreateTable(
                "dbo.HP_SV",
                c => new
                    {
                        MaHP = c.String(nullable: false, maxLength: 128),
                        MaSV = c.String(nullable: false, maxLength: 128),
                        DiemBT = c.Double(),
                        DiemGK = c.Double(),
                        DiemCK = c.Double(),
                        NgayThi = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.MaHP, t.MaSV })
                .ForeignKey("dbo.HocPhans", t => t.MaHP, cascadeDelete: true)
                .ForeignKey("dbo.SinhViens", t => t.MaSV, cascadeDelete: true)
                .Index(t => t.MaHP)
                .Index(t => t.MaSV);
            
            CreateTable(
                "dbo.SinhViens",
                c => new
                    {
                        MaSV = c.String(nullable: false, maxLength: 128),
                        TenSV = c.String(maxLength: 50),
                        LopSh = c.String(),
                        GioiTinh = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MaSV);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HP_SV", "MaSV", "dbo.SinhViens");
            DropForeignKey("dbo.HP_SV", "MaHP", "dbo.HocPhans");
            DropIndex("dbo.HP_SV", new[] { "MaSV" });
            DropIndex("dbo.HP_SV", new[] { "MaHP" });
            DropTable("dbo.SinhViens");
            DropTable("dbo.HP_SV");
            DropTable("dbo.HocPhans");
        }
    }
}
