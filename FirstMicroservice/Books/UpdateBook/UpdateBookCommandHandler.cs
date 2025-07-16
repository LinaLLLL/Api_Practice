﻿namespace FirstMicroservice.Books.UpdateBook
{

    public record UpdateBookCommand(
        Guid Id,
        string Title,
        string Name,
        string Description,
        string ImageUrl,
        decimal Price,
        List<string> Category
        ) : ICommand<UpdateBookResult>;

    public record UpdateBookResult(bool IsSuccess);

    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(item => item.Title).NotEmpty().WithMessage("Title не может быть пустым");
            RuleFor(item => item.Name).NotEmpty().WithMessage("Name не может быть пустым");
            RuleFor(item => item.Price).GreaterThan(0).WithMessage("Price должен быть больше 0");
        }
    }

    public class UpdateBookCommandHandler(IDocumentSession session) 
        : ICommandHandler<UpdateBookCommand, UpdateBookResult>
    {
        public async Task<UpdateBookResult> Handle(UpdateBookCommand command, 
            CancellationToken cancellationToken)
        {
            var book = await session.LoadAsync<Book>(command.Id, cancellationToken);

            if(book is null)
            {
                throw new BookNotFoundException(command.Id);
            }

            command.Adapt(book);

            session.Update(book);
            await session.SaveChangesAsync(cancellationToken);
            return new UpdateBookResult(true);
        }
    }
}
