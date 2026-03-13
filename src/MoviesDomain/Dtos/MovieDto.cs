using MoviesDomain.Dtos.Enums;
namespace MoviesDomain.Dtos;

public class MovieDto
{
    public Guid Id {get;set;}
    public string? Name {get;set;}
    public MoviesGenre Genre {get;set;}
    public int TicketPrice {get;set;}
    public Guid StudioId {get;set;}
}