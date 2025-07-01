namespace FastTech.Catalogo.Application.Dtos
{
    public class CardapioInputDto
    {
        public Guid? Id { get; set; }
        public required string Nome { get; set; }
        public required string Descricao { get; set; }
        public required IEnumerable<Guid> ItensIds { get; set; }
    }
}
