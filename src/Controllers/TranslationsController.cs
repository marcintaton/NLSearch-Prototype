using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using NLSearchWeb.src.Utilities;

namespace NLSearchWeb.src.Controllers
{
    internal class TranslationQueryText
    {
        public string Text { get; set; }
    }

    public class TranslationsController
    {
        static string _address = "https://api.cognitive.microsofttranslator.com/dictionary/lookup?api-version=3.0&from=pl&to=en";

        public static async Task<List<string>> GetTranslations(string query)
        {
            var json = JsonConvert.SerializeObject(new TranslationQueryText { Text = query });
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            data.Headers.Add("Ocp-Apim-Subscription-Key", "19f4eb52d69d47e0882e73a352c2d682");
            data.Headers.Add("Ocp-Apim-Subscription-Region", "westeurope");

            var client = new HttpClient();

            HttpResponseMessage response = await client.PostAsync(_address, data);

            var result = await response.Content.ReadAsStringAsync();
            var translationResult = JsonConvert.DeserializeObject<TranslationResult>(result);

            var translations = new List<string>();

            foreach (var t in translationResult.translations)
            {
                translations.Add(t.NormalizedTarget);
            }

            return translations;
        }
    }
}