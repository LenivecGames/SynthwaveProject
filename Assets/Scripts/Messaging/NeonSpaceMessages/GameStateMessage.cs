using UnityEngine;
using System.Collections;

using Messaging;

namespace NeonSpace
{
    public class GameStateMessage : IMessage
    {
        public readonly GameState GameState;
        public GameStateMessage(GameState gameState)
        {
            GameState = gameState;
        }
    }
}
