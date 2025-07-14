namespace FirstMicroservice.CQRS
{
    //обработчик для команд, которые не возвращают результат
    public interface ICommandHandler<in TCommand> : ICommandHandler<TCommand, Unit>
        where TCommand : ICommand<Unit>
    {
    }

    //обработчик для команд, которые возвращают результат
    public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
        where TResponse : notnull
    {

    }

}
