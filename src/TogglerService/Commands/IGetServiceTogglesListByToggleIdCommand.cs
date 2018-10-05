namespace TogglerService.Commands
{
    using TogglerService.ViewModels;
    using Boxed.AspNetCore;

    public interface IGetServiceTogglesListByToggleIdCommand : IAsyncCommand<string>
    {
    }
}
