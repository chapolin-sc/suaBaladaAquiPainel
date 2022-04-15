using System.ComponentModel.DataAnnotations;

namespace suaBaladaAqui2.Models
{
    public class BookModel
    {
        public BookModel(){
        }
        public BookModel(int id, string nome, string fotografo, DateTime data, 
        ICollection<FotosModel> fotos, string NomeBucketnaAws)
        {
            Id = id;
            Nome = nome;
            Fotografo = fotografo;
            this.data = data;
            this.fotos = fotos;
            this.NomeBucketnaAws = NomeBucketnaAws;
        }

        public int Id { get; set; }
        
        [Display(Name = "Evento")]
        public string Nome { get; set; } = null!;
        
        public string Fotografo { get; set; } = null!;

        [Display(Name = "Data")]
        public DateTime data { get; set; }
        
        public ICollection<FotosModel> fotos { get; set; } = null!;

        public string NomeBucketnaAws { get; set; }

    }
}