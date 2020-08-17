using Marlin_Desafio_Backend_Junior.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marlin_Desafio_Backend_Junior.Db
{
    public partial class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder mb) 
        {
            mb.Entity<Usuario>(entity =>
            {
                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Senha)
                    .IsRequired()
                    .HasMaxLength(25);
                
            });

            mb.Entity<Turma>(entity=>
            {

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(25);
                
            });

            mb.Entity<Aluno>(entity =>
            {
                entity.Property(e => e.Matricula)
                    .IsRequired()
                    .HasMaxLength(45);
                entity.HasIndex(e => e.Matricula)
                    .IsUnique();

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(45);
                entity.HasIndex(e => e.Nome)
                    .IsUnique();

                entity.HasOne(e => e.Turma)
                        .WithMany(e => e.Alunos)
                        .HasForeignKey(e => e.idTurma);            

                    
            });

            mb.Entity<RegistroAlunoTurma>(entity =>
            {
                entity.Property(e => e.Data).IsRequired();
                entity.Property(e => e.Matricula).IsRequired();
                entity.Property(e => e.Staus).IsRequired();
                entity.Property(e => e.TurmaId).IsRequired();

            });

            base.OnModelCreating(mb);
        }

        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Aluno> Alunos { get; set; }
        public virtual DbSet<Turma> Turmas { get; set; }
        public virtual DbSet<RegistroAlunoTurma> Registros { get; set; }
    }
}
