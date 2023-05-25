using System;
using System.Threading.Tasks;

namespace SyedAli.Main
{
    public class LanguageSignal
    {
        public class Setup : LanguageModuleASignal<Task<(Exception, bool)>> { }
        public class GetRelevantLangSent : LanguageModuleASignal<SentenceToken, string> { }
        public class GetDefaultLang : LanguageModuleASignal<GameLanguage> { }
    }

    public class EnglishLanguageSignal
    {
        public class Setup : LanguageModuleASignal<Task<(Exception, bool)>> { }
        public class GetSentence : LanguageModuleASignal<string, string> { }
    }
}
