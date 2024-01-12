using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereIsMyMoney.MVVM.Models
{
    public class CategoryModel
    {
        public string Name;
        public CategoryIconModel Icon;

        public CategoryModel(string name, CategoryIconModel icon)
        {
            Name = name;
            Icon = icon;
        }
    }
}
