using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereIsMyMoney.MVVM.Models;
using WhereIsMyMoney.Stores;

namespace WhereIsMyMoney.Core
{
    public static class CategoryGenerator
    {
        //ICH LIEBE INTELLISENSE!!! WARUM??? WEIL DAS EINE LIST<STRING> WAR UND DER WECHSEL AUFS DICTIONARY MIT EINPAAR MAL TAB GING!!!!!
        private static Dictionary<String, String> CategoryKeyPaires = new Dictionary<string, string>
        {
            { "amazon","Einkäufe" }
            ,{ "lidl", "Lebensmittel" }
            ,{ "rewe", "Lebensmittel" }
            ,{ "aldi", "Lebensmittel" }
            ,{ "toom", "Einrichtung" }
            ,{ "spotify", "Abos" }
            ,{ "mcdonald", "Essen" }
            ,{ "burgerking", "Essen" }
            ,{ "expert", "Einkäufe" }
            ,{ "sony", "Abos" }
            ,{ "playstation", "Abos" }
            ,{ "xbox", "Abos" }
            ,{ "microsoft", "Abos" }    
            ,{ "netto", "Lebensmittel" }                            
            ,{ "edeka", "Lebensmittel" }
            ,{ "deichmann", "Einkäufe" }
            ,{ "kaninchenkiste", "Lebensmittel" }
            ,{ "aral", "Auto" }
            ,{ "imbiss", "Essen" }
            ,{ "restaurant", "Essen" }
            ,{ "dienstleistungen", "Sontiges" }
            ,{ "audible", "Abos" }
            ,{ "telekom", "Telekommunikation" }
            ,{ "vodafone", "Telekommunikation" }
            ,{ "e.on", "Wohnen" }
            ,{ "lohn", "Einkommen" }
            ,{ "gehalt", "Einkommen" }
            ,{ "1+1", "Telekommunikation" }
            ,{ "subway", "Essen" }
            ,{ "e-center", "Lebensmittel" }
            ,{ "kaufland", "Lebensmittel" }
            ,{ "np-markt", "Lebensmittel" }
            ,{ "disneyplus", "Abos" }
            ,{ "miete", "Wohnen" }
            ,{ "netflix", "Abos" }
            ,{ "prime", "Abos" }
            ,{ "strom", "Wohnen" }
            ,{ "einkauf", "Einkäufe" }
        };

        public static CategoryModel GenerateCategory(string input)
        {
            List<String> keywords = new List<String>();
            CategoryModel categoryModel;
            foreach (var item in CategoryKeyPaires)
            {
                keywords.Add(item.Key);
            }

            List<String> foundKeywords = keywords
                .Where(keyword => input.IndexOf(keyword, StringComparison.OrdinalIgnoreCase)>=0)
                .ToList();
            

            if (foundKeywords.Count>0)
            {
                categoryModel = CategoryStore.Instance.categories
                    [CategoryStore.Instance.categories.FindIndex
                    (obj => obj.Name == CategoryKeyPaires[foundKeywords[0]])];
            }
            else
            {
                categoryModel = CategoryStore.Instance.categories[CategoryStore.Instance.categories.FindIndex(obj => obj.Name == "Sonstiges")];
            }

            return categoryModel;
        }
    }
}
