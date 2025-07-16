namespace FirstMicroservice.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request,
            RequestHandlerDelegate<TResponse> next, 
            CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResult = await Task.WhenAll(
                validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failres = validationResult
                .Where(request => request.Errors.Any())
                .SelectMany(r => r.Errors)
                .ToList();

            if (failres.Any())
            {
                throw new Exception("Ошибка валидации");
            }
            return await next();

        }
    }
}
