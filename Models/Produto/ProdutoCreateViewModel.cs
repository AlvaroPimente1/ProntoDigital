using System.ComponentModel.DataAnnotations;

namespace ProdutoProntoDigital.Models
{
    public class ProdutoCreateViewModel
    {
        [Required(ErrorMessage = "O nome do produto é obrigatório")]
        public string PROD_NOME { get; set; }

        [Required(ErrorMessage = "O preço do produto é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero")]
        public decimal PROD_PRECO { get; set; }

        [Required(ErrorMessage = "A quantidade do produto é obrigatória")]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser pelo menos 1")]
        public int PROD_QTD { get; set; }

        [Required(ErrorMessage = "A categoria do produto é obrigatória")]
        public int CAT_ID { get; set; }
    }
}
