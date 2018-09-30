using System.Linq;
using System.Threading.Tasks;
using Dados;
using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Mvc.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ApplicationDbContext _contexto;

        public CategoriaController(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            var categorias = _contexto.Categorias.ToList();
            
            return View(categorias);
        }

        public IActionResult Editar(int id)
        {
            var categoria = _contexto.Categorias.First(c => c.Id == id);
            
            return View("Salvar", categoria);
        }

        public async Task<IActionResult> Deletar(int id)
        {
            var categoria = _contexto.Categorias.First(c => c.Id == id);
            _contexto.Categorias.Remove(categoria);
            await _contexto.SaveChangesAsync();
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Salvar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Salvar(Categoria modelo)
        {
            if(modelo.Id == 0)
                _contexto.Categorias.Add(modelo);
            else{
                var categoriaJaSalva = _contexto.Categorias.First(c => c.Id == modelo.Id);
                categoriaJaSalva.Nome = modelo.Nome;
                categoriaJaSalva.PermiteEstoque = modelo.PermiteEstoque;
            }
            await _contexto.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}