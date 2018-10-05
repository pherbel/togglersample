namespace TogglerService.Commands
{
    using Boxed.AspNetCore;
    using TogglerService.ViewModels;

    public interface IPutServiceToggleCommand : IAsyncCommand<string, string, SaveServiceToggleVM>
    {
    }
}
