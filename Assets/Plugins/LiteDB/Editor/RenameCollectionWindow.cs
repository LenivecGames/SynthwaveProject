using UnityEngine;
using UnityEditor;
using System.Collections;

namespace LiteDB.Editor
{
    public class RenameCollectionWindow : EditorWindow
    {
        private string _NewCollectionName;
        private static string _OldCollectionName;
        private static DatabaseReference _Database;

        static bool? _Result = null;

        public static void ShowWindow(DatabaseReference database, string collectionName)
        {
            if(collectionName == "_files" || collectionName == "_chunks")
            {
                return;
            }
            EditorWindow window = GetWindow<RenameCollectionWindow>(true, string.Format("Rename collection \"{0}\"", collectionName), true);

            window.position = new Rect(new Vector2(Screen.width / 2, Screen.height / 2), new Vector2(400, 60));
            _Database = database;
            _OldCollectionName = collectionName;

            
        }

        public static bool? ShowWindow(DatabaseReference database, string collectionName, out string input)
        {
            input = "";
            while (_Result == null)
            {

            }
            if (collectionName == "_files" || collectionName == "_chunks")
            {
                return _Result;
            }
            EditorWindow window = GetWindow<RenameCollectionWindow>(true, string.Format("Rename collection \"{0}\"", collectionName), true);

            window.position = new Rect(new Vector2(Screen.width / 2, Screen.height / 2), new Vector2(400, 60));
            _Database = database;
            _OldCollectionName = collectionName;
            return _Result;
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("New collection name:");
            _NewCollectionName = EditorGUILayout.TextField(_NewCollectionName, GUILayout.Width(400));
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Ok", GUILayout.Width(200)))
            {
                if (_Database.LiteDatabase.CollectionExists(_NewCollectionName))
                {
                    EditorUtility.DisplayDialog("Collection with that name already exists", string.Format("Cannot add collection \"{0}\", collection with that name already exists.", _NewCollectionName), "Ok");
                }

                _Database.RenameCollection(_OldCollectionName, _NewCollectionName);

                Close();
            }
            if (GUILayout.Button("Cancel", GUILayout.Width(200)))
            {
                Close();
            }
            EditorGUILayout.EndHorizontal();
        }


    }
}
