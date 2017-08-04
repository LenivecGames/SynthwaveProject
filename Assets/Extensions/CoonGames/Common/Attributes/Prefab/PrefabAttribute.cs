using System;
using UnityEngine;

namespace CoonGames
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class PrefabAttribute : PropertyAttribute
    {
        public readonly bool AllowInstantiatedPrefabs;

        public PrefabAttribute(bool allowInstantiatedPrefabs = false)
        {
            AllowInstantiatedPrefabs = allowInstantiatedPrefabs;
        }

    }

}