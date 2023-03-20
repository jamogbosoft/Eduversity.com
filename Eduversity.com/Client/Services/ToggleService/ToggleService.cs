namespace Eduversity.com.Client.Services.ToggleService
{
    public class ToggleService : IToggleService
    {
        private bool collapseNavMenu = false;
        public int NavSubmenuId { get; private set; } = 0;
        public string NavMenuCssClass { get { return collapseNavMenu ? "collapse" : null!; } }

        public void ToggleNavMenu(int submenuId)
        {
            collapseNavMenu = !collapseNavMenu;
            NavSubmenuId = submenuId;
        }

        public void TogleSubmenu(int submenuId)
        {
            if (NavSubmenuId == submenuId)
                NavSubmenuId = 0;
            else
                NavSubmenuId = submenuId;
        }
    }
}
