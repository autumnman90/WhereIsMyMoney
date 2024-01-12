using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereIsMyMoney.MVVM.Models;

namespace WhereIsMyMoney.MVVM.ViewModels
{
    public class StatCubeAdapter : BaseViewModel
    {
        private StatCubeModel _model;

        public int IncomeBarHeight => _model.IncomeBarHeight;
        public int SpendingsBarHeight => _model.SpendingsBarHeight;
        public DateTime Range => _model.Range;
        public decimal IncomeInRange => _model.IncomeInRange;
        public decimal SpendingsInRange => _model.SpendingsInRange;

        public StatCubeAdapter(StatCubeModel statCube)
        {
            _model = statCube;
        }
    }
}
