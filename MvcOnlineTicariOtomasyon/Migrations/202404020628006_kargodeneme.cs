namespace MvcOnlineTicariOtomasyon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kargodeneme : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.KargoDetays",
                c => new
                    {
                        KargoDetayid = c.Int(nullable: false, identity: true),
                        Aciklama = c.String(),
                        TakipKodu = c.String(),
                        Personel = c.String(),
                        Alici = c.String(),
                        Tarih = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.KargoDetayid);
            
            CreateTable(
                "dbo.KargoTakips",
                c => new
                    {
                        KargoTakipid = c.Int(nullable: false, identity: true),
                        TakipKodu = c.String(),
                        Aciklama = c.String(),
                        TarihZaman = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.KargoTakipid);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.KargoTakips");
            DropTable("dbo.KargoDetays");
        }
    }
}
