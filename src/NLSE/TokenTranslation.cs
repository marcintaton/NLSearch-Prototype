
using System.Runtime.InteropServices;
using NLSearchWeb.src.Controllers;

namespace NLSearchWeb.src.NLSE
{
    public class TokenTranslation
    {
        public string _origin { get; private set; }

        public List<string> wordList { get; private set; }

        public TokenTranslation(string origin)
        {
            _origin = origin;
        }

        public async Task Translate()
        {
            wordList = await TranslationsController.GetTranslations(_origin);
            if (wordList.Count == 0)
                wordList.Add(_origin);
        }
    }
}