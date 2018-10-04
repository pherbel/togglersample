namespace TogglerService.Commands
{
    using Boxed.AspNetCore;

    public interface IDeleteGlobalToggleCommand : IAsyncCommand<string>
    {
    }
}
