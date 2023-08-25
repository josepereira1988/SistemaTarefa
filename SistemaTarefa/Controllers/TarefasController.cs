using Dominio.Interface;
using Dominio.models;
using Microsoft.AspNetCore.Mvc;
using SistemaTarefa.models;
using System.Text.Json.Serialization;

namespace SistemaTarefa.Controllers
{
	[Controller]
    public class TarefasController : Controller
	{
		private readonly ITarefaService _service;

		public TarefasController(ITarefaService service)
		{
			_service = service;
		}

		public async Task<IActionResult> Index()
		{
			var resultado = await _service.GetAll();


			return View(resultado);
		}
		[HttpGet]
		public async Task<IActionResult> CriarTarefa()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> CriarTarefa(Tarefa tarefa)
		{
			try
			{
				if(await _service.Exist(tarefa.Nome) != null)
				{
					ModelState.AddModelError("Nome", "Nome já existe");
					return View(tarefa);
				}
				if (tarefa == null)
				{
					return BadRequest();
				}
				if (await _service.Add(tarefa))
				{
					return RedirectToAction("Index");
				}
				/// Mensagem de erro
				ModelState.AddModelError(string.Empty, "Erro ao adiniar tarefa");
				return View(tarefa);
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		[HttpGet]
		public async Task<IActionResult> AtualizarTarefa(int Id)
		{
			var result = await _service.GetById(Id);
			if (result == null)
			{
				return NotFound();
			}
			return View(result);
		}
		[HttpPost]
		public async Task<IActionResult> AtualizarTarefa([FromBody]Tarefa tarefa)
		{
			try
			{
				var result = await _service.Exist(tarefa.Nome);
				if (result == null)
				{
					if (tarefa == null)
					{
						return BadRequest();
					}
					if (await _service.Update(tarefa))
					{
						return RedirectToAction("Index");
					}
					/// Mensagem de erro
					ModelState.AddModelError("Erro", "Erro ao adiniar tarefa");
					return BadRequest(ModelState);
				}
				else
				{
					if (result.Id == tarefa.Id)
					{
						if (await _service.Update(tarefa))
						{
							return RedirectToAction("Index");
						}
						ModelState.AddModelError(string.Empty, "Erro ao adiniar tarefa");
						return BadRequest(ModelState);
					}
					ModelState.AddModelError("Nome", "Nome já existe");
					return BadRequest(ModelState);
				}
			}
			catch (Exception ex)			{

				throw ex;
			}
		}


		[HttpGet]
		public async Task<IActionResult> Existe(int id,string nome)
		{
			var result = await _service.Exist(nome);
			if (result != null && result.Nome == nome && result.Id != id)
			{
				return Json(new { resultado = true});
			}
			return Json(new { resultado = false });
		}
		[HttpGet]
		public async Task<IActionResult> Delete(int Id)
		{
			try
			{
				if (await _service.Remove(Id))
				{
					return RedirectToAction("Index");
				}
				else
				{
					return NotFound();
				}
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		[HttpPost]
		public async Task<IActionResult> UpdateOrder([FromBody]List<SortedItem> sortedItems)
		{
			// Iterate through sortedItems and update the database
			foreach (var sortedItem in sortedItems)
			{
				var tarefa = await _service.GetById(sortedItem.Id);
				if (tarefa != null && tarefa.OrdemApresentacao != sortedItem.Ordem)
				{
					tarefa.OrdemApresentacao = sortedItem.Ordem;
					await _service.Update(tarefa);
				}
			}

			return Ok(); // Return appropriate response
		}

	}
}
