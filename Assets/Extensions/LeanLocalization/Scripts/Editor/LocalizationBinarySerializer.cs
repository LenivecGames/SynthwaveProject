using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Lean.Localization
{
    public static class LocalizationBinarySerializer
    {
        public static void Export(List<LeanPhrase> phrases, string path)
        {
            if(phrases == null)
            {
                throw new ArgumentNullException();
            }

            //serialize
            using (Stream stream = File.Open(path, FileMode.Create))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                bformatter.Serialize(stream, phrases);
            }
        }

        public static List<LeanPhrase> Import(string path)
        {
            if(!File.Exists(path))
            {
                throw new ArgumentException("Path not found", "path");
            }

            using (Stream stream = File.Open(path, FileMode.Open))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                List<LeanPhrase> importedPhrases = (List<LeanPhrase>)bformatter.Deserialize(stream);
                return importedPhrases;
            }
        }

    }
}
