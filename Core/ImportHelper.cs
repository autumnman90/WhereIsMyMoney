using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereIsMyMoney.Core
{
    public class ImportHelper
    {
        private static readonly ImportHelper instance = new ImportHelper();
        public static ImportHelper Instance {  get { return instance; } }

        private int[] headerPositions;

        public ImportHelper()
        {
            
        }
    }
}
