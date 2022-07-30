using System.Collections;
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif
using UnityEngine;
using UnityEngine.UI;
#if CCK_ADDIN_TRANSLATABLE_TMP
using TMPro;
#endif

namespace ABI.CCK.Components
{
    public class CVRTranslatable : MonoBehaviour
    {

        public List<ObjectTranslatable_t> Translatables = new List<ObjectTranslatable_t>();

        [System.Serializable]
        public class ObjectTranslatable_t
        {
            public TranslatableType Type = TranslatableType.Text;
            public List<Translation_t> Translations = new List<Translation_t>();
#if CCK_ADDIN_TRANSLATABLE_TMP
            public TMP_Text TmpText;
#endif
            public Text Text;
            public AudioSource Source;
            public string FallbackLanguage = "en";
            
#if UNITY_EDITOR
            public ReorderableList reorderableList;
            public Translation_t entity;

            public ReorderableList GetList()
            {
                if (reorderableList == null)
                {
                    reorderableList = new ReorderableList(Translations, typeof(Translation_t),
                        true, true, true, true);
                    reorderableList.drawHeaderCallback = OnDrawHeader;
                    reorderableList.drawElementCallback = OnDrawElement;
                    reorderableList.elementHeightCallback = OnHeightElement;
                    reorderableList.onAddCallback = OnAdd;
                    reorderableList.onChangedCallback = OnChanged; 
                }

                return reorderableList;
            }

            private void OnDrawElement(Rect rect, int index, bool isactive, bool isfocused)
            {
                if (index > Translations.Count) return;
                entity = Translations[index];
                
                rect.y += 2;
                rect.x += 12;
                rect.width -= 12;
                Rect _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);

                EditorGUI.LabelField(_rect, "Language");
                _rect.x += 100;
                _rect.width = rect.width - 100;

                var selectedIndex = CVRTranslatable.Languages.Keys.ToList().FindIndex(match => match == entity.Language);

                selectedIndex = EditorGUI.Popup(_rect, selectedIndex, CVRTranslatable.Languages.Values.ToArray());

                entity.Language = CVRTranslatable.Languages.Keys.ToArray()[selectedIndex];
            
                rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
                _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight * 
                                                      (Type==TranslatableType.AudioClip||Type==TranslatableType.GameObject?1f:3f));

                EditorGUI.LabelField(_rect, Type==TranslatableType.AudioClip?"Audio Clip":(Type==TranslatableType.GameObject?"Object":"Text"));
                _rect.x += 100;
                _rect.width = rect.width - 100;

                switch (Type)
                {
                    case TranslatableType.Text:
#if CCK_ADDIN_TRANSLATABLE_TMP
                    case TranslatableType.TextMeshPro:
#endif
                        entity.Text = EditorGUI.TextArea(_rect, entity.Text);
                        break;
                    case TranslatableType.AudioClip:
                        entity.Clip = (AudioClip) EditorGUI.ObjectField(_rect, entity.Clip, typeof(AudioClip));
                        break;
                    case TranslatableType.GameObject:
                        entity.Object = (GameObject) EditorGUI.ObjectField(_rect, entity.Object, typeof(GameObject), true);
                        break;
                }
            }

            private float OnHeightElement(int index)
            {
                return EditorGUIUtility.singleLineHeight * (Type==TranslatableType.AudioClip||Type==TranslatableType.GameObject?2.5f:5f);
            }

            private void OnChanged(ReorderableList list)
            {
                //EditorUtility.SetDirty(target);
            }

            private void OnAdd(ReorderableList list)
            {
                Translations.Add(new Translation_t());
            }

            private void OnDrawHeader(Rect rect)
            {
                Rect _rect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);

                GUI.Label(_rect, "Translations");
            }
#endif
        }

        [System.Serializable]
        public class Translation_t
        {
            public string Language = "en";
            public string Text;
            public AudioClip Clip;
            public GameObject Object;
        }

        public enum TranslatableType
        {
            AudioClip = 0,
            Text = 1,
#if CCK_ADDIN_TRANSLATABLE_TMP
            TextMeshPro = 2,
#endif
            GameObject = 3
        }

        public static Dictionary<string, string> Languages = new Dictionary<string, string>()
        {
            {"ab", "Abkhazian"},
            {"aa", "Afar"},
            {"af", "Afrikaans"},
            {"ak", "Akan"},
            {"sq", "Albanian"},
            {"am", "Amharic"},
            {"ar", "Arabic"},
            {"an", "Aragonese"},
            {"hy", "Armenian"},
            {"as", "Assamese"},
            {"av", "Avaric"},
            {"ae", "Avestan"},
            {"ay", "Aymara"},
            {"az", "Azerbaijani"},
            {"bm", "Bambara"},
            {"ba", "Bashkir"},
            {"eu", "Basque"},
            {"be", "Belarusian"},
            {"bn", "Bengali"},
            {"bh", "Bihari languages"},
            {"bi", "Bislama"},
            {"bs", "Bosnian"},
            {"br", "Breton"},
            {"bg", "Bulgarian"},
            {"my", "Burmese"},
            {"ca", "Catalan, Valencian"},
            {"ch", "Chamorro"},
            {"ce", "Chechen"},
            {"ny", "Chichewa, Chewa, Nyanja"},
            {"zh", "Chinese"},
            {"cv", "Chuvash"},
            {"kw", "Cornish"},
            {"co", "Corsican"},
            {"cr", "Cree"},
            {"hr", "Croatian"},
            {"cs", "Czech"},
            {"da", "Danish"},
            {"dv", "Divehi, Dhivehi, Maldivian"},
            {"nl", "Dutch, Flemish"},
            {"dz", "Dzongkha"},
            {"en", "English"},
            {"eo", "Esperanto"},
            {"et", "Estonian"},
            {"ee", "Ewe"},
            {"fo", "Faroese"},
            {"fj", "Fijian"},
            {"fi", "Finnish"},
            {"fr", "French"},
            {"ff", "Fulah"},
            {"gl", "Galician"},
            {"ka", "Georgian"},
            {"de", "German"},
            {"el", "Greek, Modern(1453–)"},
            {"gn", "Guarani"},
            {"gu", "Gujarati"},
            {"ht", "Haitian, Haitian Kreol"},
            {"ha", "Hausa"},
            {"he", "Hebrew"},
            {"hz", "Herero"},
            {"hi", "Hindi"},
            {"ho", "Hiri Motu"},
            {"hu", "Hungarian"},
            {"ia", "Interlingua (International Auxiliary Language Association)"},
            {"id", "Indonesian"},
            {"ie", "Interlingue, Occidental"},
            {"ga", "Irish"},
            {"ig", "Igbo"},
            {"ik", "Inupiaq"},
            {"io", "Ido"},
            {"is", "Icelandic"},
            {"it", "Italian"},
            {"iu", "Inuktitut"},
            {"ja", "Japanese"},
            {"jv", "Javanese"},
            {"kl", "Kalaallisut, Greenlandic"},
            {"kn", "Kannada"},
            {"kr", "Kanuri"},
            {"ks", "Kashmiri"},
            {"kk", "Kazakh"},
            {"km", "Central Khmer"},
            {"ki", "Kikuyu, Gikuyu"},
            {"rw", "Kinyarwanda"},
            {"ky", "Kirghiz, Kyrgyz"},
            {"kv", "Komi"},
            {"kg", "Kongo"},
            {"ko", "Korean"},
            {"ku", "Kurdish"},
            {"kj", "Kuanyama, Kwanyama"},
            {"la", "Latin"},
            {"lb", "Luxembourgish, Letzeburgesch"},
            {"lg", "Ganda"},
            {"li", "Limburgan, Limburger, Limburgish"},
            {"ln", "Lingala"},
            {"lo", "Lao"},
            {"lt", "Lithuanian"},
            {"lu", "Luba-Katanga"},
            {"lv", "Latvian"},
            {"gv", "Manx"},
            {"mk", "Macedonian"},
            {"mg", "Malagasy"},
            {"ms", "Malay"},
            {"ml", "Malayalam"},
            {"mt", "Maltese"},
            {"mi", "Maori"},
            {"mr", "Marathi"},
            {"mh", "Marshallese"},
            {"mn", "Mongolian"},
            {"na", "Nauru"},
            {"nv", "Navajo, Navaho"},
            {"nd", "North Ndebele"},
            {"ne", "Nepali"},
            {"ng", "Ndonga"},
            {"nb", "Norwegian Bokmål"},
            {"nn", "Norwegian Nynorsk"},
            {"no", "Norwegian"},
            {"ii", "Sichuan Yi, Nuosu"},
            {"nr", "South Ndebele"},
            {"oc", "Occitan"},
            {"oj", "Ojibwa"},
            {"cu", "Church Slavic, Old Slavonic, Church Slavonic, Old Bulgarian, Old Church Slavonic"},
            {"om", "Oromo"},
            {"or", "Oriya"},
            {"os", "Ossetian, Ossetic"},
            {"pa", "Panjabi, Punjabi"},
            {"pi", "Pali"},
            {"fa", "Persian"},
            {"pl", "Polish"},
            {"ps", "Pashto, Pushto"},
            {"pt", "Portuguese"},
            {"qu", "Quechua"},
            {"rm", "Romansh"},
            {"rn", "Rundi"},
            {"ro", "Romanian, Moldavian, Moldovan"},
            {"ru", "Russian"},
            {"sa", "Sanskrit"},
            {"sc", "Sardinian"},
            {"sd", "Sindhi"},
            {"se", "Northern, Sami"},
            {"sh", "Serbo-Croatian"},
            {"sm", "Samoan"},
            {"sg", "Sango"},
            {"sr", "Serbian"},
            {"gd", "Gaelic, Scottish Gaelic"},
            {"sn", "Shona"},
            {"si", "Sinhala, Sinhalese"},
            {"sk", "Slovak"},
            {"sl", "Slovenian"},
            {"so", "Somali"},
            {"st", "Southern Sotho"},
            {"es", "Spanish, Castilian"},
            {"su", "Sundanese"},
            {"sw", "Swahili"},
            {"ss", "Swati"},
            {"sv", "Swedish"},
            {"ta", "Tamil"},
            {"te", "Telugu"},
            {"tg", "Tajik"},
            {"th", "Thai"},
            {"ti", "Tigrinya"},
            {"bo", "Tibetan"},
            {"tk", "Turkmen"},
            {"tl", "Tagalog"},
            {"tn", "Tswana"},
            {"to", "Tonga (Tonga Islands)"},
            {"tr", "Turkish"},
            {"ts", "Tsonga"},
            {"tt", "Tatar"},
            {"tw", "Twi"},
            {"ty", "Tahitian"},
            {"ug", "Uighur, Uyghur"},
            {"uk", "Ukrainian"},
            {"ur", "Urdu"},
            {"uz", "Uzbek"},
            {"ve", "Venda"},
            {"vi", "Vietnamese"},
            {"vo", "Volapük"},
            {"wa", "Walloon"},
            {"cy", "Welsh"},
            {"wo", "Wolof"},
            {"fy", "Western Frisian"},
            {"xh", "Xhosa"},
            {"yi", "Yiddish"},
            {"yo", "Yoruba"},
            {"za", "Zhuang, Chuang"},
            {"zu", "Zulu"}
        };
    }
}
