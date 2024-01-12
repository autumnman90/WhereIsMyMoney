using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereIsMyMoney.MVVM.Models;
using WhereIsMyMoney.MVVM.ViewModels;
using WhereIsMyMoney.Stores;

namespace WhereIsMyMoney.Core
{
    public class Analytics
    {
        private AccountModel account;
        private List<JournalsEntryModel> journal;
        private List <CategoryModel> categories;
        private decimal incomeTotal = 0.0m;
        private decimal spendingTotal = 0.0m;
        private decimal incomeMonth = 0.0m;
        private decimal spendingMonth = 0.0m;
        private List<AnalyserModel> spendingsByCategory;

        public Analytics(AccountModel account)
        {
            this.account = account;
            journal = new List<JournalsEntryModel>(account.Journals);
            categories = CategoryStore.Instance.categories;
            spendingsByCategory = new List<AnalyserModel>();
        }

        private void CalcValues()
        {
            DateTime now = DateTime.Now;
            foreach (var item in journal)
            {
                if (item.Value<0)
                {
                    spendingTotal += item.Value;
                    if (item.Date.Month == now.Month)
                    {
                        spendingMonth += item.Value;
                    }
                }
                else
                {
                    incomeTotal += item.Value;
                    if (item.Date.Month==now.Month)
                    {
                        incomeMonth += item.Value;
                    }
                }
            }
        }

        /// <summary>
        /// Returns the income and spendings in total and monthly in an decimal[]. The Values are given in the following order:
        /// [0] => incomeTotal,
        /// [1] => spendingTotal,
        /// [2] => incomeMonthly,
        /// [3] => spendingMonthly
        /// </summary>
        /// <returns>decimal[]</returns>
        public Decimal[] GetValues()
        {
            CalcValues();
            return new decimal[] { incomeTotal, spendingTotal, incomeMonth, spendingMonth };
        }

        private void AnalyseSpendingsByCategory()
        {
            var hitCategoryValues = new Dictionary<string, decimal>();
            var hitCategories = new Dictionary<string, decimal>();
            var iconToCategory = new Dictionary<string, CategoryIconModel>();
            foreach (var item in journal)
            {
                string temp = item.Category.Name;
                if (hitCategoryValues.ContainsKey(temp))
                {
                    if (item.Value<0m)
                    {
                        hitCategoryValues[temp] += item.Value;
                        hitCategories[temp] += 1m;
                    }
                }
                else
                {
                    if (item.Value<0m)
                    {
                        hitCategoryValues.Add(temp, item.Value);
                        hitCategories.Add(temp, 1m);
                        iconToCategory.Add(temp, item.Category.Icon);
                    }
                }
            }
            List<AnalyserModel> list = new List<AnalyserModel>();
            foreach (var item in hitCategoryValues)
            {
                var avarageValue = item.Value / hitCategories[item.Key];
                var name = item.Key.ToString();
                list.Add(new AnalyserModel{ CategoryName = name,AvarageValue = avarageValue, Category = iconToCategory[name] });
            }
            list.Sort((a, b) => b.AvarageValue.CompareTo(a.AvarageValue));
            spendingsByCategory = list;
        }

        /// <summary>
        /// Returns a List of AnalyserModel wich includes all the avarage spendings for all categories sorted descending
        /// </summary>
        /// <returns>List<AnalyserModel></returns>
        public List<AnalyserModel> GetSpendingsByCategory() 
        {
            AnalyseSpendingsByCategory();
            return spendingsByCategory; 
        }

        /// <summary>
        /// Returns a List of JournalsEntryModel wich includes all Bookings for the last 3 months
        /// </summary>
        /// <returns>List<JournalsEntryModel></returns>
        public List<JournalsEntryModel> GetLastBookings()
        {
            List<JournalsEntryModel> result = new List<JournalsEntryModel>();
            DateTime now = DateTime.Now;
            foreach (var item in journal)
            {
                if (item.Date.Month==now.Month|item.Date.Month==now.Month-1|item.Date.Month==now.Month-2)
                {
                    result.Add(item);
                }
            }
            result.Sort((a,b) => b.Date.CompareTo(a.Date));
            return result;
        }

        public ObservableCollection<StatCubeAdapter> BalanceYear(int maxBarHeight)
        {
            //Set variables
            var now = DateTime.Now;
            var then = now.AddMonths(-11);
            var incomeDictionary = new Dictionary<string, decimal>();
            var spendingsDictionary = new Dictionary<string, decimal>();
            var result = new ObservableCollection<StatCubeAdapter>();
            string format = "MMMM-yy";
            //Get all values by Range in Range(means between now and then)
            foreach (var item in journal)
            {
                if (item.Date.Year==now.Year||(item.Date.Year==then.Year&item.Date.Month>=then.Month))
                {
                    if (item.Value>=0m)
                    {
                        if (incomeDictionary.ContainsKey(item.Date.ToString(format)))
                        {
                            incomeDictionary[item.Date.ToString(format)] += item.Value;
                        }
                        else
                        {
                            incomeDictionary.Add(item.Date.ToString(format), item.Value);
                        }
                    }
                    else
                    {
                        if (spendingsDictionary.ContainsKey(item.Date.ToString(format)))
                        {
                            spendingsDictionary[item.Date.ToString(format)] += item.Value;
                        }
                        else
                        {
                            spendingsDictionary.Add(item.Date.ToString(format), item.Value );
                        }
                    }
                }
            }
            //Get highest value
            var highestValue = 0m;
            foreach (var item in incomeDictionary)
            {
                if (item.Value>highestValue)
                {
                    highestValue = item.Value;
                }
            }
            foreach (var item in spendingsDictionary)
            {
                if ((item.Value*-1m)>highestValue)
                {
                    highestValue = item.Value*-1;
                }
            }
            //Calculate pixelratio(how much money will be displayed per pixel)
            var pixelRatio = highestValue / maxBarHeight;
            foreach (var item in incomeDictionary)
            {
                var inBarHeight = item.Value / pixelRatio;
                var spenBarHeight = spendingsDictionary[item.Key] / pixelRatio;
                if (inBarHeight<0)
                {
                    inBarHeight = inBarHeight * -1;
                }
                if (spenBarHeight<0)
                {
                    spenBarHeight = spenBarHeight * -1;
                }
                result.Add(new StatCubeAdapter(new StatCubeModel((int)inBarHeight, (int)spenBarHeight, DateTime.Parse(item.Key), item.Value, spendingsDictionary[item.Key])));
            }
            return result = new ObservableCollection<StatCubeAdapter>(result.OrderBy(a => a.Range));
        }
    }
}
