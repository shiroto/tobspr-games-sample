using System;
using UnityEngine;

namespace UnityUI
{
    [Serializable]
    public struct KeyBinding
    {
        public InputAction action;
        public KeyCode keyCode;

        public override string ToString()
        {
            return $"{keyCode} => {action}";
        }
    }
}