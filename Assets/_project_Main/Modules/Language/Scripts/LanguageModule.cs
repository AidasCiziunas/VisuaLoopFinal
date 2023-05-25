using System;
using System.Threading.Tasks;
using UnityEngine;

namespace SyedAli.Main.LanguageModule
{
    public class LanguageModule : MonoBehaviour
    {
        [SerializeField] GameLanguage _defaultLang;

        private void OnEnable()
        {
            LanguageModuleSignals.Get<LanguageSignal.Setup>().AddListener(OnSetup);
            LanguageModuleSignals.Get<LanguageSignal.GetRelevantLangSent>().AddListener(OnGetRelevantLanguageSentence);
            LanguageModuleSignals.Get<LanguageSignal.GetDefaultLang>().AddListener(OnGetDefaultLanguage);
        }

        private void OnDisable()
        {
            LanguageModuleSignals.Get<LanguageSignal.Setup>().RemoveListener(OnSetup);
            LanguageModuleSignals.Get<LanguageSignal.GetRelevantLangSent>().RemoveListener(OnGetRelevantLanguageSentence);
            LanguageModuleSignals.Get<LanguageSignal.GetDefaultLang>().RemoveListener(OnGetDefaultLanguage);
        }

        private async Task<(Exception, bool)> OnSetup()
        {
            await LanguageModuleSignals.Get<EnglishLanguageSignal.Setup>().Dispatch();

            Debug.Log("LanguageModule Initialization");
            return (null, true);
        }

        private string OnGetRelevantLanguageSentence(SentenceToken sentenceToken)
        {
            switch (_defaultLang)
            {
                case GameLanguage.English:
                    return LanguageModuleSignals.Get<EnglishLanguageSignal.GetSentence>().Dispatch(sentenceToken.ToString().ToLower());
                default:
                    Debug.LogError("Unable To Get String For Specified Language");
                    return sentenceToken.ToString();
            }
        }

        private GameLanguage OnGetDefaultLanguage()
        {
            return _defaultLang;
        }
    }
}
