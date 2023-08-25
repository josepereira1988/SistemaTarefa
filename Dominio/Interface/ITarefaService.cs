using Dominio.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interface
{
	public interface ITarefaService
	{
		Task<Tarefa> GetById(int id);
		Task<Tarefa> Exist(string nome);
		Task<List<Tarefa>> GetAll();
		Task<bool> Update(Tarefa tarefa);
		Task<bool> Add(Tarefa tarefa);
		Task<bool> Remove(int Id);
	}
}
