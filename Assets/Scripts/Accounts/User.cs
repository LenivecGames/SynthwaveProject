using UnityEngine;
using System.Collections;

namespace NeonSpace.Accounts
{
    public class User
    {

        public readonly int Synthese;
        public readonly int MaxScore;
        public readonly int Stage;

        public User(int synthese, int maxScore, int stage)
        {
            Synthese = synthese;
            MaxScore = maxScore;
            Stage = stage;

        }
    }
}
