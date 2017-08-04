using UnityEngine;
using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Lean.Localization
{
    [Serializable]
    class Wrapper<T>
    {
        public T[] Items;
    }

    public static class LocalizationJsonSerializer
    {

        public static void Export(List<LeanPhrase> phrases, string path, bool prettyPrint)
        {
            if (phrases != null)
            {
                Wrapper<LeanPhrase> wrapper = new Wrapper<LeanPhrase>();
                wrapper.Items = phrases.ToArray();
                string data = JsonUtility.ToJson(wrapper, prettyPrint);
                File.WriteAllText(path, data);
            }
            else {
                throw new ArgumentNullException();
            }
        }

        public static List<LeanPhrase> Import(string path)
        {
            //List<LeanPhrase> phrases;
            if (File.Exists(path))
            {
                string data = File.ReadAllText(path);
                Wrapper<LeanPhrase> wrapper = JsonUtility.FromJson<Wrapper<LeanPhrase>>(data);
                return wrapper.Items.ToList();
            }
            else {
                throw new ArgumentException();
            }
        }

    }
}
