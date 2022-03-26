using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace suaBaladaAqui2.Models
{
    public partial class EventosModel
    {
        public EventosModel()
        {}
        public EventosModel(int id, string evento1, string imagem, string atracoes, string cidade, string bairro, string endereco, string localName, DateTime dataEvento, string foneContato, string organizador, string responsavel)
        {
            Id = id;
            Evento1 = evento1;
            Imagem = imagem;
            Atracoes = atracoes;
            Cidade = cidade;
            Bairro = bairro;
            Endereco = endereco;
            LocalName = localName;
            DataEvento = dataEvento;
            FoneContato = foneContato;
            Organizador = organizador;
            Responsavel = responsavel;
        }

        public int Id { get; set; }

        [Display(Name = "Eventos")]
        public string Evento1 { get; set; } = null!;
        public string Imagem { get; set; } = null!;

        [Display(Name = "Atrações")]
        public string Atracoes { get; set; } = null!;
        public string Cidade { get; set; } = null!;
        public string Bairro { get; set; } = null!;

        [Display(Name = "Endereço")]
        public string Endereco { get; set; } = null!;

        [Display(Name = "Local")]
        public string LocalName { get; set; } = null!;

        [Display(Name = "Data")]
        public DateTime DataEvento { get; set; }

        [Display(Name = "Contato")]
        public string FoneContato { get; set; } = null!;
        public string Organizador { get; set; } = null!;

        [Display(Name = "Responsável")]
        public string Responsavel { get; set; } = null!;
    }
}
