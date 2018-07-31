using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace elogsheet
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool setform = Convert.ToBoolean(Session["setform"]);
            if (!setform)
            {
                NavigationMenu.Items.Remove(NavigationMenu.FindItem("Set Form"));
            }
            
        }

        protected void NavigationMenu_MenuItemClick(object sender, MenuEventArgs e)
        {
            if (e.CommandName == "Log Out")
            {
                Session.Clear();
            }
        }
    }
}
