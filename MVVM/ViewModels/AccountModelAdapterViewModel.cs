using System;
using System.Collections.Generic;
using WhereIsMyMoney.MVVM.Models;

namespace WhereIsMyMoney.MVVM.ViewModels
{
    public class AccountModelAdapterViewModel : BaseViewModel
    {
        private AccountModel _accountModel;

        public string AccountName => _accountModel.AccountName;
        public Decimal Value => _accountModel.Value;
        public List<JournalsEntryModel> Journals => _accountModel.Journals;

        public AccountModelAdapterViewModel(AccountModel accountModel)
        {
            _accountModel = accountModel;
        }

        public void UpdateAccountModel(AccountModel updatedAccount)
        {
            _accountModel = updatedAccount;
        }

        public AccountModel getAccountModel()
        {
            return _accountModel;
        }
    }
}
