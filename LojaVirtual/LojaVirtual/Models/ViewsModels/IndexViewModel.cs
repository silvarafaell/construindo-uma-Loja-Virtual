using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace LojaVirtual.Models.ViewsModels
{
    public class IndexViewModel
    {
        public NewsletterEmail newsletter { get; set; }
        public IPagedList<Produto> lista { get; set; }
    }
}
