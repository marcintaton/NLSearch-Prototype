
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

            // language detection not very reliable on short texts which is a concern - causes 500 by the way
            var cld2LanguageDetector = await LanguageDetector.FromStoreAsync(Language.Any, Mosaik.Core.Version.Latest, "");
            var doc = new Document(input);
            cld2LanguageDetector.Process(doc);
            lastLang = doc.Language;


            Console.WriteLine("Language: " + doc.Language);
            var nlp = Pipeline.For(doc.Language);

            // -----------------------------------------------------------------
            // current algorithm to pull interesting words and phrases, using Catalyst patterns
            // perhaps should be replaced by custom algorithm to pull all n-grams manually - which would produce a lot of garbage, 
            // but pre model filtering would take care of that
            var singlePattern = new PatternSpotter(doc.Language, 0, tag: "singlePattern", captureTag: "singlePattern");
            singlePattern.NewPattern(
                "singlePattern",
                mp => mp.Add(
                    new PatternUnit(P.Single().WithPOS(PartOfSpeech.NOUN, PartOfSpeech.PROPN, PartOfSpeech.ADJ, PartOfSpeech.NUM, PartOfSpeech.X))
            ));
            nlp.Add(singlePattern);

            // especially here, by adding or removing ADJ from multiPattern
            // it either collects garbage or skips important stuff
            // as it is if a 3-gram covers a phrase, 2-grams from that phrase will not be generated 
            // so... far from perfect
            var multiPattern = new PatternSpotter(doc.Language, 0, tag: "multiPattern", captureTag: "multiPattern");
            multiPattern.NewPattern(
                "multiPattern",
                mp => mp.Add(
                    new PatternUnit(P.Multiple().WithPOS(PartOfSpeech.NOUN, PartOfSpeech.PROPN, PartOfSpeech.NUM, PartOfSpeech.X))
            ));
            nlp.Add(multiPattern);
            // -----------------------------------------------------------------


            nlp.ProcessSingle(doc);

            /// Pull the good shit out
            var poiValues = new List<string>();

            var entities = doc.SelectMany(span => span.GetEntities());

            foreach (var token in entities)
            {
                poiValues.Add(token.Value);
            }

            return poiValues.Distinct().ToList();
        }
    }


}
