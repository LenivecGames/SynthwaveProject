using UnityEngine;
using UnityEditor;
using System.Collections;
namespace LiteDB.Editor
{
    public class InputBoxWindow : EditorWindow
    {
        private bool? _Result = null;




        private new void Show() { }

        public void OnDestroy()
        {
            _Result = false;
        }
    }
}
