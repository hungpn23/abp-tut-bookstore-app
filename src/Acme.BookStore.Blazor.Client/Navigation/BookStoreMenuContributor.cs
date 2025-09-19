using System;
using System.Threading.Tasks;
using Acme.BookStore.Localization;
using Acme.BookStore.Permissions;
using Microsoft.Extensions.Configuration;
using Volo.Abp.Account.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.UI.Navigation;

namespace Acme.BookStore.Blazor.Client.Navigation;

public class BookStoreMenuContributor : IMenuContributor
{
    private readonly IConfiguration _configuration;

    public BookStoreMenuContributor(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
        else if (context.Menu.Name == StandardMenus.User)
        {
            await ConfigureUserMenuAsync(context);
        }
    }

    private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<BookStoreResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem("BookStore.Home", l["Menu:Home"], "/", icon: "fas fa-home")
        );

        var bookStoreMenu = new ApplicationMenuItem(
            "BooksStore",
            l["Menu:BookStore"],
            icon: "fa fa-book"
        );

        context.Menu.AddItem(bookStoreMenu);

        //CHECK the PERMISSION
        bookStoreMenu.AddItem(
            new ApplicationMenuItem(
                "BooksStore.Books",
                l["Menu:Books"],
                url: "/books"
            ).RequirePermissions(BookStorePermissions.Books.Default)
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                "BooksStore.Authors",
                l["Menu:Authors"],
                url: "/authors"
            ).RequirePermissions(BookStorePermissions.Books.Default)
        );
    }

    private async Task ConfigureUserMenuAsync(MenuConfigurationContext context)
    {
        var accountStringLocalizer = context.GetLocalizer<AccountResource>();
        var authServerUrl = _configuration["AuthServer:Authority"] ?? "";

        context.Menu.AddItem(
            new ApplicationMenuItem(
                "Account.Manage",
                accountStringLocalizer["MyAccount"],
                $"{authServerUrl.EnsureEndsWith('/')}Account/Manage",
                icon: "fa fa-cog",
                order: 1000,
                target: "_blank"
            ).RequireAuthenticated()
        );

        await Task.CompletedTask;
    }
}
