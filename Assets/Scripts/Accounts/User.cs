using UnityEngine;
using System;
using System.Collections;

namespace NeonSpace.Accounts
{
    [Serializable]
    public class User
    {

        public int Synthese { get { return _Synthese; } }
        private int _Synthese;
        public int MaxScore { get { return _MaxScore; } }
        private int _MaxScore;
        public int Stage { get { return _Stage; }}
        private int _Stage;

        public string Language;
        public float Volume;
        public bool VolumeMuted;



        public User(int synthese, int maxScore, int stage)
        {
            _Synthese = synthese;
            _MaxScore = maxScore;
            _Stage = stage;

        }

        public void IncreaseSynthese(int value)
        {
            _Synthese += Mathf.Abs(value);
        }
        public void DecreaseSynthese(int value)
        {
            if(_Synthese < value)
            {
                return;
            }
            _Synthese -= Mathf.Abs(value);
        }
    }
}
