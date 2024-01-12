using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WhereIsMyMoney.MVVM.Models
{
    public class AccountModel
    {
        public string AccountName;
        public Decimal Value;
        public List<JournalsEntryModel> Journals;

        [JsonConstructor]
        public AccountModel(string acountName, List<JournalsEntryModel> journals) 
        {
            AccountName = acountName;
            Value = (journals.Count>0)? CalcValue(journals) : 0.0m;
            Journals = journals;
        }

        public AccountModel() 
        {
            
        }

        private Decimal CalcValue(List<JournalsEntryModel> journals)
        {
            Decimal calculatedValue = 0.0m;
            foreach (var entry in journals)
            {
                calculatedValue += entry.Value;
            }
            return calculatedValue;
        }
    }
}
