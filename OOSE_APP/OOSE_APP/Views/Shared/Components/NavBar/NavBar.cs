﻿using Logic.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Views.Shared.Components.NavBar
{
    public class NavBar : ViewComponent
    {
        public IViewComponentResult Invoke(string rolnaam)
        {
            return View(GetMenuItemsByRol(rolnaam));
        }

        private List<string> GetMenuItemsByRol(string rolnaam)
        {
            List<string> menuItems = new List<string>();

            if (rolnaam == Rollen.ADMIN || rolnaam == Rollen.DOCENT)
            {
                menuItems.Add("Gebruikers");
            }

            if (rolnaam == Rollen.DOCENT)
            {
                menuItems.Add("Onderwijs");
                menuItems.Add("Beoordelingen");
                menuItems.Add("Beoordelingsmodellen");
                menuItems.Add("Onderwijsuitvoeringen");

            }

            if (rolnaam == Rollen.STUDENT)
            {
                menuItems.Add("Opleidingen");
            }

            if (rolnaam == Rollen.STUDENT || rolnaam == Rollen.DOCENT)
            {
                menuItems.Add("Tentamens");
                menuItems.Add("Lessen");
            }

            if (!string.IsNullOrWhiteSpace(rolnaam))
            {
                menuItems.Add("Uitloggen");
            }

            return menuItems.ToList();
        }
    }
}
