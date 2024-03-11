using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

namespace FindingBeauty
{
    public class ProfanityFilter : MonoBehaviour
    {
        public TMP_Text textDisplay;
        public TMP_InputField inputText;

        public TextAsset textAssetBlockList;
        private string[] strBlockList;

        void Start()
        {
            strBlockList = textAssetBlockList.text.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            inputText.onValueChanged.AddListener(CensorProfanityInRealTime);
        }

        private void CensorProfanityInRealTime(string input)
        {
            inputText.text = ProfanityCheck(input);
        }

        string ProfanityCheck(string strToCheck)
        {
            foreach (var profanity in strBlockList)
            {
                string pattern = $"\\b{Regex.Escape(profanity)}\\b";
                Regex word = new Regex(pattern, RegexOptions.IgnoreCase);
                strToCheck = word.Replace(strToCheck, ""); // Replace profanity with an empty string
            }
            return strToCheck;
        }
    }
}
