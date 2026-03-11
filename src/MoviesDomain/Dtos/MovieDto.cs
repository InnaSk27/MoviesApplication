
namespace MoviesDomain.Dtos;

public class MovieDto
{
    public Guid Id {get;set;}
    public string? Name {get;set;}
    public int Genre {get;set;}
    public int TicketPrice {get;set;}
}