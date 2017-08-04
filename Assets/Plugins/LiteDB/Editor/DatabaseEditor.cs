using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace LiteDB.Editor
{
    public class DatabaseEditor : EditorWindow
    {

        private static EditorWindow _EditorWindow;
        private static SerializedObject _SerializedObject;
        private DatabaseEditorConfig _DatabaseEditorConfig;

        private DatabaseReference _SelectedDatabase;

        private CollectionReference _SelectedCollection;

        private string _DatabasePath;
        private string _NewCollectionName;

        //private CollectionReference selectedCollection;

        //private DatabaseReference selectedDatabase;

        [MenuItem("Extensions/LiteDB/Database Editor")]
        static void Initialize()
        {
            _EditorWindow = GetWindow(typeof(DatabaseEditor), false, "Database Editor");
            _EditorWindow.minSize = new Vector2(400, 400);
        }

        private void OnEnable()
        {
            _SerializedObject = new SerializedObject(this);

            if(_DatabaseEditorConfig == null)
            {
                _DatabaseEditorConfig = CreateInstance<DatabaseEditorConfig>();
            }
            else if(!string.IsNullOrEmpty(_DatabaseEditorConfig.DatabasePath))
            {
                //_SelectedDatabase = new DatabaseReference(_DatabaseEditorConfig.DatabasePath, "");
                OpenDatabase(_DatabaseEditorConfig.DatabasePath);
            }
        }


        private void OnGUI()
        {

            DrawToolbar();

            if (_SelectedDatabase != null)
            {

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.BeginVertical(GUILayout.Width(Screen.width / 4));

                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Add collection"))
                {
                    AddCollectionWindow.ShowWindow(_SelectedDatabase);
                }

                EditorGUILayout.EndHorizontal();

                foreach (string collectionName in _SelectedDatabase.LiteDatabase.GetCollectionNames().ToArray())
                {
                    EditorGUILayout.BeginHorizontal();
                    if (GUILayout.Button(collectionName))
                    {
                        _SelectedCollection = new CollectionReference(collectionName, _SelectedDatabase); //_SelectedDatabase.LiteDatabase.GetCollection(collectionName);
                    }
                    if (GUILayout.Button("Rename", GUILayout.Width(60)))
                    {
                        RenameCollectionWindow.ShowWindow(_SelectedDatabase, collectionName);
                    }
                    GUI.color = Color.red;
                    if (GUILayout.Button("Drop", GUILayout.Width(50)))
                    {
                        DropCollection(collectionName);
                    }
                    GUI.color = Color.white;

                    EditorGUILayout.EndHorizontal();
                }

                EditorGUILayout.EndVertical();

                EditorGUILayout.BeginVertical();
                EditorGUILayout.BeginHorizontal();
                if (_SelectedCollection != null)
                {
                    if (GUILayout.Button("Add item", GUILayout.Width(100)))
                    {
                        AddItemToCollection(_SelectedCollection);
                    }
                    
                }
                EditorGUILayout.EndHorizontal();
                ShowCollection<BsonDocument>(_SelectedCollection);
                EditorGUILayout.EndVertical();
                EditorGUILayout.EndHorizontal();

            }
            _SerializedObject.Update();
        }

        private void CreateDatabase()
        {
            string path = EditorUtility.SaveFilePanelInProject("Create database", "LiteDB", "db", "");
            if(!String.IsNullOrEmpty(path))
            {
                _DatabaseEditorConfig.DatabasePath = path;
                _SelectedDatabase = new DatabaseReference(path, "");
            }
        }

        private void SelectDatabase()
        {
            string path = EditorUtility.OpenFilePanel("Select database", Application.dataPath, "db");
            //Debug.Log(Application.dataPath + " | " + Application.persistentDataPath);
            if (path != null && path.Contains(Application.dataPath))
            {
                OpenDatabase(path);
            }
            else
            {
                EditorUtility.DisplayDialog("Need to open in the asset folder", "You need to save the file inside of the project's assets folder", "Ok");
                //throw new ArgumentNullException();
            }
        }

        private void OpenDatabase(string path)
        {
            if (!File.Exists(path))
            {
                EditorUtility.DisplayDialog("Cannot open database, file not found.", "File not found", "Ok");
                return;
            }

            try
            {
                _SelectedDatabase = new DatabaseReference(path, "");
                _DatabaseEditorConfig.DatabasePath = path;
            }
            catch (Exception e)
            {
                EditorUtility.DisplayDialog("Failed to open database:" + Environment.NewLine + e.Message, "Database Error", "Ok");
                return;
            }
        }

        private void ShowCollection<T>(CollectionReference collectionReference)
        {

            if (collectionReference != null)
            {
                List<string> keys = new List<string>();
                foreach (var item in collectionReference.Items)
                {
                    keys = item.LiteDocument.Keys.Union(keys).ToList();
                }
                EditorGUILayout.BeginHorizontal("Box");
                foreach (var key in keys)
                {
                    EditorGUILayout.LabelField(key);
                }
                EditorGUILayout.EndHorizontal();

                for (int i = 0; i < collectionReference.LiteCollection.Count(); i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    if(GUILayout.Button(i.ToString()))
                    {
                        EditorGUILayout.EndHorizontal();
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }
        }

        private void sss(CollectionReference collectionReference)
        {
            List<string> keys = new List<string>();
            foreach (var item in collectionReference.Items)
            {
                keys = item.LiteDocument.Keys.Union(keys).ToList();
            }
            foreach (var key in keys)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(key);
                EditorGUILayout.EndHorizontal();
            }
        }

        private void DropCollection(string collectionName)
        {
            if (EditorUtility.DisplayDialog("Are you sure?", String.Format("Are you sure you want to drop collection \"{0}\" ?", collectionName), "Ok", "Cancel"))
            {
                _SelectedDatabase.DropCollection(collectionName);
                _SelectedCollection = null;
            }
        }

        private void AddItemToCollection(CollectionReference collectionReference)
        {
            BsonDocument newDoc = new BsonDocument{["_id"] = ObjectId.NewObjectId()};
            collectionReference.AddItem(newDoc);
        }

        private void DrawToolbar()
        {
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar, GUILayout.Width(Screen.width));
            if (GUILayout.Button("File", EditorStyles.toolbarButton, GUILayout.Width(100)))
            {
                GenericMenu menu = new GenericMenu();
                menu.AddItem(new GUIContent("Create"), false, CreateDatabase);
                menu.AddItem(new GUIContent("Select"), false, SelectDatabase);

                // display the menu
                menu.ShowAsContext();
            }

            if (_SelectedDatabase != null)
            {
                EditorGUILayout.LabelField(_DatabaseEditorConfig.DatabasePath);
            }

            EditorGUILayout.EndHorizontal();
        }

        private void OnDestroy()
        {
            if (_SelectedDatabase != null)
            {
                _SelectedDatabase.Dispose();
            }
        }

    }
}
