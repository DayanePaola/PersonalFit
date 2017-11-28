namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterTableTreino : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TREINO", "Titulo", c => c.String(nullable: false));
            AlterColumn("dbo.ALUNO", "Cpf", c => c.String(nullable: false));
            AlterColumn("dbo.ALUNO", "Objetivo", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ALUNO", "Objetivo", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.ALUNO", "Cpf", c => c.String(nullable: false, maxLength: 11));
            DropColumn("dbo.TREINO", "Titulo");
        }
    }
}
