using System.Windows.Input;

namespace FirstMicroservice.CQRS
{
    //команды, которые не возвращают значение
    public interface ICommand : ICommand<Unit>

    {
    }
    //команды, которые возвращают значение
    public interface ICommand<out TResponse> : IRequest<TResponse>

    {
    }
}
