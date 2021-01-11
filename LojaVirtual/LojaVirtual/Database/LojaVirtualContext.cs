using LojaVirtual.Models;
using LojaVirtual.Models.ProdutoAgregador;
using Microsoft.EntityFrameworkCore; //referente ao banco
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Database
{
    public class LojaVirtualContext : DbContext
    {
        /* EF - Core
         * SQL >
         * ORM > Biblioteca mapear objetos para Banco de Dados relacionais
         */
        
        //Construtor
        public LojaVirtualContext(DbContextOptions<LojaVirtualContext> options) : base(options)
        {

        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<NewsletterEmail> NewsletterEmails { get; set; }
        public DbSet<Colaborador> Colaboradores { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Imagem> Imagens { get; set; }
    }
}
