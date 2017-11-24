namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterTableUsuario : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.USUARIO", "Senha", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.USUARIO", "Senha", c => c.String(nullable: false, maxLength: 30));
        }
    }
}
