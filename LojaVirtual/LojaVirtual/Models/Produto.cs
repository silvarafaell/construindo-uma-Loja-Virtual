﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Models
{
    public class Produto
    {
        //PK
        public int Id { get; set; }

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public int Quantidade { get; set; }

        //Frete - Correios
        public double Peso { get; set; }
        public int Largura { get; set; }
        public int Altura { get; set; }
        public int Comprimento { get; set; }

        //EF - ORM - Biblioteca Unir - Banco de dados e POO.(ORM - Mapeamento de Objetos <-> Relacionamento)
        //Fluente API - Attributes

        //Banco de dados - Relacionamento entre Tabela
        public int CategoriaId { get; set; }

        //POO - Associações entre Objetos
        [ForeignKey("CategoriaId")]
        public Categoria categoria { get; set; }

        public ICollection<Imagem> Imagens { get; set; }
    }
}
