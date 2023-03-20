namespace Eduversity.com.Client.Services.ToggleService
{
    public interface IToggleService
    {
        int NavSubmenuId { get; }
        string NavMenuCssClass { get; }
        void ToggleNavMenu(int submenuId);
        void TogleSubmenu(int submenuId);
    }
}