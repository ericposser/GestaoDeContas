using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleDeContas.Models
{
    [Table("Categoria")]
    public class Categoria
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("Nome")]
        [Display(Name = "Nome Categoria")]
        public string Nome { get; set; }

    }
}
