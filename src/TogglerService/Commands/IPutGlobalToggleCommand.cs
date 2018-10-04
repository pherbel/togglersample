namespace TogglerService.Commands
{
    using TogglerService.ViewModels;
    using Boxed.AspNetCore;

    public interface IPutGlobalToggleCommand : IAsyncCommand<string, SaveGlobalToggleVM>
    {
    }
}
