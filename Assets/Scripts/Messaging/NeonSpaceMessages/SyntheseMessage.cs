using UnityEngine;
using System.Collections;

using Messaging;

namespace NeonSpace
{
    public class SyntheseMessage : IMessage
    {
        public readonly int Amount;
        public readonly IncreaseDecreaseType IncreaseDecreaseType;
        public SyntheseMessage(int amount, IncreaseDecreaseType increaseDecreaseType)
        {
            Amount = amount;
            IncreaseDecreaseType = increaseDecreaseType;
        }
    }
}
