using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastTech.Catalogo.Domain.Entities
{
    public class TipoRefeicao
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }

        public IReadOnlyCollection<Item> Itens => _itens.AsReadOnly();
        private readonly List<Item> _itens = [];

        protected TipoRefeicao() { }

        public TipoRefeicao(string nome)
        {
            Validar(nome);
            Nome = nome;
        }

        public void AtualizarNome(string nome)
        {
            Validar(nome);
            Nome = nome;
        }

        private static void Validar(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome é obrigatório.");

            if (nome.Length > 100)
                throw new ArgumentException("Nome deve ter no máximo 100 caracteres.");
        }
    }
}
