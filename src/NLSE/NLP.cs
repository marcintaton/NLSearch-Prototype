
using Catalyst;
using Catalyst.Models;
using Mosaik.Core;
using P = Catalyst.PatternUnitPrototype;

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
            /// Process the text

            // not very reliable on short texts which is a concern
            var cld2LanguageDetector = await LanguageDetector.FromStoreAsync(Language.Any, Mosaik.Core.Version.Latest, "");
            var doc = new Document(input);
            cld2LanguageDetector.Process(doc);
            lastLang = doc.Language;


            Console.WriteLine("Language: " + doc.Language);
            var nlp = Pipeline.For(doc.Language);
            // nlp.Add(await AveragePerceptronEntityRecognizer.FromStoreAsync(language: doc.Language, version: Mosaik.Core.Version.Latest, tag: "WikiNER"));

            var singlePattern = new PatternSpotter(doc.Language, 0, tag: "singlePattern", captureTag: "singlePattern");
            singlePattern.NewPattern(
                "singlePattern",
                mp => mp.Add(
                    new PatternUnit(P.SingleOptional().WithPOS(PartOfSpeech.NOUN, PartOfSpeech.PROPN, PartOfSpeech.ADJ, PartOfSpeech.NUM, PartOfSpeech.X))
            ));
            nlp.Add(singlePattern);

            var multiPattern = new PatternSpotter(doc.Language, 0, tag: "multiPattern", captureTag: "multiPattern");
            multiPattern.NewPattern(
                "multiPattern",
                mp => mp.Add(
                    new PatternUnit(P.MultipleOptional().WithPOS(PartOfSpeech.NOUN, PartOfSpeech.PROPN, PartOfSpeech.NUM, PartOfSpeech.X))
            ));
            nlp.Add(multiPattern);

            nlp.ProcessSingle(doc);

            /// Pull the good shit out


            var poiValues = new List<string>();

            var entities = doc.SelectMany(span => span.GetEntities());

            // Pass response
            foreach (var token in entities)
            {
                poiValues.Add(token.Value);
            }

            return poiValues.Distinct().ToList();
        }
    }


}
