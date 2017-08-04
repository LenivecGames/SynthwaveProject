using UnityEngine;
using System.Collections;

    [DisallowMultipleComponent]
    public class Coroutiner : MonoBehaviour
    {

        private static Coroutiner instance;

        public static void Start(IEnumerator routine)
        {
            GetInstance().StartLocalCoroutine(routine);
        }

        public static void Stop(IEnumerator routine)
        {
            GetInstance().StopLocalCoroutine(routine);
        }

        public void StartLocalCoroutine(IEnumerator routine)
        {
            StartCoroutine(routine);
        }

        public void StopLocalCoroutine(IEnumerator routine)
        {
            StopCoroutine(routine);
        }

        private static Coroutiner GetInstance()
        {
            if (instance == null)
            {
                GameObject go = new GameObject("Coroutiner");
                instance = go.AddComponent<Coroutiner>();
            }
            return instance;
        }

    }
