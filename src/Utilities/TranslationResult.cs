using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLSearchWeb.src.Utilities
{
    public class BackTranslation
    {
        public string NormalizedText { get; set; }
        public string DisplayText { get; set; }
        public string NumExamples { get; set; }
        public string FrequencyCount { get; set; }
    }

    public class Translation
    {
        public string NormalizedTarget { get; set; }
        public string DisplayTarget { get; set; }
        public string PosTag { get; set; }
        public string Confidence { get; set; }
        public string PrefixWord { get; set; }
        public BackTranslation[] BackTranslations { get; set; }

    }

    public class TranslationResult
    {
        public string NormalizedSource { get; set; }
        public string DisplaySource { get; set; }
        public Translation[] translations { get; set; }
    }
}