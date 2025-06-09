using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastTech.Catalogo.Domain.ValueObjects;

namespace FastTech.Catalogo.Domain.Entities
{
    public class Item
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public TipoRefeicao TipoRefeicao { get; private set; }
        public int TipoRefeicaoId { get; private set; }
        public Preco Preco { get; private set; }

        protected Item() { }

        public Item(string nome, string descricao, TipoRefeicao tipoRefeicao, Preco preco)
        {
            ValidarDados(nome, descricao, tipoRefeicao, preco);

            Id = Guid.NewGuid();
            Nome = nome;
            Descricao = descricao;
            TipoRefeicao = tipoRefeicao;
            TipoRefeicaoId = tipoRefeicao.Id;
            Preco = preco;
        }

        public void Atualizar(string nome, string descricao, TipoRefeicao tipoRefeicao, Preco preco)
        {
            ValidarDados(nome, descricao, tipoRefeicao, preco);

            Nome = nome;
            Descricao = descricao;
            TipoRefeicao = tipoRefeicao;
            TipoRefeicaoId = tipoRefeicao.Id;
            Preco = preco;
        }

        private static void ValidarDados(string nome, string descricao, TipoRefeicao tipo, Preco preco)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome é obrigatório.");

            if (nome.Length > 250)
                throw new ArgumentException("Nome deve ter no máximo 250 caracteres.");

            if (descricao.Length > 500)
                throw new ArgumentException("Descrição deve ter no máximo 500 caracteres.");

            if (tipo is null)
                throw new ArgumentNullException(nameof(tipo), "Tipo de refeição é obrigatório.");

            if (preco is null)
                throw new ArgumentNullException(nameof(preco), "Preço é obrigatório.");
        }
    }
}
