using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace suaBaladaAqui2.Models
{
    public partial class UsuariosModel
    {
        public UsuariosModel(){}
        
        public UsuariosModel(int id, string nome, string login, string senha, string tipo, DateTime dataCadastro)
        {
            Id = id;
            Nome = nome;
            Login = login;
            Senha = senha;
            Tipo = tipo;
            DataCadastro = dataCadastro;
        }

        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string Senha { get; set; } = null!;
        public string Tipo { get; set; } = null!;

        [Display(Name = "Data")]
        public DateTime DataCadastro { get; set; }
    }
}
