using System;
using System.Collections.Generic;

namespace suaBaladaAqui.ViewsModels
{
    public partial class boxBaladaViewsModels
    {
        public boxBaladaViewsModels(string evento1, string dataEvento, string cidade, string localName, string imagem)
        {
            Evento1 = evento1;
            DataEvento = dataEvento;
            Cidade = cidade;
            LocalName = localName;
            Imagem = imagem;
        }

        public string Evento1 { get; set; } = null!;
        public string DataEvento { get; set; }
        public string Cidade { get; set; } = null!;
        public string LocalName { get; set; } = null!;
        public string Imagem { get; set; } = null!;
    }
}
