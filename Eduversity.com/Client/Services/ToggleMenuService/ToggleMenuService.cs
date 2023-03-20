namespace Eduversity.com.Client.Services.ToggleMenuService
{
    public class ToggleMenuService: IToggleMenuService
    {
        private bool collapseNavMenu = true;
        public NavSubmenu Submenu { get; private set; } = NavSubmenu.None;

        public string NavMenuCssClass { get { return collapseNavMenu ? "collapse" : null!; } }

        public void ToggleNavMenu(NavSubmenu? submenu = null)
        {
            collapseNavMenu = !collapseNavMenu;
            Submenu = submenu ?? Submenu;
        }

        public void TogleSubmenu(NavSubmenu submenu)
        {
            if (Submenu == submenu)
                Submenu = NavSubmenu.None;
            else
                Submenu = submenu;
        }       
    }

    public enum NavSubmenu
    {
        None,
        First,
        Second
    }
}
