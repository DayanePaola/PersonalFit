namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterTableProfessor : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PROFESSOR", "Cpf", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PROFESSOR", "Cpf", c => c.String(nullable: false, maxLength: 11));
        }
    }
}
