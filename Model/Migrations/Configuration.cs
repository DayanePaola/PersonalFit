namespace Model.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using static Util.Validation;

    internal sealed class Configuration : DbMigrationsConfiguration<Model.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Model.Context context)
        {
            context.Usuario.Add(new UsuarioModel()
            {
                Login = "admin",
                Senha = GeraMD5.GeraHash("admin")
            });
            context.SaveChanges();
        }
    }
}