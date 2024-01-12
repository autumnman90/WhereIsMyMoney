using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereIsMyMoney.MVVM.Models;

namespace WhereIsMyMoney.MVVM.ViewModels
{
    public class AnalyserModelAdapter : BaseViewModel
    {
        private AnalyserModel _analyserModel;

        public String CategoryName => _analyserModel.CategoryName;
        public Decimal AvarageValue => _analyserModel.AvarageValue;
        public CategoryIconModel Category => _analyserModel.Category;
        public AnalyserModelAdapter(AnalyserModel analyser)
        {
            _analyserModel = analyser;
        }
    }
}
