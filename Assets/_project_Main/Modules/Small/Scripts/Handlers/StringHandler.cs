using System;
using System.Threading.Tasks;
using UnityEngine;

namespace SyedAli.Main.SmallModule
{
    internal class StringHandler : MonoBehaviour
    {
        private void OnEnable()
        {
            SmallModuleSignals.Get<StringSignal.Setup>().AddListener(OnSetup);
            SmallModuleSignals.Get<StringSignal.PutCharacterBwString>().AddListener(OnPutCharacterBwString);
            SmallModuleSignals.Get<StringSignal.TrimString>().AddListener(OnTrimString);
        }

        private void OnDisable()
        {
            SmallModuleSignals.Get<StringSignal.Setup>().RemoveListener(OnSetup);
            SmallModuleSignals.Get<StringSignal.PutCharacterBwString>().RemoveListener(OnPutCharacterBwString);
            SmallModuleSignals.Get<StringSignal.TrimString>().RemoveListener(OnTrimString);
        }


        private async Task<(Exception, bool)> OnSetup()
        {
            return (null, true);
        }

        public static string OnPutCharacterBwString(string charString, int afterNoOfChars, string character)
        {
            if (charString.Length > afterNoOfChars && !charString.Contains(character))
            {
                return charString.Substring(0, afterNoOfChars) + character + charString.Substring(afterNoOfChars);
            }
            else
            {
                return charString;
            }
        }

        private string OnTrimString(string str, int charLimit)
        {
            if (str.Length > 9)
                return str.Substring(0, charLimit) + "...";
            else
                return str;
        }
    }
}
