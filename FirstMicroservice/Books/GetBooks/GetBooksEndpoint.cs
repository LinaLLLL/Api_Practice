namespace FirstMicroservice.Books.GetBooks
{
    public record GetBooksRequest(int? PageNumber = 1, int? PageSize = 5);
    public record GetBookResponse(IEnumerable<Book> Books);
    public class GetBooksEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/books", async (
                [AsParameters] GetBooksRequest request,
                ISender sender) =>
            {
                GetBooksQuery query = request.Adapt<GetBooksQuery>();
                GetBooksResult result = await sender.Send(query);
                GetBookResponse response = result.Adapt<GetBookResponse>();
                return Results.Ok(response);
            });
        }
    }
}
