namespace Alquimiaware
{
    using UnityEngine;

    public class KeyEvent
    {
        public static bool JustReleased(KeyCode keycode)
        {
            // Key events should only be triggered during their event type
            return Event.current.type == EventType.KeyUp
                && Event.current.keyCode == keycode;
        }
    }
}