
using Catalyst;
using Catalyst.Models;
using Mosaik.Core;

namespace NLSearchWeb.src.NLSE
{
    public class NLP
    {
        public Language lastLang;

        public NLP()
        {
            Catalyst.Models.Polish.Register();
            Catalyst.Models.English.Register();
            Storage.Current = new DiskStorage("catalyst-models");
        }

        public async Task<List<string>> GetPointsOfInterest(string input)
        {
            // not very reliable on short texts which is a concern
            var cld2LanguageDetector = await LanguageDetector.FromStoreAsync(Language.Any, Mosaik.Core.Version.Latest, "");
            var doc = new Document(input);
            cld2LanguageDetector.Process(doc);

            Console.WriteLine(doc.Language);
            var nlp = Pipeline.For(doc.Language);

            nlp.ProcessSingle(doc);

            var pointsOfInterest = new List<string>();

            foreach (var sentence in doc)
            {
                foreach (var token in sentence)
                {
                    // Console.WriteLine(token.Value + " " + token.POS.ToString());
                    if (token.POS.ToString() == "NOUN"
                        || token.POS.ToString() == "PROPN"
                        || token.POS.ToString() == "ADJ"
                        || token.POS.ToString() == "X")
                        pointsOfInterest.Add(token.Value);
                }
            }

            lastLang = doc.Language;

            return pointsOfInterest;
        }
    }


}
