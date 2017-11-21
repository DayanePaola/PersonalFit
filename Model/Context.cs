using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Context : DbContext
    {
        public Context() : base("name=dbconnection") { }

        public DbSet<AlunoModel> Aluno { get; set; }
        public DbSet<ExercicioModel> Exercicio { get; set; }
        public DbSet<ProfessorModel> Professor { get; set; }
        public DbSet<TreinoModel> Treino { get; set; }
        public DbSet<TreinoExercicioModel> TreinoExercicio { get; set; }
        public DbSet<UsuarioModel> Usuario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
