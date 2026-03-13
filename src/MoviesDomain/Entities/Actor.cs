namespace MoviesDomain.Entities
{
    public class Actor
    {
        public Actor(Guid Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<MovieActor>? MovieActors { get; set; }
    }
}