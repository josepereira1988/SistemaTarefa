using Dominio.Interface;
using Dominio.models;
using Microsoft.EntityFrameworkCore;
using Persist.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persist.Persist
{
	public class TarefaPersist : ITarefaPersist
	{
		private MyContext _context;

		public TarefaPersist(MyContext context)
		{
			_context = context;
		}
		public async Task<bool> Add(Tarefa tarefa)
		{
			using (var transaction = _context.Database.BeginTransaction())
			{
				try
				{
					tarefa.OrdemApresentacao = ProximaOrdemApresentacao();
					_context.Add(tarefa);

					await _context.SaveChangesAsync();
					transaction.Commit();

					return true;
				}
				catch (Exception ex)
				{
					transaction.Rollback();
					throw ex;
				}
			}
		}

		public async Task<List<Tarefa>> GetAll()
		{
			return await _context.tarefas.OrderBy(t => t.OrdemApresentacao).ToListAsync();
		}

		public async Task<Tarefa> GetById(int id)
		{
			return await _context.tarefas.FirstOrDefaultAsync(t => t.Id == id);
		}
		public async Task<bool> Remove(int Id)
		{
			using (var transaction = _context.Database.BeginTransaction())
			{
				try
				{
					var tarefa = await _context.tarefas.FindAsync(Id);
					if (tarefa == null)
					{
						return false; // O objeto não foi encontrado, não é possível remover
					}

					_context.tarefas.Remove(tarefa);
					await _context.SaveChangesAsync();
					transaction.Commit();

					return true; // Remoção bem-sucedida
				}
				catch (Exception ex)
				{
					throw ex; // Algum erro ocorreu durante a remoção
				}
			}

		}

		public async Task<bool> Update(Tarefa tarefa)
		{
			try
			{
				var tarefaExistente = await _context.tarefas.FindAsync(tarefa.Id);
				if (tarefaExistente == null)
				{
					return false; // A tarefa não foi encontrada, não é possível atualizar
				}
				tarefaExistente.Nome = tarefa.Nome;
				tarefaExistente.Custo = tarefa.Custo;
				tarefaExistente.DataLimite = tarefa.DataLimite;
				tarefaExistente.Custo = tarefa.Custo;
				tarefaExistente.OrdemApresentacao = tarefa.OrdemApresentacao;
				_context.tarefas.Update(tarefaExistente);
				await _context.SaveChangesAsync();

				return true; // Atualização bem- sucedida
			}
			catch (Exception ex)
			{
				throw ex; // Algum erro ocorreu durante a atualização
			}
		}
		public int ProximaOrdemApresentacao()
		{
			var maiorOrdem = _context.tarefas.Max(t => (int?)t.OrdemApresentacao) ?? 0;
			return maiorOrdem + 1;
		}

		public async Task<Tarefa> Exist(string nome)
		{
			return await _context.tarefas.Where(t => t.Nome == nome).FirstOrDefaultAsync() ;
		}
	}
}
