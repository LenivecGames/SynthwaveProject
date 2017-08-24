using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Reflection;

namespace NeonSpace.Editors
{
    [CustomEditor(typeof(ConfigDatabase))]
    public class ConfigDatabaseInspector : Editor
    {
        private ConfigDatabase _Database = null;

        private void OnEnable()
        {
            _Database = target as ConfigDatabase;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }

    }
}
