
namespace suaBaladaAqui2.ViewsModels;

    public class carouselViewModel
    {
        
        public carouselViewModel()
        {}
        
        public carouselViewModel(string imagem, string frasePrincipal, string fraseSecundaria)
        {
            Imagem = imagem;
            FrasePrincipal = frasePrincipal;
            FraseSecundaria = fraseSecundaria;
        }

        public string Imagem { get; set; } = null!;
        public string FrasePrincipal { get; set; } = null!;
        public string FraseSecundaria { get; set; } = null!;
    }
