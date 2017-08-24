using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace NeonSpace
{
    [CreateAssetMenu(fileName = "ConfigDatabase", menuName = "Neon Space/Config Database")]
    public class ConfigDatabase : ScriptableObject
    {
        public SpaceshipConfig[] SpaceshipConfigs;
        public ShieldConfig[] ShieldConfigs;
        public WeaponConfig[] WeaponConfigs;
    }
}
