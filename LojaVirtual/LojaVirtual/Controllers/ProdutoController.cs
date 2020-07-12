using LojaVirtual.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Controllers
{
    public class ProdutoController : Controller  //todo controlador erda da classe controller
    {
        /*
         * Todo metodo tem que resultar 
         * ActionResult ou
         * IActionResult
        */

        public ActionResult Visualizar()
        {
            Produto produto = GetProduto();

            return View(produto);  //passar o produto para a tela
           //return new ContentResult() { Content = "<h3>Produto Visualizar<h3>", ContentType = "text/html" };
            
        }

        private Produto GetProduto()
        {
            return new Produto()
            {
                Id = 1,
                Nome = "Notebook para Programar",
                Descricao = "Programar mais",
                Valor = 4000.00M

            };

        }

    }
}
