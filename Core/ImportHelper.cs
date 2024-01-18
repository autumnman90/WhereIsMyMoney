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
        public static ImportHelper Instance { get { return instance; } }

        private int[] headerPositions = new int[4];

        // 0-> Date, 1-> Begünstigter, 2-> VWZ, 3-> Betrag
        private Dictionary<String, int> headerKeyPairs = new Dictionary<String, int>
        {
            {"buchungsdatum",0 },
            {"beguenstigter",1 },
            {"begünstigter",1 },
            {"zahlungspflichtiger",1 },
            {"verwendungszweck",2 },
            {"betrag",3 },
        };

        private List<string> headerKeyWords = new List<string>
        {
            "buchungsdatum",
            "beguenstigter",
            "begünstigter",
            "zahlungspflichtiger",
            "verwendungszweck",
            "betrag",
        };

        public ImportHelper()
        {

        }

        public int[] GetHeaderPositions()
        {
            return headerPositions;
        }

        public int[] GetHeaderPositions(List<String> headers)
        {
            Dictionary<String, int> foundKeywords = new Dictionary<string, int>();
            foreach (var keyword in headerKeyWords)
            {
                for (int i = 0; i < headers.Count; i++)
                {
                    if (headers[i].IndexOf(keyword, StringComparison.OrdinalIgnoreCase)>=0)
                    {
                        if (!foundKeywords.ContainsKey(keyword))
                        {
                            foundKeywords.Add(keyword, i);
                        }
                    }
                }
            }

            if (foundKeywords.ContainsKey("beguenstigter") & foundKeywords.ContainsKey("begünstigter") & foundKeywords.ContainsKey("zahlungspflichtiger"))
            {
                foreach (var item in foundKeywords)
                {
                    if (item.Key!="begünstigter"|item.Key!="zahlungspflichtiger")
                    {
                        headerPositions[headerKeyPairs[item.Key]] = item.Value;
                    }
                }
            }
            else if (!foundKeywords.ContainsKey("beguenstigter") & foundKeywords.ContainsKey("begünstigter") & foundKeywords.ContainsKey("zahlungspflichtiger"))
            {
                foreach (var item in foundKeywords)
                {
                    if (item.Key != "zahlungspflichtiger")
                    {
                        headerPositions[headerKeyPairs[item.Key]] = item.Value;
                    }
                }
            }
            else if (foundKeywords.ContainsKey("beguenstigter") & !foundKeywords.ContainsKey("begünstigter") & foundKeywords.ContainsKey("zahlungspflichtiger"))
            {
                foreach (var item in foundKeywords)
                {
                    if (item.Key != "zahlungspflichtiger")
                    {
                        headerPositions[headerKeyPairs[item.Key]] = item.Value;
                    }
                }
            }
            else if (foundKeywords.ContainsKey("beguenstigter") & foundKeywords.ContainsKey("begünstigter") & !foundKeywords.ContainsKey("zahlungspflichtiger"))
            {
                foreach (var item in foundKeywords)
                {
                    if (item.Key != "begünstigter")
                    {
                        headerPositions[headerKeyPairs[item.Key]] = item.Value;
                    }
                }
            }

            return headerPositions;
        }
    }
}
