namespace Demo.Domain.Entities
{
    public class Book : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid AuthorId { get; set; }
        public DateTime PublishDate { get; set; }

    }
}
