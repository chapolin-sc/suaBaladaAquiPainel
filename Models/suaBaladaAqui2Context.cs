using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using suaBaladaAqui2.Models;

namespace suaBaladaAqui2.Models
{
    public partial class suaBaladaAqui2Context : DbContext
    {
        public suaBaladaAqui2Context()
        {
        }

        public suaBaladaAqui2Context(DbContextOptions<suaBaladaAqui2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Efmigrationshistory> Efmigrationshistories { get; set; } = null!;
        public virtual DbSet<EventosModel> eventos { get; set; } = null!;
        public virtual DbSet<UsuariosModel> usuarios { get; set; } = null!;
        public virtual DbSet<CarouselModel> carousel { get; set; } = null!;
        public virtual DbSet<BookModel> book { get; set; } = null!;
        public virtual DbSet<FotosModel> fotos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=paineladm.cnljv0sbllba.us-east-1.rds.amazonaws.com;database=paineladministrativo2;uid=chapolinAdm;pwd=27bJT6KBLxAfSR8WycZ4", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.23-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Efmigrationshistory>(entity =>
            {
                entity.HasKey(e => e.MigrationId)
                    .HasName("PRIMARY");

                entity.ToTable("__efmigrationshistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ProductVersion).HasMaxLength(32);
            });

            modelBuilder.Entity<EventosModel>(entity =>
            {
                entity.ToTable("eventos");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Atracoes).HasColumnName("atracoes");

                entity.Property(e => e.Bairro).HasColumnName("bairro");

                entity.Property(e => e.Cidade).HasColumnName("cidade");

                entity.Property(e => e.DataEvento)
                    .HasMaxLength(6)
                    .HasColumnName("dataEvento");

                entity.Property(e => e.Endereco).HasColumnName("endereco");

                entity.Property(e => e.Evento1).HasColumnName("evento");

                entity.Property(e => e.FoneContato).HasColumnName("foneContato");

                entity.Property(e => e.Imagem).HasColumnName("imagem");

                entity.Property(e => e.LocalName).HasColumnName("localName");

                entity.Property(e => e.Organizador).HasColumnName("organizador");

                entity.Property(e => e.Responsavel).HasColumnName("responsavel");
            });

            modelBuilder.Entity<UsuariosModel>(entity =>
            {
                entity.ToTable("usuarios");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DataCadastro).HasMaxLength(6);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
