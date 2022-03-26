using System.ComponentModel.DataAnnotations;

namespace suaBaladaAqui2.Models
{
    public class CarouselModel
    {
        public CarouselModel()
        {}

        public CarouselModel(int id, string imagem, string frasePrincipal, string fraseSecundaria, bool ativa, int ordenacao)
        {
            Id = id;
            Imagem = imagem;
            FrasePrincipal = frasePrincipal;
            FraseSecundaria = fraseSecundaria;
            Ativa = ativa;
            Ordenacao = ordenacao;
        }

        public int Id { get; set; }
        public string Imagem { get; set; } = null!;

        [Display(Name = "Frase Principal")]
        public string FrasePrincipal { get; set; } = null!;

        [Display(Name = "Frase Secundaria")]
        public string FraseSecundaria { get; set; } = null!;
        public bool Ativa { get; set; }

        [Display(Name = "Ordem")]
        public int Ordenacao { get; set; }
    }
}