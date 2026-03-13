namespace MoviesDomain.Entities
{
    public class Studio
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Movie>? Movies { get; set; }
    }
}