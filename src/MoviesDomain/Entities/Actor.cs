namespace MoviesDomain.Entities
{
    public class Actor
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public virtual ICollection<MovieActor>? MovieActors { get; set; }
    }
}