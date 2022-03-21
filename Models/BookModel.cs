namespace suaBaladaAqui2.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Fotografo { get; set; } = null!;
        public DateTime data { get; set; }
    }
}