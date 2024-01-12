using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereIsMyMoney.MVVM.Models
{
    public class StatCubeModel
    {
        public int IncomeBarHeight;
        public int SpendingsBarHeight;
        public decimal IncomeInRange;
        public decimal SpendingsInRange;
        public DateTime Range;

        public StatCubeModel(int incomeBarHeight, int spendingsBarHeight, DateTime range, decimal incomeInRange, decimal spendingsInRange) 
        {
            IncomeBarHeight = incomeBarHeight;
            SpendingsBarHeight = spendingsBarHeight;
            Range = range;
            IncomeInRange = incomeInRange;
            SpendingsInRange = spendingsInRange;
        }
    }
}
