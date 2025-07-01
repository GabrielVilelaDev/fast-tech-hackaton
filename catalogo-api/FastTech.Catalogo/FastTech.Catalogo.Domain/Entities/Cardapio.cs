using System;
using System.Collections.Generic;

namespace FastTech.Catalogo.Domain.Entities
{
    public class Cardapio : EntidadeBase
    {
        public string Nome { get; private set; }
        public string? Descricao { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime? DataEdicao { get; private set; }
        public DateTime? DataExclusao { get; private set; }

        public ICollection<Item> Itens { get; private set; } = [];

        public Cardapio(string nome, string? descricao, DateTime dataCriacao)
        {
            Id = Guid.NewGuid();
            ValidarDados(nome, descricao);

            Nome = nome;
            Descricao = descricao;
            DataCriacao = dataCriacao;
            DataEdicao = null;
            DataExclusao = null;
        }

        public Cardapio(string nome, string? descricao, ICollection<Item> itens, DateTime dataCriacao)
        {
            Id = Guid.NewGuid();
            ValidarDados(nome, descricao);

            Nome = nome;
            Descricao = descricao;
            DataCriacao = dataCriacao;
            DataEdicao = null;
            DataExclusao = null;

            AdicionarItens(itens);
        }

        public void Atualizar(string nome, string? descricao)
        {
            ValidarDados(nome, descricao);
            Nome = nome;
            Descricao = descricao;
            DataEdicao = DateTime.UtcNow;
        }

        public void Excluir()
        {
            if (DataExclusao != null)
                throw new InvalidOperationException("O cardápio já foi excluído.");

            DataExclusao = DateTime.UtcNow;
        }

        public void AtualizarItens(IEnumerable<Item> itensAtualizados)
        {
            if (itensAtualizados is null)
                throw new ArgumentException("A lista de itens não pode ser nula.");

            var itensParaRemover = Itens.Where(item => !itensAtualizados.Contains(item)).ToList();
            if(itensParaRemover.Count > 0)
                RemoverItens(itensParaRemover);

            var itensParaAdicionar = itensAtualizados.Where(item => !Itens.Contains(item)).ToList();
            if(itensParaAdicionar.Count > 0)
                AdicionarItens(itensParaAdicionar);
        }

        public void AdicionarItens(IEnumerable<Item> itens)
        {
            if (itens is null || !itens.Any())
                throw new ArgumentException("Necessário preencher ao menos um item.");

            foreach (var item in itens)
                AdicionarItem(item);
        }

        public void AdicionarItem(Item item)
        {
            if (item == null)
                throw new ArgumentException("Item não pode ser nulo.");

            if (!Itens.Contains(item))
                Itens.Add(item);
        }

        public void RemoverItem(Item item)
        {
            if (item == null || !Itens.Contains(item))
                throw new ArgumentException("Item inválido para remoção.");

            Itens.Remove(item);
        }

        public void RemoverItens(IEnumerable<Item> itens)
        {
            if (itens == null || Itens.Count == 0 || Itens.Intersect(itens).Count() != itens.Count())
                throw new ArgumentException("Itens inválidos para remoção.");

            foreach(var item in itens)
                RemoverItem(item);
        }

        public IEnumerable<Item> ListarItens()
        {
            return Itens;
        }

        private static void ValidarDados(string nome, string? descricao)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome é obrigatório.");

            if (nome.Length > 100)
                throw new ArgumentException("Nome deve ter no máximo 100 caracteres.");

            if (descricao != null && descricao.Length > 500)
                throw new ArgumentException("Descrição deve ter no máximo 500 caracteres.");
        }
    }
}
