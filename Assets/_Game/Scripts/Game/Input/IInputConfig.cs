using UnityEngine;

namespace Assets._Game.Scripts.Game.Input
{
    interface IInputConfig
    {
        bool changeByTouch { get; }
        bool changeByClick { get; }
        KeyCode changeDirectionKey { get; }
    }
}
