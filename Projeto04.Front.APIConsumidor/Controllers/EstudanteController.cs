using Microsoft.AspNetCore.Mvc;
using Projeto04.Front.APIConsumidor.Models;
using Projeto04.Front.APIConsumidor.Services;

namespace Projeto04.Front.APIConsumidor.Controllers
{
    public class EstudanteController : Controller
    {
        // definir o elemento referencial para a DI(Injeção de Dependência)
        // para as operações do Controller. 

        private EstudanteService _estudanteService;

        // definir o construtor
        public EstudanteController(EstudanteService estudanteService)
        {
            _estudanteService = estudanteService;
        }

        // 1ª tarefa assíncrona: Leitura e exibição dos dados, posteriormente,
        // na view. 

        public async Task<IActionResult> Index()
        {
            try
            {
                var listaEstudantes = await 
                    _estudanteService.GetEstudantesAsync();
                return View(listaEstudantes);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Não foi possível acessar os" +
                    "dados de estudantes. Tente novamente.");
                return View(new List<Estudante>());
            }
        }

        // 2ª tarefa CRUD: Inserção - Create 
        public ViewResult GetEstudanteUnico() => View();

        // praticar a sobrecarga para selecionar o registro
        [HttpPost]
        public async Task<IActionResult> GetEstudanteUnico(int id)
        {
            // definir a requisição para a seleção do registro
            var estudante = await _estudanteService.GetEstudanteByIdAsync(id);

            // verificar o valor da variável 
            if(estudante == null)
            {
                return NotFound();
            }
            return View(estudante);
        }

        // 3ª tarefa CRUD: Inserção - Create()

        public ViewResult AddEstudante() => View();

        [HttpPost]

        public async Task<IActionResult> AddEstudante(Estudante estudante)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _estudanteService.AddEstudanteAsync
                        (estudante);
                    return RedirectToAction(nameof(Index));
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Erro ao criar registro do Estudante");
                }
            }

            return View(estudante);
        }
    }
}
