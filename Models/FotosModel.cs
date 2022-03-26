using System.ComponentModel.DataAnnotations;

namespace suaBaladaAqui2.Models
{
    public class FotosModel
    {
        public FotosModel(){
        }

        public FotosModel(int id, string foto, DateTime data)
        {
            this.id = id;
            Foto = foto;
            this.Data = data;
        }

        public int id { get; set; }

        [Display(Name = "Foto(Nome)")]
        public string Foto { get; set; } = null!;
        public DateTime Data { get; set; }
        public BookModel Book { get; set; } = null!; 
    }
}