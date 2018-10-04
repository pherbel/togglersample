namespace TogglerService.Commands
{
    using TogglerService.ViewModels;
    using Boxed.AspNetCore;

    public interface IPostGlobalToggleCommand : IAsyncCommand<SaveGlobalToggleVM>
    {
    }
}
