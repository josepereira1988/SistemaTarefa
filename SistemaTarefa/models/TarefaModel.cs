using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SistemaTarefa.models
{
	public class TarefaModel
	{

		public int Id { get; set; }
		public string Nome { get; set; }
		public double Custo { get; set; }
		public string DataLimite { get; set; }
		public int OrdemApresentacao { get; set; }
	}
}
