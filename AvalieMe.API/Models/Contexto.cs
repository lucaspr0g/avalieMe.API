using System.Data.Entity;

namespace AvalieMe.API.Models
{
    public partial class Contexto : DbContext
    {
        public Contexto()
            : base("name=Contexto")
        {
            Database.SetInitializer<Contexto>(null);
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
            this.Database.CommandTimeout = 300;
        }

        public virtual DbSet<usuario> usuario { get; set; }
        public virtual DbSet<teste> teste { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<usuario>()
                .Property(e => e.nome)
                .IsUnicode(false);

            modelBuilder.Entity<usuario>()
                .Property(e => e.senha)
                .IsUnicode(false);

            modelBuilder.Entity<usuario>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<teste>()
                .Property(e => e.nomeImagem)
                .IsUnicode(false);

            modelBuilder.Entity<teste>()
                .Property(e => e.categoria)
                .IsUnicode(false);

            modelBuilder.Entity<teste>()
                .Property(e => e.posicaoPessoa)
                .IsUnicode(false);
        }
    }
}
