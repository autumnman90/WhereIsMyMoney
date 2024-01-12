using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereIsMyMoney.MVVM.Models;

namespace WhereIsMyMoney.Stores
{
    class IconStore
    {   
        private List<CategoryIconModel> CategoryIcons { get;}
        
        public List<CategoryIconModel> GetList() { return CategoryIcons;}

        public IconStore() 
        {
            CategoryIcons = new List<CategoryIconModel>();
            CategoryIcons.Add(new CategoryIconModel("Auto", "/Resources/Images/auto-ico.png"));
            CategoryIcons.Add(new CategoryIconModel("Bank", "/Resources/Images/bank-ico.png"));
            CategoryIcons.Add(new CategoryIconModel("Bankkarte", "/Resources/Images/bankkarte-ico.png"));
            CategoryIcons.Add(new CategoryIconModel("Bildung", "/Resources/Images/bildung-ico.png"));
            CategoryIcons.Add(new CategoryIconModel("Buchhaltung", "/Resources/Images/buchhaltung-ico.png"));
            CategoryIcons.Add(new CategoryIconModel("Elektrizität", "/Resources/Images/elektrizität-ico.png"));
            CategoryIcons.Add(new CategoryIconModel("Fixkosten", "/Resources/Images/fixkosten-ico.png"));
            CategoryIcons.Add(new CategoryIconModel("Garderobe", "/Resources/Images/garderobe-ico.png"));
            CategoryIcons.Add(new CategoryIconModel("Gehalt", "/Resources/Images/gehalt-ico.png"));
            CategoryIcons.Add(new CategoryIconModel("Geld", "/Resources/Images/geld-ico.png"));
            CategoryIcons.Add(new CategoryIconModel("Gelddose", "/Resources/Images/gelddose-ico.png"));
            CategoryIcons.Add(new CategoryIconModel("Haus", "/Resources/Images/haus-ico.png"));
            CategoryIcons.Add(new CategoryIconModel("Kasse", "/Resources/Images/kasse-ico.png"));
            CategoryIcons.Add(new CategoryIconModel("Kleidung", "/Resources/Images/kleidung-ico.png"));
            CategoryIcons.Add(new CategoryIconModel("Lebensmittel", "/Resources/Images/lebensmittel-ico.png"));
            CategoryIcons.Add(new CategoryIconModel("Mehr", "/Resources/Images/mehr-ico.png"));
            CategoryIcons.Add(new CategoryIconModel("Möbel", "/Resources/Images/möbel-ico.png"));
            CategoryIcons.Add(new CategoryIconModel("Mobilfunk", "/Resources/Images/mobilfunk-ico.png"));
            CategoryIcons.Add(new CategoryIconModel("Restaurant", "/Resources/Images/restaurant-ico.png"));
            CategoryIcons.Add(new CategoryIconModel("Schmuck", "/Resources/Images/schmuck-ico.png"));
            CategoryIcons.Add(new CategoryIconModel("Schreibwaren", "/Resources/Images/schreibwaren-ico.png"));
            CategoryIcons.Add(new CategoryIconModel("Sparbuch", "/Resources/Images/sparbuch-ico.png"));
            CategoryIcons.Add(new CategoryIconModel("Spaten", "/Resources/Images/spaten-ico.png"));
            CategoryIcons.Add(new CategoryIconModel("Ticket", "/Resources/Images/ticket-ico.png"));
            CategoryIcons.Add(new CategoryIconModel("Tresor", "/Resources/Images/tresor-ico.png"));
            CategoryIcons.Add(new CategoryIconModel("Urlaub", "/Resources/Images/urlaub-ico.png"));
            CategoryIcons.Add(new CategoryIconModel("Warenkorb", "/Resources/Images/warenkorb-ico.png"));
        }
    }
}
