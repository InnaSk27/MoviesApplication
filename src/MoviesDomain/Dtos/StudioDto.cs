namespace MoviesDomain.Dtos;

public class StudioDto
{
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public List<Guid> MovieIds { get; set; }
}