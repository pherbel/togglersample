namespace TogglerService.Commands
{
    using TogglerService.ViewModels;
    using Boxed.AspNetCore;

    public interface IPutServiceToggleCommand : IAsyncCommand<string, SaveServiceToggleVM>
    {
    }
}
