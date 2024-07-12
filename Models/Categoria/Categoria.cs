using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProdutoProntoDigital.Models
{
    public class Categoria
    {
        [Key]
        public int CAT_ID { get; set; }

        [Required]
        public string CAT_NOME { get; set; }
    }
}
