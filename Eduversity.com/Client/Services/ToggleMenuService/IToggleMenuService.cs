namespace Eduversity.com.Client.Services.ToggleMenuService
{
    public interface IToggleMenuService
    {
        NavSubmenu Submenu { get; }
        string NavMenuCssClass { get; }
        void ToggleNavMenu(NavSubmenu? submenu = null);
        void TogleSubmenu(NavSubmenu submenu);
    }
}
