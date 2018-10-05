namespace TogglerService.Commands
{
    using TogglerService.ViewModels;
    using Boxed.AspNetCore;

    public interface IPostServiceToggleCommand : IAsyncCommand<SaveServiceToggleVM>
    {
    }
}
