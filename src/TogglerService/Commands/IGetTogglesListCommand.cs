﻿namespace TogglerService.Commands
{
    using TogglerService.ViewModels;
    using Boxed.AspNetCore;

    public interface IGetTogglesListCommand : IAsyncCommand<string,string>
    {
    }
}
