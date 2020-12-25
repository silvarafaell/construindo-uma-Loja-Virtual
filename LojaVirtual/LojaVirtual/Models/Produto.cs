﻿using LojaVirtual.Libraries.Lang;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Models
{
    public class Produto
    {
        //PK
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")] //deixa o campo obrigatorio
        public string Nome { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")] //deixa o campo obrigatorio
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")] //deixa o campo obrigatorio
        [Display(Name = "Preço")]
        public decimal Valor { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")] //deixa o campo obrigatorio
        [Range(0, 10000, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E006")]
        public int Quantidade { get; set; }

        //Frete - Correios
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")] //deixa o campo obrigatorio
        [Range(0.001, 30, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E006")]
        public double Peso { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")] //deixa o campo obrigatorio
        [Range(11, 105, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E006")]
        public int Largura { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")] //deixa o campo obrigatorio
        [Range(2, 105, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E006")]
        public int Altura { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")] //deixa o campo obrigatorio
        [Range(16, 105, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E006")]
        public int Comprimento { get; set; }

        //EF - ORM - Biblioteca Unir - Banco de dados e POO.(ORM - Mapeamento de Objetos <-> Relacionamento)
        //Fluente API - Attributes

        //Banco de dados - Relacionamento entre Tabela
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")] //deixa o campo obrigatorio
        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }

        //POO - Associações entre Objetos
        [ForeignKey("CategoriaId")]
        public virtual Categoria Categoria { get; set; }

        public virtual ICollection<Imagem> Imagens { get; set; }
    }
}
