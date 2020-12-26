using LojaVirtual.Libraries.Arquivo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Areas.Colaborador.Controllers
{
    public class ImagemController : Controller
    {
        [Area("Colaborador")]
        public IActionResult Armazenar(IFormFile file)
        {
            GerenciadorArquivo.CadastrarImagemProduto(file);
            return View();
        }

        public IActionResult Deletar()
        {
            return View();
        }
    }
}
