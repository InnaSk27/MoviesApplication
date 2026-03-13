namespace MoviesDomain.Entities
{
    public class Studio
    {
        public Studio(Guid Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Movie>? Movies { get; set; }
    }
}