using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleDeContas.Models
{
    [Table("Produto")]
    public class Produto
    {
        [Column("Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }


        [Column("Nome")]
        [Display(Name = "Nome Produto")]
        public String Nome { get; set; }

        [DataType(DataType.Currency)]
        [Column("Valor")]
        [Display(Name = "Valor")]
        public float Valor { get; set; }

    }
}
