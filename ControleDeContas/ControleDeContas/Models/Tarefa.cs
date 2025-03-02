using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleDeContas.Models
{

    [Table("Tarefa")]

    public class Tarefa
    {
        [Column("Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Column("Nome")]
        [Display(Name = "Nome Tarefa")]
        public String Nome { get; set; }

        [Column("Data")]
        [Display(Name = "Data De Entrega")]
        public String Data { get; set; }


    }
}
