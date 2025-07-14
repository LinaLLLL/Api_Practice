namespace FirstMicroservice.CQRS
{
    //общий запрос
    public interface IQuery<out TResponse> : IRequest<TResponse>
        where TResponse : notnull
    {
    }
}
