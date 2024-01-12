using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereIsMyMoney.Stores;

namespace WhereIsMyMoney.MVVM.Models
{
    public class CategoryIconModel
    {
        public string Name { get; }
        public string Path { get; }

        [JsonConstructor]
        public CategoryIconModel(string name, string path)
        {
            Name = name;
            Path = path;
        }

        public CategoryIconModel(string category)
        {
            IconStore tempStore = new IconStore();
            List<CategoryIconModel> categoryIconModels = tempStore.GetList();

            foreach (var categoryIconModel in categoryIconModels)
            {
                if (categoryIconModel.Name == category)
                {
                    Name = categoryIconModel.Name;
                    Path = categoryIconModel.Path;
                    return;
                }
            }
            Name = category;
            Path = "/Resources/Images/mehr-ico.png";
        }
    }
}
