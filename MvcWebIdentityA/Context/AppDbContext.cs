using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MvcWebIdentityA.Entities;

namespace MvcWebIdentityA.Context
{
    public class AppDbContext : IdentityDbContext
    { 
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Produto> Produtos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Aluno>().HasData(
                new Aluno
                {
                    AlunoId = 1,
                    Nome = "Douglas",
                    Email = "ddouglss1999@gmail.com",
                    Idade = 25,
                    Curso = "Engenharia"
                });
        }

        public void AddProduto(Produto produto)
        {
            Produtos.Add(produto);
        }

    }
}
