using System;
using System.Collections.Generic;

namespace suaBaladaAqui.Models
{
    public partial class Evento
    {
        public int Id { get; set; }
        public string Evento1 { get; set; } = null!;
        public string Imagem { get; set; } = null!;
        public string Atracoes { get; set; } = null!;
        public string Cidade { get; set; } = null!;
        public string Bairro { get; set; } = null!;
        public string Endereco { get; set; } = null!;
        public string LocalName { get; set; } = null!;
        public DateTime DataEvento { get; set; }
        public string FoneContato { get; set; } = null!;
        public string Organizador { get; set; } = null!;
        public string Responsavel { get; set; } = null!;
    }
}
