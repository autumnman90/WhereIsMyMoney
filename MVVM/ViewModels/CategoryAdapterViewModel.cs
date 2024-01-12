using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereIsMyMoney.MVVM.Models;

namespace WhereIsMyMoney.MVVM.ViewModels
{
    class CategoryAdapterViewModel : BaseViewModel
    {
        private CategoryModel _category;

        public string CategoryName => _category.Name;
        public CategoryIconModel CategoryIcon => _category.Icon;

        public CategoryAdapterViewModel(CategoryModel categoryModel)
        {
            _category = categoryModel;
        }
    }
}
