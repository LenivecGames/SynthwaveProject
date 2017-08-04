using UnityEngine;
using UnityEditor;
using System.Collections;

namespace LiteDB.Editor
{
    [System.Serializable]
    public class DatabaseEditorConfig : ScriptableObject
    {
        public string DatabasePath;


        private void OnEnable()
        {
            hideFlags = HideFlags.HideAndDontSave;
        }
    }
}
