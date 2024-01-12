using System;

namespace WhereIsMyMoney.MVVM.Models
{
    public class JournalsEntryModel
    {
        public DateTime Date { get; set; }
        public Decimal Value { get; set; }
        public string Description { get; set; }
        public CategoryModel Category { get; set; }
        public string Name { get; set; }

    }
}
