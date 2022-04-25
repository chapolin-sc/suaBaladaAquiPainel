using System.ComponentModel.DataAnnotations;

namespace suaBaladaAqui2.ServiceModels
{
    public class BookServiceModel
    {
        public BookServiceModel(){
        }

        public BookServiceModel(string nome, string nomeFotografo, DateTime data)
        {
            this.nome = nome;
            this.nomeFotografo = nomeFotografo;
            this.data = data;
        }

        [Display(Name = "Evento")]
        public string nome { get; set; } = null!;
        
        [Display(Name = "Fotografo")]
        public string nomeFotografo { get; set; } = null!;

        [Display(Name = "Data")]
        public DateTime data { get; set; }
    }
}