using UnityEngine;
using UnityEditor;
using System.Collections;

namespace LiteDB.Editor
{
    public class AddCollectionWindow : EditorWindow
    {
        static DatabaseReference _Database;
        string _CollectionName;

        public static void ShowWindow(DatabaseReference database)
        {
            EditorWindow window = GetWindow<AddCollectionWindow>(true, "Add collection", true);
            window.position = new Rect(new Vector2(Screen.width/2,Screen.height/2), new Vector2(400, 60));
            _Database = database;
        }

        public static void ShowWindow(DatabaseReference database,out string input)
        {
            input = "";
            EditorWindow window = GetWindow<AddCollectionWindow>(true, "Add collection", true);
            window.position = new Rect(new Vector2(Screen.width / 2, Screen.height / 2), new Vector2(400, 60));
            _Database = database;
        }

        private void OnGUI()
        {

            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("New collection name:");
            _CollectionName = EditorGUILayout.TextField( _CollectionName, GUILayout.Width(400));
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginHorizontal();
            if(GUILayout.Button("Ok", GUILayout.Width(200)))
            {
                
                if (_Database.LiteDatabase.CollectionExists(_CollectionName))
                {
                    EditorUtility.DisplayDialog("Collection with that name already exists", string.Format("Cannot add collection \"{0}\", collection with that name already exists.", _CollectionName), "Ok");
                }
                var coll = _Database.LiteDatabase.GetCollection(_CollectionName);
                var newDoc = new BsonDocument
                {
                    ["_id"] = ObjectId.NewObjectId()
                };

                coll.Insert(newDoc);
                coll.Delete(newDoc["_id"]);
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
