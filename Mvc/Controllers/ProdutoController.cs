using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dados;
using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mvc.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ApplicationDbContext _contexto;

        public ProdutoController(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        public IActionResult Index()
        {
            var queryDeProduto = _contexto.Produtos
                .Where(p => p.Ativo && p.Categoria.PermiteEstoque)
                .OrderBy(p => p.Nome);

            if(!queryDeProduto.Any())
                return View(new List<Produto>());
            
            return View(queryDeProduto.ToList());
        }
        
        [HttpGet]
        public IActionResult Salvar()
        {
            ViewBag.Categorias = _contexto.Categorias.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Salvar(Produto modelo)
        {
            _contexto.Produtos.Add(modelo);
            await _contexto.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}