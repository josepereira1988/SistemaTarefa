using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dominio.models
{
    public class Tarefa
    {
        // Identificador da tarefa (chave primária)
        public int Id { get; set; }

        // Nome da tarefa - não deve permitir duplicidade
        [Required(ErrorMessage = "O nome da tarefa é obrigatório.")]
        [StringLength(30, ErrorMessage = "O nome da tarefa deve conter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        // Custo (R$) - valor fracionário maior ou igual a zero
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Digite um valor de custo válido.")]
        [Range(0, double.MaxValue, ErrorMessage = "O custo deve ser maior ou igual a zero.")]
        public double Custo { get; set; }

        // Data limite - deve ser uma data válida e exibida sempre no formato DD/MM/AAAA
        [Display(Name = "Data Limite")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DataLimite { get; set; }

        // Ordem de apresentação - campo numérico, não repetido, que servirá para ordenar os registros na tela
        [Range(1, int.MaxValue, ErrorMessage = "A ordem de apresentação deve ser maior ou igual a 1.")]
        public int OrdemApresentacao { get; set; }
    }
}
