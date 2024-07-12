using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProdutoProntoDigital.Models
{
    public class Produto
    {
        [Key]
        public int PROD_ID { get; set; }

        [Required]
        public string PROD_NOME { get; set; }

        [Required]
        public decimal PROD_PRECO { get; set; }

        [Required]
        public int PROD_QTD { get; set; }

        [Required]
        public int CAT_ID { get; set; }

        [NotMapped]
        public string NomeCategoria { get; set; }
    }
}
