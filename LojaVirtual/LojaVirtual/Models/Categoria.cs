﻿using LojaVirtual.Libraries.Lang;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Models
{
    public class Categoria
    {
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")] //deixa o campo obrigatorio
        [MinLength(4, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E002")] //quantidade minima de caracteres
        //TODO - Criar Validação - Nome Categoria unico no Banco de dados.
        public string Nome { get; set; }

        /*
         * Nome : Telefone sem fio
         * Slug: telefone-sem-fio
         * URL normal: www.lojavirutal.com.br/categoria/5
         * URL Amigável e com Slug: www.lojavirtual.com.br/categoria/informatica (Url amigável)
         */

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")] //deixa o campo obrigatorio
        [MinLength(4, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E002")] //quantidade minima de caracteres
        public string Slug { get; set; }

        /*
         * Auto-relacionamento
         * 1-Informatica - P:null
         * - 2-Mouse: P:1
         * --3-Mouse: P:1
         * --4-Mouse Gamer P:2
         */
         //para permitir Nulo o ?
         [Display(Name = "Categoria Pai")]
         public int? CategoriaPaiId { get; set; }

        /*
         * ORM - EntityFrameworkCore
         */

        [ForeignKey("CategoriaPaiId")]
        public virtual Categoria categoriaPai { get; set; }
    }
}
