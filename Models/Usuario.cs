using System;
using System.Collections.Generic;

namespace suaBaladaAqui.Models
{
    public partial class Usuario
    {
        public Usuario(){}
        
        public Usuario(int id, string nome, string login, string senha, string tipo, DateTime dataCadastro)
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
        public DateTime DataCadastro { get; set; }
    }
}
