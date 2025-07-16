using Marten;
using Marten.Schema;

namespace FirstMicroservice.Data.Seed
{
    public class InitializeBookDatabase : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();//сессия без кэширования
            if (!await session.Query<Book>().AnyAsync()) //проверка наличия данных
            {
                session.Store<Book>(InitialData.Books);//добавление данных
                await session.SaveChangesAsync(); //сохранение изменений
            }
        }
    }
}
