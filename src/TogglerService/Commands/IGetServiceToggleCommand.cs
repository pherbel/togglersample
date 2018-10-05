namespace TogglerService.Commands
{
    using Boxed.AspNetCore;

    public interface IGetServiceToggleCommand : IAsyncCommand<string>
    {
    }
}
