using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace NeonSpace.UserInterface
{
    public class ChangeGameStateWidget : Widget
    {
        [SerializeField]
        private GameState GameState;
        [SerializeField]
        private Button ChangeGameStateButton;

        // Use this for initialization
        protected override void Start()
        {
            ChangeGameStateButton.onClick.AddListener(() =>
            {
                EventManager.Publish(new GameStateMessage(GameState));
            });
        }
    }
}
