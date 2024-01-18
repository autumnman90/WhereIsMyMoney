using System;
using WhereIsMyMoney.MVVM.Models;

namespace WhereIsMyMoney.MVVM.ViewModels
{
    public class JournalEntryAdapterViewModel : BaseViewModel
    {
        private JournalsEntryModel _journalsEntry;

        public string Name => _journalsEntry.Name;
        public Decimal Value => _journalsEntry.Value;
        public string Description => _journalsEntry.Description;
        public CategoryAdapterViewModel Category => new CategoryAdapterViewModel(_journalsEntry.Category);
        public DateTime Date => _journalsEntry.Date;

        public JournalEntryAdapterViewModel(JournalsEntryModel journalsEntry)
        {
            _journalsEntry = journalsEntry;
        }

        public JournalsEntryModel GetModel()
        {
            return _journalsEntry;
        }
    }
}
