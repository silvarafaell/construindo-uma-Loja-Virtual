using LojaVirtual.Models;
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
    }
}
