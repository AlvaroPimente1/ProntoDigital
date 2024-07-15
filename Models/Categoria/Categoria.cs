using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProdutoProntoDigital.Models
{
    public class Categoria
    {
        [Key]
        public int CAT_ID { get; set; }

        [Required]
        [Display(Name = "Categoria")]
        public string CAT_NOME { get; set; }

        public bool CAT_STATUS { get; set; } = true;
    }
}
