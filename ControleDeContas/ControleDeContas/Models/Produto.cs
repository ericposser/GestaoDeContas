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

        [Column("Data")]
        [Display(Name = "Data")]
        public String Data { get; set; }

        [Column("Pago")]
        [Display(Name = "Pago?")]
        public Boolean Pago { get; set; }

        [Required(ErrorMessage = "A categoria é obrigatória.")]
        public virtual Categoria Categoria { get; set; }

    }
}
