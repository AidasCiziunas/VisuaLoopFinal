using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace SyedAli.Main.LanguageModule
{
    internal class EnglishLanguageHandler : MonoBehaviour
    {
        private Dictionary<string, string> _langDict = new Dictionary<string, string>();

        private void OnEnable()
        {
            LanguageModuleSignals.Get<EnglishLanguageSignal.Setup>().AddListener(OnSetup);
            LanguageModuleSignals.Get<EnglishLanguageSignal.GetSentence>().AddListener(OnGetSentence);
        }

        private void OnDisable()
        {
            LanguageModuleSignals.Get<EnglishLanguageSignal.Setup>().RemoveListener(OnSetup);
            LanguageModuleSignals.Get<EnglishLanguageSignal.GetSentence>().RemoveListener(OnGetSentence);
        }

        private async Task<(Exception, bool)> OnSetup()
        {
            SetupLanguageData();
            return (null, true);
        }

        private void SetupLanguageData()
        {
            _langDict.Add(LanguageModuleHelper.ChangeToLowerString(SentenceToken.None), "None");
            _langDict.Add(LanguageModuleHelper.ChangeToLowerString(SentenceToken.Products), "Products");
            _langDict.Add(LanguageModuleHelper.ChangeToLowerString(SentenceToken.VStyleLoop), "V Style Loop");
            _langDict.Add(LanguageModuleHelper.ChangeToLowerString(SentenceToken.UStyleLoop), "U Style Loop");
            _langDict.Add(LanguageModuleHelper.ChangeToLowerString(SentenceToken.TriLoop), "Tri-Loop");
            _langDict.Add(LanguageModuleHelper.ChangeToLowerString(SentenceToken.TriFlexLoop), "Tri-Flex Loop");
            _langDict.Add(LanguageModuleHelper.ChangeToLowerString(SentenceToken.ThreeD), "3d");
            _langDict.Add(LanguageModuleHelper.ChangeToLowerString(SentenceToken.Move), "Move");
            _langDict.Add(LanguageModuleHelper.ChangeToLowerString(SentenceToken.Zoom), "Zoom");
            _langDict.Add(LanguageModuleHelper.ChangeToLowerString(SentenceToken.Info), "Info");
            _langDict.Add(LanguageModuleHelper.ChangeToLowerString(SentenceToken.Y), "Y");
            _langDict.Add(LanguageModuleHelper.ChangeToLowerString(SentenceToken.Z), "Z");
            _langDict.Add(LanguageModuleHelper.ChangeToLowerString(SentenceToken.X), "X");
            _langDict.Add(LanguageModuleHelper.ChangeToLowerString(SentenceToken.GoodMorning), "Good Morning");
            _langDict.Add(LanguageModuleHelper.ChangeToLowerString(SentenceToken.GoodAfternoon), "Good Afternoon");
            _langDict.Add(LanguageModuleHelper.ChangeToLowerString(SentenceToken.GoodEvening), "Good Evening");
            _langDict.Add(LanguageModuleHelper.ChangeToLowerString(SentenceToken.LoremIpsum1), "Lorem Ipsum 1");
            _langDict.Add(LanguageModuleHelper.ChangeToLowerString(SentenceToken.MoveUpDown), "Move Up/Down");
            _langDict.Add(LanguageModuleHelper.ChangeToLowerString(SentenceToken.MoveLeftRight), "Move Left/Right");
            _langDict.Add(LanguageModuleHelper.ChangeToLowerString(SentenceToken.Rotate), "Rotate");
            _langDict.Add(LanguageModuleHelper.ChangeToLowerString(SentenceToken.Detail), "Detail");
            _langDict.Add(LanguageModuleHelper.ChangeToLowerString(SentenceToken.Brochures), "Brochures");
            _langDict.Add(LanguageModuleHelper.ChangeToLowerString(SentenceToken.BIMContent), "BIM Content");
            _langDict.Add(LanguageModuleHelper.ChangeToLowerString(SentenceToken.Submittals), "Submittals");
            _langDict.Add(LanguageModuleHelper.ChangeToLowerString(SentenceToken.Reset), "Reset");
        }

        private string OnGetSentence(string token)
        {
            if (_langDict.ContainsKey(token))
            {
                return _langDict[token];
            }
            else
            {
                Debug.LogError("English Language Value Not Found In Dictionary For token : " + token);
                return token;
            }
        }
    }
}
