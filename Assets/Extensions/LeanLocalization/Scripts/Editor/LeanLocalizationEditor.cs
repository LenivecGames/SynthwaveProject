using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace Lean.Localization
{
    public class LeanLocalizationEditor : EditorWindow
    {

        /*private static EditorWindow EditorWindow;
        private LeanLocalization _Localization;

        private GenericMenu _FileOptionsGenericMenu = new GenericMenu();

        private string _SelectedLanguage;
        private GenericMenu _NewLanguageGenericMenu = new GenericMenu();

        private LeanPhrase _NewPhrase = new LeanPhrase();
        private LeanTranslation _NewTranslation = new LeanTranslation();

        private Vector2 _PhraseScrollPosition = new Vector2();

        private Event _CurrentEvent = new Event();

        [MenuItem("Extensions/Lean Localization/Localization Editor")]
        private static void ShowEditor()
        {
            EditorWindow = GetWindow(typeof(LeanLocalizationEditor), false, "Localization Editor");
        }

        private void OnEnable()
        {
            _SelectedLanguage = LeanLocalization.CurrentLanguage;

        }
        private List<LeanPhrase> phrases = new List<LeanPhrase>();
        private void OnGUI()
        {
            bool localizationSetup = LocalizationSetup();
            if(!localizationSetup)
            {
                return;
            }

            _CurrentEvent = Event.current;

            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar, GUILayout.Width(Screen.width));
            if(GUILayout.Button("File", EditorStyles.toolbarButton, GUILayout.Width(100)))
            {
                _FileOptionsGenericMenu.AddItem(new GUIContent("Export"), false, ImportTranslations);
                _FileOptionsGenericMenu.AddItem(new GUIContent("Import"), false, ExportTranslations);
                _FileOptionsGenericMenu.DropDown(EditorGUILayout.GetControlRect());
            }
            EditorGUILayout.EndHorizontal();

            DrawLanguageToolbar();
            if(GUILayout.Button("Export to binary TEMP"))
            {
                LocalizationBinarySerializer.Export(_Localization.Phrases, "Assets/Translations.bin");
            }
            if (GUILayout.Button("Import from binary TEMP"))
            {
                phrases = LocalizationBinarySerializer.Import("Assets/Translations.bin");
            }
            if (GUILayout.Button("Export to JSON TEMP"))
            {
                LocalizationJsonSerializer.Export(_Localization.Phrases, "Assets/Translations.json", true);
            }
            if (GUILayout.Button("Import from JSON TEMP"))
            {
                phrases = LocalizationJsonSerializer.Import("Assets/Translations.json");
            }
            /*for(int i = 0; i < phrases.Count; i++)
            {
                EditorGUILayout.BeginHorizontal("Box");
                GUILayout.Label(phrases[i].Name);
                EditorGUILayout.EndHorizontal();
            }*//*
            DrawTextLocalization(_Localization);
        }


        [MenuItem("Extensions/Lean Localization/Localization Object", false, 1)]
        public static void CreateLocalization()
        {
            if(FindObjectOfType<LeanLocalization>() != null)
            {
                Debug.Log("Localization object already setup");
                return;
            }
            var gameObject = new GameObject(typeof(LeanLocalization).Name);

            UnityEditor.Undo.RegisterCreatedObjectUndo(gameObject, "Create Localization");

            gameObject.AddComponent<LeanLocalization>();

            Selection.activeGameObject = gameObject;
        }

        private void DrawTextLocalization(LeanLocalization localization)
        {
            EditorGUILayout.BeginVertical();

            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
            string searchTemp = EditorGUILayout.TextField("Search phrase", GUILayout.Width(800));
            EditorGUILayout.EndHorizontal();

            _PhraseScrollPosition = EditorGUILayout.BeginScrollView(_PhraseScrollPosition);
            LeanTranslation translation;
            for(int i = 0; i < localization.Phrases.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                //EditorGUILayout.LabelField(localization.Phrases[i].Name);
                localization.Phrases[i].Name = EditorGUILayout.TextField(localization.Phrases[i].Name, GUILayout.Width(400));
                for (int j = 0; j < localization.Phrases[i].Translations.Count; j++)
                {
                    if (localization.Phrases[i].Translations[j].Language == _SelectedLanguage)
                    {
                        //EditorGUILayout.LabelField(localization.Phrases[i].Translations[j].Text);
                        localization.Phrases[i].Translations[j].Text = EditorGUILayout.TextField(localization.Phrases[i].Translations[j].Text, GUILayout.Width(400));

                    }

                }
                translation = localization.Phrases[i].FindTranslation(_SelectedLanguage);
                if (translation == null)
                {
                    if (GUILayout.Button("Create Translation", GUILayout.Width(400)))
                    {
                        localization.AddTranslation(_SelectedLanguage, localization.Phrases[i].Name);
                    }
                }
                EditorGUILayout.EndHorizontal();
            }


            if (GUILayout.Button("Add Phrase", GUILayout.Width(800)))
            {
                localization.AddPhrase("New Phrase");
            }
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
        }

        private void DrawLanguageToolbar()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Language", GUILayout.ExpandWidth(false));
            for (int i = 0; i < LeanLocalization.CurrentLanguages.Count; i++)
            {
                if (LeanLocalization.CurrentLanguages[i] == _SelectedLanguage)
                {
                    GUI.color = Color.green;
                }
                if (GUILayout.Button(LeanLocalization.CurrentLanguages[i], GUILayout.ExpandWidth(false)))
                {
                    _SelectedLanguage = LeanLocalization.CurrentLanguages[i];
                    GUI.FocusControl("");
                }
                GUI.color = Color.white;
            }
            if (GUILayout.Button("+", GUILayout.Width(25f)))
            {
                Rect menuRect = EditorGUILayout.GetControlRect();

                //_Localization.AddLanguage();
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Culture");

            EditorGUILayout.EndHorizontal();
        }

        private void ImportTranslations()
        {

        }

        private void ExportTranslations()
        {

        }

        private bool LocalizationSetup()
        {
            if (FindObjectOfType<LeanLocalization>() != null)
            {
                _Localization = FindObjectOfType<LeanLocalization>();
                return true;
            }
            else if (_Localization == null)
            {
                if (GUILayout.Button("Create Localization Object"))
                {
                    CreateLocalization();
                }
                return false;
            }
            else if (!_Localization.isActiveAndEnabled)
            {
                EditorGUILayout.HelpBox("Set Localization component to active state", MessageType.None, true);
                _Localization.enabled = EditorGUILayout.Toggle("Set active", _Localization.enabled);
                return false;
            }
            return false;
        }*/
    }
}
