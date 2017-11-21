namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ALUNO",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 15),
                        Cpf = c.String(nullable: false, maxLength: 11),
                        Idade = c.Int(nullable: false),
                        Peso = c.Double(nullable: false),
                        Altura = c.Double(nullable: false),
                        Objetivo = c.String(nullable: false, maxLength: 15),
                        UsuarioFK = c.Int(nullable: false),
                        ProfessorFK = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PROFESSOR", t => t.ProfessorFK)
                .ForeignKey("dbo.USUARIO", t => t.UsuarioFK)
                .Index(t => t.UsuarioFK)
                .Index(t => t.ProfessorFK);
            
            CreateTable(
                "dbo.PROFESSOR",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UsuarioFK = c.Int(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 30),
                        Cpf = c.String(nullable: false, maxLength: 11),
                        Cref = c.String(nullable: false, maxLength: 11),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.USUARIO", t => t.UsuarioFK)
                .Index(t => t.UsuarioFK);
            
            CreateTable(
                "dbo.USUARIO",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false, maxLength: 10),
                        Senha = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TREINO",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AlunoFK = c.Int(nullable: false),
                        Situacao = c.String(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ALUNO", t => t.AlunoFK)
                .Index(t => t.AlunoFK);
            
            CreateTable(
                "dbo.TREINOEXERCICIO",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TreinoFK = c.Int(nullable: false),
                        ExercicioFK = c.Int(nullable: false),
                        Repeticoes = c.Int(nullable: false),
                        Peso = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EXERCICIO", t => t.ExercicioFK)
                .ForeignKey("dbo.TREINO", t => t.TreinoFK)
                .Index(t => t.TreinoFK)
                .Index(t => t.ExercicioFK);
            
            CreateTable(
                "dbo.EXERCICIO",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 30),
                        Descricao = c.String(nullable: false, maxLength: 100),
                        Equipamento = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ALUNO", "UsuarioFK", "dbo.USUARIO");
            DropForeignKey("dbo.TREINOEXERCICIO", "TreinoFK", "dbo.TREINO");
            DropForeignKey("dbo.TREINOEXERCICIO", "ExercicioFK", "dbo.EXERCICIO");
            DropForeignKey("dbo.TREINO", "AlunoFK", "dbo.ALUNO");
            DropForeignKey("dbo.ALUNO", "ProfessorFK", "dbo.PROFESSOR");
            DropForeignKey("dbo.PROFESSOR", "UsuarioFK", "dbo.USUARIO");
            DropIndex("dbo.TREINOEXERCICIO", new[] { "ExercicioFK" });
            DropIndex("dbo.TREINOEXERCICIO", new[] { "TreinoFK" });
            DropIndex("dbo.TREINO", new[] { "AlunoFK" });
            DropIndex("dbo.PROFESSOR", new[] { "UsuarioFK" });
            DropIndex("dbo.ALUNO", new[] { "ProfessorFK" });
            DropIndex("dbo.ALUNO", new[] { "UsuarioFK" });
            DropTable("dbo.EXERCICIO");
            DropTable("dbo.TREINOEXERCICIO");
            DropTable("dbo.TREINO");
            DropTable("dbo.USUARIO");
            DropTable("dbo.PROFESSOR");
            DropTable("dbo.ALUNO");
        }
    }
}
