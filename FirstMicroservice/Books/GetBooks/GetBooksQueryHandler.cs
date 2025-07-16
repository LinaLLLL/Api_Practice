namespace FirstMicroservice.Books.GetBooks
{
    public record GetBooksQuery(int? PageNumber = 1, int? PageSize = 5) : IQuery<GetBooksResult>;
    public record GetBooksResult(IEnumerable<Book> Books);

    //обработчик запроса
    internal class GetBooksQueryHandler(IDocumentSession session) : IQueryHandler<GetBooksQuery, GetBooksResult>
    {
        public async Task<GetBooksResult> Handle(GetBooksQuery query, CancellationToken cancellationToken)
        {
            var books = await session.Query<Book>()
                .ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 5, cancellationToken);
            return new GetBooksResult(books);
        }
    }
}
