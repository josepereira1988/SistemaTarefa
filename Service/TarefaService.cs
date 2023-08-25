using Dominio.Interface;
using Dominio.models;

namespace Service
{
	public class TarefaService : ITarefaService
	{
		private readonly ITarefaPersist _persist;
		public TarefaService(ITarefaPersist persist)
		{
			_persist = persist;
		}

		public async Task<bool> Add(Tarefa tarefa)
		{
			try
			{
				if (await _persist.Add(tarefa))
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		public async Task<Tarefa> Exist(string nome)
		{
			return await _persist.Exist(nome);
		}

		public async Task<List<Tarefa>> GetAll()
		{
			var tarefas = await _persist.GetAll();
			return tarefas;
		}

		public async Task<Tarefa> GetById(int Id)
		{
			return await _persist.GetById(Id);
		}

		public async Task<bool> Remove(int Id)
		{
			try
			{
				if (await _persist.Remove(Id))
				{
					return true;
				}
				else
				{
					return false;
				}

			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		public async Task<bool> Update(Tarefa tarefa)
		{
			try
			{
				if (await _persist.Update(tarefa))
				{				
					return true;
				}
				else
				{
					return false;
				}

			}
			catch (Exception ex)
			{

				throw ex;
			}
		}		
	}
}
