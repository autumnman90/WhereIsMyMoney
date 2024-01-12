using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using WhereIsMyMoney.Core;
using WhereIsMyMoney.Customs;
using WhereIsMyMoney.MVVM.Models;
using WhereIsMyMoney.Stores;

namespace WhereIsMyMoney.MVVM.ViewModels
{
    class CategoriesViewModel : BaseViewModel
    {
        private IconStore IconStore = new IconStore();
        private List<CategoryIconModel> _categoryIcons;
        public List<CategoryIconModel> CategoryIcons 
        {
            get {return _categoryIcons;}
            set 
            { 
                _categoryIcons = value;
                OnPropertyChanged();
            }
        }

        public DelegateCommand SaveNewCategoryCommand { get; private set; }
        public DelegateCommand DeleteSelectedCategoryCommand { get; private set; }

        private ObservableCollection<CategoryAdapterViewModel> _categories = new ObservableCollection<CategoryAdapterViewModel>();

        public ObservableCollection<CategoryAdapterViewModel> Categories
        {
            get { return _categories;}
            set 
            { 
                _categories = value; 
                OnPropertyChanged(); 
            }
        }

        private CategoryAdapterViewModel _selectedCategory;

        public CategoryAdapterViewModel SelectedCategory
        {
            get { return _selectedCategory; }
            set 
            { 
                _selectedCategory = value; 
                OnPropertyChanged();
            }
        }

        private string _nameInput;

        public string NameInput
        {
            get { return _nameInput; }
            set { _nameInput = value; }
        }

        private CategoryIconModel _selectedIcon;

        public CategoryIconModel SelectedIcon
        {
            get { return _selectedIcon; }
            set { _selectedIcon = value; }
        }


        public CategoriesViewModel()
        {
            SaveNewCategoryCommand = new DelegateCommand((o)=> SaveNewCategory());
            DeleteSelectedCategoryCommand = new DelegateCommand((o)=> DeleteSelectedCategory());
            CategoryIcons = IconStore.GetList();
            LoadCategories();
            Categories = new ObservableCollection<CategoryAdapterViewModel>(Categories.OrderBy(c => c.CategoryName));
        }

        private void SaveNewCategory()
        {
            if (NameInput != null && NameInput.Length > 0 && SelectedIcon != null)
            {
                var categoryIcon = new CategoryIconModel(SelectedIcon.Name);
                var newEntry = new CategoryAdapterViewModel(new CategoryModel(NameInput, categoryIcon));
                var hit = false;
                foreach (var categorie in Categories)
                {
                    if (categorie.CategoryName.Trim().ToLower()==NameInput.Trim().ToLower())
                    {
                        hit = true;
                    }
                }
                if (hit)
                {
                    var customWarningBox = new CustomWrongInputWarning("Kategorie existiert bereits!");
                    customWarningBox.ShowDialog();
                }
                else
                {
                    Categories.Add(newEntry);
                    CategoryStore.Instance.AddCategory(new CategoryModel(NameInput, categoryIcon));
                    var customWarningBox = new CustomWrongInputWarning("Kategorie hinzugefügt");
                    customWarningBox.ShowDialog();
                }
            }
            else if (NameInput == null || NameInput.Length < 1)
            {
                var customWarningBox = new CustomWrongInputWarning("Bitte Namen Eingeben!");
                customWarningBox.ShowDialog();
            }
            else if (SelectedIcon == null)
            {
                var customWarningBox = new CustomWrongInputWarning("Bitte Icon wählen!");
                customWarningBox.ShowDialog();
            }
            Categories = new ObservableCollection<CategoryAdapterViewModel>(Categories.OrderBy(c => c.CategoryName));
        }

        private void DeleteSelectedCategory()   
        {
            var customMessageBox = new CustomDeleteWarning("Achtung!", $"Bist du dir sicher das du die Kategorie {SelectedCategory.CategoryName} löschen möchtest?");
            bool? result = customMessageBox.ShowDialog();

            if (result == true)
            {
                Categories.Remove(SelectedCategory);
                CategoryStore.Instance.RemoveCategory(new CategoryModel(SelectedCategory.CategoryName, SelectedCategory.CategoryIcon));
            }
            Categories = new ObservableCollection<CategoryAdapterViewModel>(Categories.OrderBy(c => c.CategoryName));
        }

        private void LoadCategories()
        {
            var catStore = CategoryStore.Instance.categories;
            foreach (var item in catStore)
            {
                Categories.Add(new CategoryAdapterViewModel(item));
            }
        }

        //private void LoadDummyList() 
        //{
        //    Categories.Add(new CategoryAdapterViewModel
        //        (
        //        new CategoryModel
        //        {
        //            Name = "Lebensmittel",
        //            Icon = new CategoryIconModel("Lebensmittel")
        //        }));

        //    Categories.Add(new CategoryAdapterViewModel
        //        (
        //        new CategoryModel
        //        {
        //            Name = "Auto",
        //            Icon = new CategoryIconModel("Auto")
        //        }));

        //    Categories.Add(new CategoryAdapterViewModel
        //        (
        //        new CategoryModel
        //        {
        //            Name = "Bücher",
        //            Icon = new CategoryIconModel("Buchhaltung")
        //        }));

        //    Categories.Add(new CategoryAdapterViewModel
        //        (
        //        new CategoryModel
        //        {
        //            Name = "Kommunikation",
        //            Icon = new CategoryIconModel("Mobilfunk")
        //        }));

        //    Categories.Add(new CategoryAdapterViewModel
        //        (
        //        new CategoryModel
        //        {
        //            Name = "Sparen",
        //            Icon = new CategoryIconModel("Sparbuch")
        //        }));

        //    Categories.Add(new CategoryAdapterViewModel
        //        (
        //        new CategoryModel
        //        {
        //            Name = "Einrichtung",
        //            Icon = new CategoryIconModel("Möbel")
        //        }));

        //    Categories.Add(new CategoryAdapterViewModel
        //        (
        //        new CategoryModel
        //        {
        //            Name = "Lebensmittel",
        //            Icon = new CategoryIconModel("Lebensmittel")
        //        }));

        //    Categories.Add(new CategoryAdapterViewModel
        //        (
        //        new CategoryModel
        //        {
        //            Name = "Auto",
        //            Icon = new CategoryIconModel("Auto")
        //        }));

        //    Categories.Add(new CategoryAdapterViewModel
        //        (
        //        new CategoryModel
        //        {
        //            Name = "Bücher",
        //            Icon = new CategoryIconModel("Buchhaltung")
        //        }));

        //    Categories.Add(new CategoryAdapterViewModel
        //        (
        //        new CategoryModel
        //        {
        //            Name = "Kommunikation",
        //            Icon = new CategoryIconModel("Mobilfunk")
        //        }));

        //    Categories.Add(new CategoryAdapterViewModel
        //        (
        //        new CategoryModel
        //        {
        //            Name = "Sparen",
        //            Icon = new CategoryIconModel("Sparbuch")
        //        }));

        //    Categories.Add(new CategoryAdapterViewModel
        //        (
        //        new CategoryModel
        //        {
        //            Name = "Einrichtung",
        //            Icon = new CategoryIconModel("Möbel")
        //        }));
        //}
    }
}
