namespace TogglerService.Commands
{
    using Boxed.AspNetCore;

    public interface IGetGlobalToggleCommand : IAsyncCommand<string>
    {
    }
}
