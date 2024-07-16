namespace ProdutoProntoDigital.Models
{
    public class ProdutoDTO
    {
        public int PROD_ID { get; set; }
        public string PROD_NOME { get; set; }
        public decimal PROD_PRECO { get; set; }
        public int PROD_QTD { get; set; }
        public int CAT_ID { get; set; }
        public string NomeCategoria { get; set; }
    }
}
