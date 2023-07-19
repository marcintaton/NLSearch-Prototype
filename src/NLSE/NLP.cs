
using Catalyst;
using Mosaik.Core;

namespace NLSearchWeb.src.NLSE
{
    public class NLP
    {
        public NLP()
        {
            Catalyst.Models.Polish.Register();
            Storage.Current = new DiskStorage("catalyst-models");
        }

        public List<string> GetPointsOfInterest(string input)
        {
            var nlp = Pipeline.For(Language.Polish);

            var doc = new Document(input, Language.Polish);

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

            return pointsOfInterest;
        }
    }


}
