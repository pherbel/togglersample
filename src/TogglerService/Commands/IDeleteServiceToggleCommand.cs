namespace TogglerService.Commands
{
    using Boxed.AspNetCore;

    public interface IDeleteServiceToggleCommand : IAsyncCommand<string>
    {
    }
}
