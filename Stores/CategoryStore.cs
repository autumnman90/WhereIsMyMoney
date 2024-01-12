using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using WhereIsMyMoney.MVVM.Models;

namespace WhereIsMyMoney.Stores
{
    /// <summary>
    /// Store zum Anzeigen der Kategorien
    /// </summary>
    public class CategoryStore
    {
        private static readonly CategoryStore instance = new CategoryStore();
        public static CategoryStore Instance
        {
            get { return instance; }
        }

        private string categoryFolderPath;
        private string categoryFilePath; 
        public List<CategoryModel> categories;

        public CategoryStore()
        {
            categoryFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"WhereIsMyMoney", "Categories");
            categoryFilePath = Path.Combine(categoryFolderPath, "Categories.json");
            categories = new List<CategoryModel>();
            LoadCategories();
        }

        private void LoadCategories()
        {
            if (!Directory.Exists(categoryFolderPath))
            {
                Directory.CreateDirectory(categoryFolderPath);
            }

            if (File.Exists(Path.Combine(categoryFolderPath, categoryFilePath)))
            {
                string jsonContent = File.ReadAllText(Path.Combine(categoryFolderPath, categoryFilePath));
                categories = JsonConvert.DeserializeObject<List<CategoryModel>>(jsonContent);
            }
            else
            {
                categories = new List<CategoryModel>
                {
                    new CategoryModel("Auto", new CategoryIconModel("Auto")),
                    new CategoryModel("Bildung", new CategoryIconModel("Bildung")),
                    new CategoryModel("Buchhaltung", new CategoryIconModel("Buchhaltung")),
                    new CategoryModel("Kleidung", new CategoryIconModel("Garderobe")),
                    new CategoryModel("Einkommen", new CategoryIconModel("Gehalt")),
                    new CategoryModel("Sparen", new CategoryIconModel("Gelddose")),
                    new CategoryModel("Wohnen", new CategoryIconModel("Haus")),
                    new CategoryModel("Lebensmittel", new CategoryIconModel("Lebensmittel")),
                    new CategoryModel("Einrichtung", new CategoryIconModel("Möbel")),
                    new CategoryModel("Telekommunikation", new CategoryIconModel("Mobilfunk")),
                    new CategoryModel("Essen", new CategoryIconModel("Restaurant")),
                    new CategoryModel("Garten", new CategoryIconModel("Spaten")),
                    new CategoryModel("Freizeit", new CategoryIconModel("Ticket")),
                    new CategoryModel("Urlaub", new CategoryIconModel("Urlaub")),
                    new CategoryModel("Einkäufe", new CategoryIconModel("Warenkorb")),
                    new CategoryModel("Sonstiges", new CategoryIconModel("Mehr")),
                    new CategoryModel("Abos", new CategoryIconModel("Fixkosten")),
                    new CategoryModel("Zahlungen", new CategoryIconModel("Fixkosten")),
                    new CategoryModel("Gebühren", new CategoryIconModel("Fixkosten")),
                };
                SaveToFile();
            }
        }

        public void SaveToFile()
        {
            if (File.Exists(Path.Combine(categoryFolderPath, categoryFilePath)))
            {
                string jsonData = JsonConvert.SerializeObject(categories, Formatting.Indented);
                File.WriteAllText(Path.Combine(categoryFolderPath, categoryFilePath), jsonData);
            }
            else
            {
                string jsonData = JsonConvert.SerializeObject(categories, Formatting.Indented);
                File.WriteAllText(Path.Combine(categoryFolderPath, categoryFilePath), jsonData);
            }
        }

        public void AddCategory(CategoryModel category)
        {
            categories.Add(category);
            SaveToFile();
        }

        public void RemoveCategory(CategoryModel category)
        {
            categories.RemoveAt(categories.FindIndex(o=> o.Name == category.Name&o.Icon == category.Icon));
            SaveToFile();
        }
    }
}
