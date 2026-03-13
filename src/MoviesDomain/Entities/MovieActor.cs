namespace MoviesDomain.Entities
{
    public class MovieActor
    {
        //Constructor
        public MovieActor(Guid movieId, Guid actorId)
        {
            this.MovieId = movieId;
            this.ActorId = actorId;
        } 

        public Movie? Movie { get; set; }
        public Guid MovieId { get; set; }
        public Actor? Actor { get; set; }
        public Guid ActorId { get; set; }
    }
}