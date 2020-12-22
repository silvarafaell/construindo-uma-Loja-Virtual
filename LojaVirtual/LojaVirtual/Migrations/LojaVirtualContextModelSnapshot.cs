﻿// <auto-generated />
using System;
using LojaVirtual.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LojaVirtual.Migrations
{
    [DbContext(typeof(LojaVirtualContext))]
    partial class LojaVirtualContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LojaVirtual.Models.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CategoriaPaiId");

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.Property<string>("Slug")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CategoriaPaiId");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("LojaVirtual.Models.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CPF")
                        .IsRequired();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<DateTime>("Nascimento");

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.Property<string>("Senha")
                        .IsRequired();

                    b.Property<string>("Sexo")
                        .IsRequired();

                    b.Property<string>("Situacao");

                    b.Property<string>("Telefone")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("LojaVirtual.Models.Colaborador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.Property<string>("Senha")
                        .IsRequired();

                    b.Property<string>("Tipo");

                    b.HasKey("Id");

                    b.ToTable("Colaboradores");
                });

            modelBuilder.Entity("LojaVirtual.Models.Imagem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Caminho");

                    b.Property<int>("ProdutoId");

                    b.HasKey("Id");

                    b.HasIndex("ProdutoId");

                    b.ToTable("Imagens");
                });

            modelBuilder.Entity("LojaVirtual.Models.NewsletterEmail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("NewsletterEmails");
                });

            modelBuilder.Entity("LojaVirtual.Models.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Altura");

                    b.Property<int>("CategoriaId");

                    b.Property<int>("Comprimento");

                    b.Property<string>("Descricao");

                    b.Property<int>("Largura");

                    b.Property<string>("Nome");

                    b.Property<double>("Peso");

                    b.Property<int>("Quantidade");

                    b.Property<decimal>("Valor");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("LojaVirtual.Models.Categoria", b =>
                {
                    b.HasOne("LojaVirtual.Models.Categoria", "categoriaPai")
                        .WithMany()
                        .HasForeignKey("CategoriaPaiId");
                });

            modelBuilder.Entity("LojaVirtual.Models.Imagem", b =>
                {
                    b.HasOne("LojaVirtual.Models.Produto", "Produto")
                        .WithMany("Imagens")
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LojaVirtual.Models.Produto", b =>
                {
                    b.HasOne("LojaVirtual.Models.Categoria", "categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
