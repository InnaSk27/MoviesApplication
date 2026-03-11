namespace MoviesDomain.Entities;

public class Movie
{
    public Guid Id {get;set;}
    public string? Name {get;set;}
    public int Genre {get;set;}
    public int TicketPrice {get;set;}
}