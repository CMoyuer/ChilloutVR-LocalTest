using System;
using System.Collections;
using System.Collections.Generic;
using ABI.CCK.Scripts.Translation;
using UnityEditor;
using UnityEngine;

public static class CCKLocalizationProvider
{
    
    private struct LocalizationDriver
    {
        public string LanguageName;
        public Dictionary<string, string> LanguageDefinition;
    }
    
    
    private static readonly List<LocalizationDriver> KnownLanguages = new List<LocalizationDriver>()
    {
        new LocalizationDriver(){ LanguageName = "English", LanguageDefinition = English.Localization },
        new LocalizationDriver(){ LanguageName = "Deutsch", LanguageDefinition = German.Localization },
        new LocalizationDriver(){ LanguageName = "Nederlands", LanguageDefinition = Dutch.Localization },
        new LocalizationDriver(){ LanguageName = "日本語", LanguageDefinition = Japanese.Localization },
        new LocalizationDriver(){ LanguageName = "한국어", LanguageDefinition = Korean.Localization },
        new LocalizationDriver(){ LanguageName = "中文（简体）", LanguageDefinition = Chinese.Localization },
        new LocalizationDriver(){ LanguageName = "Français", LanguageDefinition = French.Localization },
        new LocalizationDriver(){ LanguageName = "русский", LanguageDefinition = Russian.Localization }
    };

    public static List<string> GetKnownLanguages()
    {
        List<string> knownLanguages = new List<string>();

        foreach (LocalizationDriver driver in KnownLanguages)
        {
            knownLanguages.Add(driver.LanguageName);
        }

        return knownLanguages;
    }

    
    public static string GetLocalizedText(string input)
    {
        try
        {
            string output;
            #if  UNITY_EDITOR
                KnownLanguages.Find(match => match.LanguageName == EditorPrefs.GetString("ABI_CCKLocals", "English")).LanguageDefinition.TryGetValue(input, out output);
            #else
                KnownLanguages.Find(match => match.LanguageName == "English").LanguageDefinition.TryGetValue(input, out output);
            #endif

            if (string.IsNullOrEmpty(output))
            {
                KnownLanguages.Find(match => match.LanguageName == "English").LanguageDefinition.TryGetValue(input, out output);
                Debug.LogWarning($"[CCK] [LocalizationProvider] Unable to use localized string. The defined language is likely not or not fully supported.");
            }
            
            return output;
        }
        catch (Exception e)
        {
            string output;
            KnownLanguages.Find(match => match.LanguageName == "English").LanguageDefinition.TryGetValue(input, out output);
            Debug.LogWarning($"[CCK] [LocalizationProvider] Unable to use localized string: {e}. The defined language is likely not or not fully supported.");
            return output;
        }
    }
    
}
